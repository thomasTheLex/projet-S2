using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public static int playerToSpawn; //Le nombre de joueur qui se sont qualifiés
    public static int aiToSpawn; //Le nombre d'IA qui se sont qualifiées
    public static int nbSurvivor; //Le nombre de personnes qui seront qualifié sur ce round
    public static int peopleFinish = 0; //Le nombre de personnes qui ont actuellement fini le round

    public static void Finish(GameObject obj)
    {
        if (peopleFinish < nbSurvivor)
        {
            if (obj.CompareTag("Player"))
                playerToSpawn++;
            else
                aiToSpawn++;

            peopleFinish++;
        }
    }

    public static void NewLevel()
    {
        Debug.Log("NewLevel");
        aiToSpawn = 0;
        playerToSpawn = 0;
        peopleFinish = 0;
    }   
}
