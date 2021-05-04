using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [SerializeField] private string visionActiveLevel;
    [SerializeField] private GameObject toLoad;

    private bool visionIsActive = false;

    GameObject[] ghosts;
    Ghost[] ghostScript;
    bool[] catched;

    private void Start()
    {
        ghosts = GameObject.FindGameObjectsWithTag("Mainghost");
        ghostScript = new Ghost[ghosts.Length];

        for (int i = 0; i < ghosts.Length; i++)
        {
            ghostScript[i] = ghosts[i].GetComponent<Ghost>();
        }

        

    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyUp(KeyCode.E))
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

        catched = new bool[ghostScript.Length];
        for(int i = 0; i < ghostScript.Length; i++)
        {
            catched[i] = ghostScript[i].catching;
        }
        */
    }

    private void FindAllGhosts()
    {
        foreach(GameObject i in ghosts)
        {
            i.GetComponent<Ghost>();
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
