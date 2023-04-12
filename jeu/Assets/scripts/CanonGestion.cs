using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonGestion : MonoBehaviour
{
    private int minCooldown = 2;
    private int maxCooldown = 8;
    public float cooldown = 8f;

    public Vector3 decal = new Vector3(0,0,0);
    public int minPower = 100;
    public int maxPower = 200;
    public int speed = 5;
    public GameObject bouletPrefab;


    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown < 0)
        {
            cooldown = Random.Range(minCooldown, maxCooldown); //Reset du cooldown
            Vector3 toSpawn = this.transform.position + decal; //On choisit la position où va spawn le boulet

            GameObject bouletSpawn = Instantiate(bouletPrefab, toSpawn, transform.rotation); //On le fait spawn tout en le mettant dans une variable
            Rigidbody bouletSpawnrb = bouletSpawn.GetComponent<Rigidbody>(); //On récupère son rigidbody

            bouletSpawnrb.AddRelativeForce(Vector3.left * Random.Range(minPower, maxPower), ForceMode.Impulse); //Pour lui appliquer une force dès le spawn
        }
        else
        {
            cooldown -= Time.deltaTime; //Reduction du cooldown
        }
    }
}
