using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public static List<GameObject> player =  new List<GameObject>(60); //La liste contenant tout les personnages à faire spawn au prochain round
    public static int nbSurvivor; //Le nombre de personnes qui seront qualifié sur ce round
    public static int peopleFinish = 0; //Le nombre de personnes qui ont actuellement fini le round

    public static void Finish()
    {
        if (peopleFinish < nbSurvivor)
        {
            peopleFinish++;
        }
    }

    public static void NewLevel()
    {
        peopleFinish = 0;
    }

    public static void Start(List<GameObject> playerList)
    {
        foreach (GameObject playe in player) //On detruit les joueurs de la game precedante
            Destroy(playe.gameObject);

        player = playerList;
        foreach (GameObject obj in playerList)
            DontDestroyOnLoad(obj);
    }
}
