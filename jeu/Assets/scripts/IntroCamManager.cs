using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCamManager : MonoBehaviour
{
    StartManager startManager;
    public float camSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        startManager = GameObject.FindObjectOfType<StartManager>();


        if (startManager.scene == 1)
        {
            transform.position = new Vector3(12, 8, -23);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!startManager.playerCanMove)
        {
            if (startManager.scene == 1)
            {
                if (transform.position.x < 100)
                    transform.Translate(Vector3.right * Time.deltaTime * camSpeed); //Fait un traveling sur la map
                else
                {
                    startManager.playerCanMove = true; //Active les mouvements du joueur une fois terminé
                }
            }
        }
    }
}
