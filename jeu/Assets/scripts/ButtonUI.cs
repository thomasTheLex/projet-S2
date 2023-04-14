using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject[] layer;
    public GameObject[] inputFields;
    public void StartGameButton()
    {
        NextLevel.nbSurvivor = 1;
        NextLevel.Finish(Instantiate(playerPrefab));
        SceneManager.LoadScene("map1");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void SaveButton()
    {
        
    }

    public void LoadButton()
    {

    }

    public void ChangeMenu()
    {
        foreach(GameObject lay in layer)
        {
            lay.SetActive(!lay.activeSelf);
        }
    }

    public void RealInput(string input)
    {
        string i = Input.inputString;
    }


}
