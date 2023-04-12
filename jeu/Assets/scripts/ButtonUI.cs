using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject iaPrefab;
    public void StartGameButton()
    {
        NextLevel.ToSpawn[0] = playerPrefab;
        //for(int i = 1, i < 60, i++)
        {
            //NextLevel.ToSpawn[i] == iaPrefab;
        }
        NextLevel.nbSurvivor = 1;
        SceneManager.LoadScene("map1");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
