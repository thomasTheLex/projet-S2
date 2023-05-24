using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public int xMove;
    public int yMove;
    public int zMove;
    public float speed = 5;
    private Vector3 translation;
    private Vector3 startPos;
    private bool _switch = false;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position; //Position de depart
        translation = new Vector3(xMove, yMove, zMove) + startPos; //Position d'arrivee
    }

    // Update is called once per frame
    void FixedUpdate() //FixedUpdate permet d'effectuer toujours dans le meme temps et d'eviter des problemes de collision
    {
        if (_switch) //En fonction de _switch
            transform.position = Vector3.MoveTowards(transform.position, startPos, speed * Time.deltaTime); //On bouge vers la position de depart
        else
            transform.position = Vector3.MoveTowards(transform.position, translation, speed * Time.deltaTime); //Ou vers celle arriver

        if (transform.position == (startPos) || transform.position == translation) //Quand on arrive a une des deux
            _switch = !_switch;    //On modifie _switch pour aller vers l'autre position
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
            DontDestroyOnLoad(other);
        }
    }
}
