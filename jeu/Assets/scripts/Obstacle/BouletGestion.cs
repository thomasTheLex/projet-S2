using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouletGestion : MonoBehaviour
{
    public int maxPower;
    public int minPower;
    public ParticleSystem explosionParticle;
    private float timer = 8;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime; //réduction du timer

        if (timer < 0 || transform.position.y < -10)
            Destroy(this.gameObject); //On fait despawn le boulet si son timer passe à 0 ou si il tombe dans le vide
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var explosion = Instantiate(explosionParticle);
            explosion.transform.position = collision.transform.position;
            explosion.transform.parent = null;
            Destroy(this.gameObject);
        }
    }
}
