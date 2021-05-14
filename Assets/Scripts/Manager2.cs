using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class Manager2 : MonoBehaviour
{
    [SerializeField] private GameObject[] ghostsObjects;
    [SerializeField] private GameObject fieldOfView;
    [SerializeField] private GameObject grumblingGhost;

    public bool visionIsActive = false;

    Volume globalVolume;
    Fog fg;
    Exposure exp;
    GradientSky grSky;
    VisualEnvironment visEnv;
    Bloom bl;

    private HDAdditionalLightData hdL;

    Camera mainCam;
    GameObject dirLight;
    GameObject[] ghosts;

    private float timeToLerp = 0.25f;
    private float scaleModifier = 1;

    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        dirLight = GameObject.Find("Directional Light");
        hdL = dirLight.GetComponent<HDAdditionalLightData>();
        ghosts = GameObject.FindGameObjectsWithTag("MainGhost");

        grumblingGhost.SetActive(false);

        foreach (GameObject ghost in ghosts)
        {
            ghost.SetActive(false);
        }

        globalVolume = GameObject.Find("Global Volume").GetComponent<Volume>();
        globalVolume.profile.TryGet(out fg);
        globalVolume.profile.TryGet(out exp);
        globalVolume.profile.TryGet(out grSky);
        globalVolume.profile.TryGet(out visEnv);
        globalVolume.profile.TryGet(out bl);
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
        // Setting up the lighting and volume profile

        fg.enabled.value = false;
        exp.limitMin.value = 3.3f;
        grSky.active = true;
        visEnv.skyType.value = (int)SkyType.Gradient;
        bl.active = true;

        //hdL.intensity = 15;
        StartCoroutine(LerpFunction(15, 0.5f));

        // All objects from with the following layer name will...

        mainCam.cullingMask = ~(1 << LayerMask.NameToLayer("RealWorldEnvironment"));    // ..NOT be rendered
        mainCam.cullingMask |= 1 << LayerMask.NameToLayer("GhostsWorldEnvironment");    // ..be rendered

        // Set active the specific objects 

        foreach (GameObject ghost in ghosts)
        {
            ghost.SetActive(true);
        }

        grumblingGhost.SetActive(true);
        fieldOfView.SetActive(true);
        visionIsActive = true;

        yield return null;
    }

    private IEnumerator VisionOff()
    {
        // Setting up the lighting and volume profile

        fg.enabled.value = true;
        exp.limitMin.value = 0.8251294f;
        grSky.active = false;
        visEnv.skyType.value = (int)SkyType.PhysicallyBased;
        bl.active = false;

        //hdL.intensity = 300;
        StartCoroutine(LerpFunction(300, 0.5f));

        // All objects from with the following layer name will...

        mainCam.cullingMask |= 1 << LayerMask.NameToLayer("RealWorldEnvironment");      // ..be rendered
        mainCam.cullingMask = ~(1 << LayerMask.NameToLayer("GhostsWorldEnvironment"));  // ..NOT be rendered

        // Set false the specific objects 

        foreach (GameObject ghost in ghosts)
        {
            ghost.SetActive(false);
        }

        grumblingGhost.SetActive(false);
        fieldOfView.SetActive(false);
        visionIsActive = false;

        yield return null;
    }

    IEnumerator LerpFunction(float endValue, float duration)
    {
        float time = 0;
        float startValue = hdL.intensity;

        while (time < duration)
        {
            hdL.intensity = Mathf.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        hdL.intensity = endValue;
    }

}
