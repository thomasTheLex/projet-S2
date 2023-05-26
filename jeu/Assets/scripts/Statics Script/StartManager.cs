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
    private void OnLevelWasLoaded(int level)
    {
        playerCanMove = false;
        CharacterController characterController;
        ICharacter character;
        IAController iAController;
        initializationFinish = false;
        List<GameObject> playerList = new List<GameObject>(2);
        scene = level;
        Instantiate(introCamera);
        playerCanMove = false;
        Vector3 position = new Vector3(1, 0, -10);

        List<GameObject> toRemove = new List<GameObject>(10);
        foreach(GameObject obj in NextLevel.player)
        {
            if (obj != null)
            {
                obj.transform.position = position;
                obj.transform.eulerAngles = new Vector3(0, 90, 0);
                obj.SetActive(true);

                if (obj.TryGetComponent<ICharacter>(out character))
                {
                    if (character.HaveFinish)
                    {
                        if (obj.TryGetComponent<CharacterController>(out characterController))
                        {
                            characterController.StopAllCoroutines(); //Stop la coroutine de l'animation de fin des joueurs
                            playerList.Add(characterController.gameObject); //On récupère les différents player
                        }
                        else if (obj.TryGetComponent<IAController>(out iAController))
                        {
                            iAController.StopAllCoroutines();
                            iAController.Init();
                        }
                        character.HaveFinish = false;
                    }
                    else
                    {
                        toRemove.Add(obj);
                    }
                }
            }
            if (position.z == 10) //Pour créer 3 lignes de 5
                position = new Vector3(position.x + 2, 0, -10);
            else
                position += new Vector3(0, 0, 5);
        }

        foreach (GameObject obj in toRemove)
        {
            NextLevel.player.Remove(obj);
            Destroy(obj);
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
        NextLevel.nbSurvivor = NextLevel.nbSurvivor / 2;
        NextLevel.NewLevel();
        initializationFinish = true;
        StartCoroutine(WaitEndOfCinematic());
        StartCoroutine(WaitFinish(playerList));
    }

    private void Update()
    {
    }

    private bool HavePlayerLeft()
    {
        bool res = false;
        int i = 0;
        int l = NextLevel.player.Count;
        while (i < l && !res)
        {
            if (NextLevel.player[i] != null)
            {
                CharacterController tmp;
                if (NextLevel.player[i].TryGetComponent<CharacterController>(out tmp))
                    res = tmp.HaveFinish;//On regarde la liste de tout les personnes en vie jusqu'à trouver un joueur
                i++;
            }  
        }

        return res;
    }

    private IEnumerator WaitEndOfCinematic()
    {
        yield return new WaitUntil(() => playerCanMove);
        foreach (GameObject cam in playerCamera)
        {
            if (cam != null)
                cam.SetActive(true);
        }
    }

    private IEnumerator WaitFinish(List<GameObject> playerList)
    {
        yield return new WaitUntil(() => NextLevel.peopleFinish == NextLevel.nbSurvivor);

        playerCanMove = false;
  
        if (HavePlayerLeft()) //Verifier si il y a encore au moins un joueur
        {
            if (scene == 3)
            {
                yield return new WaitForSeconds(4);
                NextLevel.EndOfGame();
                SceneManager.LoadScene(0); //Victoire
            }
            else
                SceneManager.LoadScene(scene + 1); //Niveau suivant
        }
        else
        {
            foreach (GameObject player in playerList)
            {
                var tmp = player.GetComponent<CharacterController>();
                tmp.Defeat();
            }
            yield return new WaitForSeconds(4);
            NextLevel.EndOfGame();
            SceneManager.LoadScene(0); //Défaite
        }
    }
}
