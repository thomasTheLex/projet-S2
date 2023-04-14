using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public static Dictionary<string, string> controlDict = new Dictionary<string, string>(12);
    private static GameObject[] buttons;

    public static void Initialized() //Lancer au start
    {
        buttons = FindObjectOfType<ButtonUI>().inputFields;
        foreach (var b in buttons) //Pour chaque bouton
        {
            string txt = PlayerPrefs.GetString(b.name, b.GetComponentInChildren<Text>().text); //Regarde si le fichier existe, sinon prends un paprametre par défaut
            controlDict.Add(b.name, txt); //L'ajoute au dictionnaire
        }
    }

    public static void SetKey()
    {
        foreach(KeyValuePair<string, string> entry in controlDict) //Pour chaque entrée dans la dictionnaire
        {
            PlayerPrefs.SetString(entry.Key, entry.Value); //Mets à jour la valeur dans le fichier
        }
        PlayerPrefs.Save(); //Sauvegarde les modifications
    }
}
