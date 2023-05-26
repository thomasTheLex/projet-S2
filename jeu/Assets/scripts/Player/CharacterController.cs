using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour, ICharacter
{
    private Dictionary<string, string> control;
    public GameObject specCamera;
    public int playerNumber = 1;
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
    private ParticleSystem checkpointParticle;
    private bool _haveFinish = true;
    private Text endText;
    private AudioSource audioSource;
    public AudioClip[] soundEffects;

    public bool HaveFinish { get => _haveFinish; set => _haveFinish = value; }  

    // Start is called before the first frame update
    void Start()
    {
        control = SettingsManager.controlDict;
        playerCam = GetComponentInChildren<Camera>();
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        checkpointParticle = GetComponentInChildren<ParticleSystem>();
        endText = GetComponentInChildren<Text>();
        endText.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();

        checkPoint = new Vector3(1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (StartManager.playerCanMove && !HaveFinish) //Permet d'activer le controle du joueur lorsque la cinématique d'intro est fini
        {
           /* if (Input.GetKeyDown(KeyCode.M))
                transform.position = new Vector3(100, 0, -3);*/


            //Double la vitesse si touche de sprint enfonce
            if (Input.GetKey(control["Sprint" + playerNumber]))
                speed = 1;
            else
                speed = 0.5f;

            

            //Active la marche arriere si S est enfonce, desactive sinon
            if (Input.GetKey(control["Backward" + playerNumber]))
                playerAnim.SetBool("back_b", true);
            else
                playerAnim.SetBool("back_b", false);

            if (Input.GetKey(control["Forward" + playerNumber])) //Si touche pour avancer
                playerAnim.SetFloat("speed_f", speed);
            else
                playerAnim.SetFloat("speed_f", 0);

            horizontalInput = 0;
            if (Input.GetKey(control["Left" + playerNumber]))
                horizontalInput -= 1;
            if (Input.GetKey(control["Right" + playerNumber]))
                horizontalInput += 1;
            transform.Rotate(Vector3.up, Time.deltaTime * horizontalInput * turnSpeed);

            if (Input.GetKeyDown(control["Jump" + playerNumber]) && !inAir)
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
            endText.gameObject.SetActive(false);
            playerAnim.SetFloat("speed_f", 0);
            if (!StartManager.playerCanMove)
                playerAnim.ResetTrigger("dance_t");
        }

        if (StartManager.scene == 0)
            Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
            {
                checkPoint = other.gameObject.transform.position;
                PlaySound(0);
                checkpointParticle.Play();
            }

        if (other.gameObject.CompareTag("Finish") && !HaveFinish)
            {
            checkPoint = new Vector3(0, 0, 0);
            _haveFinish = true; //On désactive les mouvements du joueur
            endText.text = "Victory !";
            endText.gameObject.SetActive(true);
            NextLevel.Finish();
            playerAnim.SetTrigger("dance_t");
            StartCoroutine(Wait4());
            //Gérer la fin de niveau ici
            }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Pente"))
            inAir = false;

        else if (collision.gameObject.CompareTag("Boulet")) //Respawn si on se prend un boulet
        {
            PlaySound(1);
            Respawn();
        }
            
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

    private IEnumerator Wait4()
    {
        yield return new WaitForSeconds(4); //On attend le temps de l'animation
        SetSpectator(); //On active la camera spectateur
        endText.gameObject.SetActive(false);
        this.gameObject.SetActive(false); //Desactivation du sprite du joueur
    }

    private void SetSpectator()
    {
        GameObject spec = Instantiate(specCamera);
        spec.transform.parent = null;
        spec.GetComponent<Camera>().rect = playerCam.rect;
        spec.GetComponent<SpectatorCameraController>().playerNumber = playerNumber;
    }

    public void Defeat()
    {
        endText.text = "Defeat...";
        endText.gameObject.SetActive(true);
    }

    private void PlaySound(int soundToPlay)
    {
        audioSource.clip = soundEffects[soundToPlay];
        audioSource.Play();
    }
}
