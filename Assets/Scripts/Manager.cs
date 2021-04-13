using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [SerializeField] private string visionActiveLevel;
    [SerializeField] private GameObject toLoad;

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

}
