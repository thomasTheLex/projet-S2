using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public static GameObject[] ToSpawn = new GameObject[60]; //La liste contenant tout les personnages à faire spawn au prochain round
    public static int nbSurvivor; //Le nombre de personnes qui seront qualifié sur ce round
    public static int peopleFinish = 0; //Le nombre de personnes qui ont actuellement fini le round

    public static void Finish(GameObject obj)
    {
        if (peopleFinish < nbSurvivor)
        {
            DontDestroyOnLoad(obj);
            ToSpawn[peopleFinish] = obj;
            obj.SetActive(false);
            peopleFinish++;
        }
    }

    public static void NewLevel()
    {
        ToSpawn = new GameObject[nbSurvivor];
        peopleFinish = 0;
    }   
}
