using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 posOffset = new Vector3(-4, 3, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //A modifier, ne fonctionne pas
        transform.rotation = player.transform.rotation;
        transform.position = player.transform.position + posOffset;
    }
}
