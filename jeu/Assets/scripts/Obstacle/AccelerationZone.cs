using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationZone : MonoBehaviour
{
    public int Acceleration = 3;
    private void OnCollisionStay(Collision collision)
    {
        collision.transform.Translate(new Vector3(0,0,Acceleration) * Time.deltaTime);
    }
}
