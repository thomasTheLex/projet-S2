using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Animator playerAnim;
    private float speed = 0;
    private float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        //Double la vitesse si touche de sprint enfoncé
        if (Input.GetKey(KeyCode.LeftShift))
            speed = 1;
        else
            speed = 0.5f;

        //Active la marche arrière si S est enfoncé, désactive sinon
        if (Input.GetKey(KeyCode.S))
            playerAnim.SetBool("back_b", true);
        else
            playerAnim.SetBool("back_b", false);

        if (Input.GetAxis("Vertical") != 0) //Si S ou W enfoncé
            playerAnim.SetFloat("speed_f", speed);
        else
            playerAnim.SetFloat("speed_f", 0);

        horizontalInput = Input.GetAxis("Horizontal"); //Pour obtenir si A ou D sont pressés
        transform.Rotate(Vector3.up, Time.deltaTime * horizontalInput * 10);

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            playerAnim.SetTrigger("jump_t"); //Active le trigger de l'animation si ESPACE est pressé
        }
    }
}
