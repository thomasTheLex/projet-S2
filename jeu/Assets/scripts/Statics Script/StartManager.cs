using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public GameObject introCamera;
    public GameObject playerPrefab;
    //public GameObject aiPrefab;
    private GameObject[] playerCamera;
    public static bool playerCanMove = false;
    public static int scene;
    private bool initializationFinish = false;

    private void Start()
    {
        
    }
    private void OnLevelWasLoaded(int level)
    {
        
        CharacterController characterController;
        initializationFinish = false;
        List<GameObject> playerList = new List<GameObject>(2);
        scene = level;
        Instantiate(introCamera);
        playerCanMove = false;
        Vector3 position = new Vector3(1, 0, 0);
        foreach(GameObject obj in NextLevel.player)
        {
            if (obj != null)
            {
                obj.transform.position = position;
                obj.transform.eulerAngles = new Vector3(0, 90, 0);
                obj.SetActive(true);

                if (obj.TryGetComponent<CharacterController>(out characterController))
                {
                    if (characterController.HaveFinish)
                    {
                        characterController.StopAllCoroutines(); //Stop la coroutine de l'animation de fin des joueurs
                        playerList.Add(characterController.gameObject); //On récupère les différents player
                        characterController.HaveFinish = false;
                    }
                    else
                    {
                        Destroy(obj);
                    }
                        
                    
                }
            }

            position += new Vector3(0, 0, 5);
        }
        
        if (playerList.Count == 2) //Si il y a les 2 joueurs
        {
            foreach (GameObject player in playerList) //On met le split screen
            {
                if (player.GetComponent<CharacterController>().playerNumber == 1)
                    player.GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0, 0), new Vector2(0.5f, 1)); //Permet de définir la taille de la caméra
                else
                    player.GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0.5f, 0), new Vector2(0.5f, 1));
            }
        }
        else
            playerList[0].GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0, 0), new Vector2(1, 1));

        playerCamera = GameObject.FindGameObjectsWithTag("MainCamera");
        NextLevel.nbSurvivor = 1;//NextLevel.nbSurvivor / 2;
        NextLevel.NewLevel();
        initializationFinish = true;
    }

    private void Update()
    {
        if (playerCanMove)
        {
            foreach (GameObject cam in playerCamera)
            {
                if (cam != null)
                    cam.SetActive(true);
            }
                
        }

        if (NextLevel.peopleFinish == NextLevel.nbSurvivor && initializationFinish) //Si le nombre de qualifié est égale au nombre de perso ayant fini
        {
            if (StartManager.playerCanMove) //Verifier si il y a encore au moins un joueur (à faire)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(scene + 1); //Niveau suivant
            }
        }
    }
}
