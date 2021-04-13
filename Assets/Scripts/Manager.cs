using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class Manager : MonoBehaviour
{
    [SerializeField] private string visionActiveLevel;
    [SerializeField] private GameObject toLoad;
    [SerializeField] private GameObject globalVolume;

    private Volume volume;
    private MotionBlur dofComponent;

    private bool visionIsActive = false;

    // Start is called before the first frame update
    void Start()
    {
        volume = globalVolume.GetComponent<Volume>();
        MotionBlur tmp;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            volume.profile.TryGet<MotionBlur>(out var motionBloor);
            motionBloor = volume.profile.Add<MotionBlur>(false);

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
