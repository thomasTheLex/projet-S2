using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouletGestion : MonoBehaviour
{
    private float timer = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;

        if (timer < 0 || transform.position.y < -10)
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
