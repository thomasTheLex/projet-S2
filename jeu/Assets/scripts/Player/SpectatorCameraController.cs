using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpectatorCameraController : MonoBehaviour
{
    public int playerNumber = 1;
    public int defaultSpeed = 20;
    private int speed;
    private Dictionary<string, string> control;
    private Text txt;
    // Start is called before the first frame update
    void Awake()
    {
        control = SettingsManager.controlDict;
        txt = gameObject.GetComponentInChildren<Text>();

        switch (StartManager.scene)
        {
            case 1:
                transform.position = new Vector3(100, 80, 0);
                transform.eulerAngles = new Vector3(90, 0, 0);
                break;

            case 2:
                transform.position = new Vector3(110,110,9);
                transform.eulerAngles = new Vector3(90, 0, 0);  
                break;

            case 3:
                transform.position = new Vector3(100,75,0);
                transform.eulerAngles = new Vector3(90, 0, 0);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(control["Sprint" + playerNumber]))
            speed = defaultSpeed * 2;
        else
            speed = defaultSpeed;

        if (Input.GetKey(control["Forward" + playerNumber]))
            transform.Translate(new Vector3(0,1,0) * speed * Time.deltaTime);

        if (Input.GetKey(control["Backward" + playerNumber]))
            transform.Translate(new Vector3(0,-1,0) * speed * Time.deltaTime);

        if (Input.GetKey(control["Left" + playerNumber]))
            transform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime);
        if (Input.GetKey(control["Right" + playerNumber]))
            transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);

        txt.text = $"{NextLevel.peopleFinish}/{NextLevel.nbSurvivor}\nPeoples qualifie";

    }
}
