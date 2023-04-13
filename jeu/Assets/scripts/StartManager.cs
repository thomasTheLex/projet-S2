using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public GameObject introCamera;
    public GameObject playerPrefab;
    //public GameObject aiPrefab;
    private GameObject playerCamera;
    public static bool playerCanMove = false;
    public static int scene;

    private void Start()
    {
        
    }
    private void OnLevelWasLoaded(int level)
    {
        scene = level;
        Instantiate(introCamera);
        playerCanMove = false;
        Vector3 position = new Vector3(1, 0, 0);
        for(int i = 0; i < NextLevel.playerToSpawn; i++)
        {
            Instantiate(playerPrefab, position, playerPrefab.transform.rotation);
            //Changer la position
        }

        /*for (int j = 0; j < NextLevel.aiToSpawn; j++)
        {
            Instantiate(aiPrefab, position, playerPrefab.transform.rotation);
            //Changer la position
        }*/

        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        NextLevel.nbSurvivor = 1;//NextLevel.nbSurvivor / 2;
        NextLevel.NewLevel();
    }

    private void Update()
    {
        if (playerCanMove)
        {
            playerCamera.SetActive(true);
        }

        if (NextLevel.peopleFinish == NextLevel.nbSurvivor) //Si le nombre de qualifi� est �gale au nombre de perso ayant fini
        {
            if (playerCanMove)
            {
                //Defaite
            }
            else
            {
                SceneManager.LoadScene(scene + 1); //Niveau suivant
            }
        }
    }
}
