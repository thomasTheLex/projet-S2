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


    private void OnLevelWasLoaded(int level)
    {
        Instantiate(playerPrefab, new Vector3(0, 0, 0), playerPrefab.transform.rotation);
        introCamera = GameObject.FindGameObjectWithTag("IntroCam");
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");

        //Rajouter le spawn de ai avec un foreach ici

        introCamera.SetActive(true);
        playerCamera.SetActive(false);


        if (level == 1)
        {
            introCamera.transform.position = new Vector3(12, 8, -23);
            
            //Trouver un moyen ici de faire la cinématique d'intro

            introCamera.SetActive(false);
            playerCamera.SetActive(true);
        }
    }

    private IEnumerable WaitForMove(GameObject entity, Vector3 vector)
    {
        yield return new WaitForEndOfFrame();
        entity.transform.Translate(vector);
    }
}
