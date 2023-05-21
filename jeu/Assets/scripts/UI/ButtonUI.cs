using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    private string[] keys = new string[] { "backspace", "delete", "tab", "clear", "return", "pause", "escape", "space", "up", "down", "right", "left", "insert", "home", "end", "page up", "page down", "f1", "f2", "f3", "f4", "f5", "f6", "f7", "f8", "f9", "f10", "f11", "f12", "f13", "f14", "f15", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "!", "\"", "#", "$", "&", "'", "(", ")", "*", "+", ",", "-", ".", "/", ":", ";", "<", "=", ">", "?", "@", "[", "\\", "]", "^", "_", "`", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "numlock", "caps lock", "scroll lock", "right shift", "left shift", "right ctrl", "left ctrl", "right alt", "left alt" };
    int l = 103;
    public GameObject playerPrefab;
    public GameObject[] layer;
    public GameObject[] inputFields;

    private void Start()
    {
        SettingsManager.Initialized();
    }

    public void StartGameButton()
    {
        NextLevel.nbSurvivor = 1; //60
        NextLevel.Start(CreateStartList(1, 59));
        SceneManager.LoadScene(1);
    }

    public void TwoPlayerStartButton()
    {
        NextLevel.nbSurvivor = 2; //60

        NextLevel.Start(CreateStartList(2, 58));

        SceneManager.LoadScene(1);
    }

    private List<GameObject> CreateStartList(int nbPlayer, int nbAI)
    {
        List<GameObject> res = new List<GameObject>();
        for (int i = 0; i < nbPlayer; i++)
        {
            var tmp = Instantiate(playerPrefab);
            tmp.GetComponent<CharacterController>().playerNumber = i + 1;
            res.Add(tmp);
        }

        //Rajouter boucle pour les IA

        return res;
    }



    public void ExitButton()
    {
        Application.Quit();
    }

    public void SaveButton()
    {
        SettingsManager.SetKey();
    }

    public void ChangeMenu()
    {
        foreach(GameObject lay in layer)
        {
            lay.SetActive(!lay.activeSelf);
        }
    }

    public void InputButton()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        string res = "";
        int i = 0;
        while (i < l && res == "")
        {
            if (Input.GetKey(keys[i]))
                res = keys[i];
            i++;
        }

        if (res != "")
            SettingsManager.controlDict[name] = res;
    }
}
