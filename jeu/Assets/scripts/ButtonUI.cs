using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    public void StartGameButton()
    {
        NextLevel.nbSurvivor = 1;
        NextLevel.playerToSpawn = 1;
        //NextLevel.ToSpawn[0] = playerPrefab;
        SceneManager.LoadScene("map1");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
