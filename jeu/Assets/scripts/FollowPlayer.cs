using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 posOffset = new Vector3(0, 3, 0);

    // Start is called before the first frame update
    void Start()
    {
        transform.position += posOffset; //Permet à la caméra d'être bien placé
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    }
}
