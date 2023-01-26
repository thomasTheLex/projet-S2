using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{

    Animation animations;

    //stats
    public float walkSpeed;
    public float runSpeed;
    public float turnSpeed;

    //input
    public string inputFront;
    public string inputLeft;
    public string inputRight;
    public string inputBack;
    public string inputChangeView;
    public string inputChangeSideView;

    public Vector3 jumpheigt;
    CapsuleCollider playerCollider;

    //caméra
    public GameObject FPS_view;
    public GameObject TPS_view_L;
    public GameObject TPS_view_R;
    public int cam = 0;
    public int side = 0;



    void Start()
    {
        animations = gameObject.GetComponent<Animation>();
        playerCollider = gameObject.GetComponent<CapsuleCollider>();
    }

    bool IsGronded()
    {
        Vector3 dwn = transform.TransformDirection(Vector3.down);

        return (Physics.Raycast(transform.position, dwn, 0.3f));
    }

        void Update()
    {
        //avance
        if (Input.GetKey(inputFront))
        {
            transform.Translate(0, 0, walkSpeed* Time.deltaTime);
        }

        //recule
        if (Input.GetKey(inputBack))
        {
            transform.Translate(0, 0, -(walkSpeed/2) * Time.deltaTime);
        }
        //gauche
        if (Input.GetKey(inputLeft))
        {
            transform.Rotate(0, -turnSpeed*Time.deltaTime, 0);

        }
        //droite
        if (Input.GetKey(inputRight))
        {
            transform.Rotate(0,turnSpeed*Time.deltaTime,0);
        }
        if (Input.GetKeyDown(inputChangeView))
        {
            if (cam==0)
            {
                FPS_view.SetActive(false);
                if (side==0)
                {
                    TPS_view_R.SetActive(true);
                }
                else
                {
                    TPS_view_L.SetActive(true);
                }
               
                cam = 1;
            }
            else
            {
                FPS_view.SetActive(true);
                TPS_view_R.SetActive(false);
                TPS_view_L.SetActive(false);
                cam = 0;
            }
        }
        if (Input.GetKeyDown(inputChangeSideView))
        {
            if (TPS_view_R.active) 
            {
                TPS_view_R.SetActive(false);
                TPS_view_L.SetActive(true);
                side = 1;
            }
            else
            {
                TPS_view_R.SetActive(true);
                TPS_view_L.SetActive(false);
                side = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGronded())
        {
            Debug.Log(IsGronded());
            Vector3 v = gameObject.GetComponent<Rigidbody>().velocity;
            v.y = jumpheigt.y;

            gameObject.GetComponent<Rigidbody>().velocity = jumpheigt;
        }

    }
}
