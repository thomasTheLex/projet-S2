using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    private GameObject introCamera;
    private GameObject playerCamera;
    public static bool playerCanMove = false;
    public static int scene;

    private void Start()
    {
        
    }
    private void OnLevelWasLoaded(int level)
    {
        Debug.Log(NextLevel.ToSpawn[0]);
        scene = level;
        playerCanMove = false;
        Vector3 position = new Vector3(1, 0, 0);
        foreach (GameObject objet in NextLevel.ToSpawn)
        {
            if (objet != null)
                Instantiate(objet, position, Quaternion.Euler(0, 90, 0));
        }
        introCamera = GameObject.FindGameObjectWithTag("IntroCam");
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        NextLevel.nbSurvivor = NextLevel.nbSurvivor / 2;
        NextLevel.NewLevel();
    }

    private void Update()
    {
        if (playerCanMove)
        {
            playerCamera.SetActive(true);
        }

        if (NextLevel.peopleFinish == NextLevel.nbSurvivor) //Si le nombre de qualifié est égale au nombre de perso ayant fini
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
