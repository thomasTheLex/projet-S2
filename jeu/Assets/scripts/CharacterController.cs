using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float rotationCam = 0;
    private Animator playerAnim;
    private Rigidbody playerRb;
    private Camera playerCam;
    private float speed = 0;
    private float horizontalInput;
    public int camRotationSpeed = 5;
    public float turnSpeed = 10.0f;
    public float jumpForce = 1.00f;
    private bool inAir = false;
    public Vector3 checkPoint;

    // Start is called before the first frame update
    void Start()
    {
        playerCam = GetComponentInChildren<Camera>();
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();

        checkPoint = new Vector3(1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (StartManager.playerCanMove) //Permet d'activer le controle du joueur lorsque la cinématique d'intro est fini
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
                Respawn();
            }

            if (StartManager.scene != 1)
                CalculRotation();
        }
        else
        {
            playerAnim.SetFloat("speed_f", 0);
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
            StartManager.playerCanMove = false; //On désactive les mouvements du joueur
            playerAnim.SetTrigger("dance_t"); //Lancement de la danse de fin
            NextLevel.Finish(this.gameObject);
            //Gérer la fin de niveau ici
            }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Pente"))
            inAir = false;

        else if (collision.gameObject.CompareTag("Boulet")) //Respawn si on se prend un boulet
            Respawn();
    }

    private void Respawn()
    {
        transform.position = checkPoint;
        //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }


    private void CalculRotation()
    {
        Ray ray = new Ray();
        RaycastHit hit;
        Vector3 axis;
        float angle;

        ray.origin = transform.position;
        ray.direction = -Vector3.up;

        if(Physics.Raycast(ray, out hit)) //Un rayon qui va toucher un objet vers le bas (le sol)
        {
            if (hit.transform.rotation.eulerAngles == Vector3.zero)
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            else
            {
                axis = Vector3.Cross(-transform.up, -hit.normal);
                if (axis != Vector3.zero)
                {
                    angle = Mathf.Atan2(Vector3.Magnitude(axis), Vector3.Dot(-transform.up, -hit.normal)); //Calcul de l'angle
                    transform.RotateAround(axis, angle); //On applique la rotation
                }
            }
        }
        
    }
}
