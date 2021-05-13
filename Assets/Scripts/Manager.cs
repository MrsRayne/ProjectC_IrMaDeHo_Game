using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [SerializeField] private string visionActiveLevel;
    [SerializeField] private GameObject[] toLoad;
    [SerializeField] private GameObject fieldOfView;

    public bool visionIsActive = false;

    GameObject dirLight;
    GameObject[] ghosts;
    Ghost[] ghostScript;
    bool[] catched;
    public bool Catched;

    private void Start()
    {
        dirLight = GameObject.Find("Directional Light (1)");
        ghosts = GameObject.FindGameObjectsWithTag("MainGhost");

        foreach(GameObject ghost in ghosts)
        {
            ghost.SetActive(false);
        }

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

        foreach(GameObject i in toLoad)
        {
            i.SetActive(false);
        }

        foreach (GameObject ghost in ghosts)
        {
            ghost.SetActive(true);
        }

        dirLight.SetActive(false);
        fieldOfView.SetActive(true);
        visionIsActive = true;
    }

    private IEnumerator VisionOff()
    {
        AsyncOperation operation = SceneManager.UnloadSceneAsync(visionActiveLevel);

        while (!operation.isDone)
        {
            yield return null;
        }

        foreach (GameObject i in toLoad)
        {
            i.SetActive(true);
        }

        foreach (GameObject ghost in ghosts)
        {
            ghost.SetActive(false);
        }

        dirLight.SetActive(true);
        fieldOfView.SetActive(false);
        visionIsActive = false;
    }

    

}
