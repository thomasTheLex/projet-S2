using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Animator playerAnim;
    private Rigidbody playerRb;
    private float speed = 0;
    private float horizontalInput;
    public float turnSpeed = 10.0f;
    public float jumpForce = 1.00f;
    private bool inAir = false;
    public Vector3 checkPoint;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        checkPoint = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Double la vitesse si touche de sprint enfonce
        if (Input.GetKey(KeyCode.LeftShift))
            speed = 1;
        else
            speed = 0.5f;

        //Active la marche arriere si S est enfonce, desactive sinon
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            playerAnim.SetBool("back_b", true);
        else
            playerAnim.SetBool("back_b", false);

        if (Input.GetAxis("Vertical") != 0) //Si S ou W enfonce
            playerAnim.SetFloat("speed_f", speed);
        else
            playerAnim.SetFloat("speed_f", 0);

        horizontalInput = Input.GetAxis("Horizontal"); //Pour obtenir si A ou D sont presses
        transform.Rotate(Vector3.up, Time.deltaTime * horizontalInput * turnSpeed);

        if (Input.GetKeyDown(KeyCode.Space) && !inAir)
        {
            playerAnim.SetTrigger("jump_t"); //Active le trigger de l'animation si ESPACE est presse
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            inAir = true;
        }

        if (transform.position.y < -10) //Si on tombe, on retourne au checkpoint
        {
            transform.position = checkPoint;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
            {
                checkPoint = other.gameObject.transform.position;
            }

        if (other.gameObject.CompareTag("Finish"))
            {
                //Gérer la fin de niveau ici
            }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            inAir = false;
    }
}
