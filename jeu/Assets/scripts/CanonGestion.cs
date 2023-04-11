using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonGestion : MonoBehaviour
{
    private int minCooldown = 2;
    private int maxCooldown = 8;
    public float cooldown = 5f;

    public Vector3 decal = new Vector3(0,0,0);
    public int power = 100;
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
            cooldown = Random.Range(minCooldown, maxCooldown);
            Vector3 toSpawn = this.transform.position + decal;

            GameObject bouletSpawn = Instantiate(bouletPrefab, toSpawn, transform.rotation);
            Rigidbody bouletSpawnrb = bouletSpawn.GetComponent<Rigidbody>();

            bouletSpawnrb.AddRelativeForce(Vector3.left * power, ForceMode.Impulse);
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
    }
}
