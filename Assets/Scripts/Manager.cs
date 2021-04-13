using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [SerializeField] private string visionActiveLevel;
    [SerializeField] private GameObject toLoad;
    [SerializeField] private CanvasGroup blinkCavas;

    private bool visionIsActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            var fade = Mathf.Sin(1f * Mathf.PI);
            StartCoroutine(FadeBlinkScreen(fade, 5f));

            if (!visionIsActive)
            {
                StartCoroutine(VisionActivation());
            }
            else
            {
                StartCoroutine(VisionOff());
            }           
        }
    }


    private IEnumerator VisionActivation()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(visionActiveLevel, LoadSceneMode.Additive);

        while (!operation.isDone)
        {
            yield return null;
        }

        toLoad.SetActive(false);
        visionIsActive = true;
    }

    private IEnumerator VisionOff()
    {
        AsyncOperation operation = SceneManager.UnloadSceneAsync(visionActiveLevel);

        while (!operation.isDone)
        {
            yield return null;
        }

        toLoad.SetActive(true);
        visionIsActive = false;
    }

    private IEnumerator FadeBlinkScreen(float targetValue, float duration)
    {
        float startValue = blinkCavas.alpha;
        float time = 0;

        while (time < duration)
        {
            blinkCavas.alpha = Mathf.Lerp(startValue, targetValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        blinkCavas.alpha = targetValue;
    }
}
