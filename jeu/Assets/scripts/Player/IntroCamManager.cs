using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCamManager : MonoBehaviour
{
    private StartManager startManager;
    public float camSpeed = 10;
    private Vector3 travelingVector;
    private Camera[] cameras;
    private Camera introCam;

    // Start is called before the first frame update
    void Awake()
    {
        cameras = Camera.allCameras;
        //introCam = GetComponent<Camera>();

        if (StartManager.scene == 1)
        {
            transform.position = new Vector3(12, 8, -23);
            transform.eulerAngles = new Vector3(0, 0, 0);
            travelingVector = new Vector3(100, 8, -23);
        }
        else if (StartManager.scene == 2)
        {
            transform.position = new Vector3(-22, 12, 0);
            transform.eulerAngles = new Vector3(0, 90, 0);
            travelingVector = new Vector3(109, 54, 0);
        }
        else
        {
            transform.position = new Vector3(23,20,-44);
            transform.eulerAngles = new Vector3(25, 0, 0);
            travelingVector = new Vector3(126, 20, -60);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!StartManager.playerCanMove)
        {
            Traveling(travelingVector);
        }
    }

    private void Traveling(Vector3 goTo)
    {
        if (transform.position != goTo)
            transform.position = Vector3.MoveTowards(transform.position, goTo, camSpeed*Time.deltaTime);
        else
        {
            StartManager.playerCanMove = true;
            Destroy(gameObject);
        }
            
    }
}
