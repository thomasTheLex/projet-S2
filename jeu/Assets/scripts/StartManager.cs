using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject[] AiToSpawn;
    private GameObject introCamera;
    private GameObject playerCamera;
    public bool playerCanMove = false;
    public int scene;


    private void OnLevelWasLoaded(int level)
    {
        scene = level;  
        playerCanMove = false;
        Instantiate(playerPrefab, new Vector3(0, 0, 0), playerPrefab.transform.rotation);
        introCamera = GameObject.FindGameObjectWithTag("IntroCam");
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");

        //Rajouter le spawn de ai avec un foreach ici

    }

    private void Update()
    {
        if (playerCanMove)
        {
            introCamera.SetActive(false);
            playerCamera.SetActive(true);
        }
        else
        {
            introCamera.SetActive(true);
            playerCamera.SetActive(false);
        }
    }

    private IEnumerable WaitForSecond(int second)
    {
        yield return new WaitForSeconds(second);
    }
}
