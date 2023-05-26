using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class IAController : MonoBehaviour, ICharacter
{
    public bool condemned = false;
    
    public int HadToGo = 0;
    public NavMeshAgent agent;
    public IOrderedEnumerable<GameObject> destinations;
    public Vector3 checkPoint;
    private int checkPointHadToGo;
    private ParticleSystem checkpointParticle;
    public GameObject[] skins;
    public bool _haveFinish = true;
    public bool HaveFinish { get => _haveFinish; set => _haveFinish = value; }
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        checkpointParticle = GetComponentInChildren<ParticleSystem>();
        var tmp = Instantiate(skins[Random.Range(0, skins.Length - 1)], transform.position, transform.rotation);

        tmp.transform.parent = transform;
        anim = GetComponent<Animator>();

    }
    public void Init() //Exécuté à chaque changement de map
    {
        checkPoint = new Vector3(1, 2, 1);
        checkPointHadToGo = 0;
        GameObject[] FindDestinations= GameObject.FindGameObjectsWithTag("chemin");
        destinations = FindDestinations.OrderBy(gameobject => gameobject.name);
        StartCoroutine(WaitForStart());
    }

    private IEnumerator WaitForStart()
    {
        yield return new WaitUntil(() => StartManager.playerCanMove && !HaveFinish);
        gameObject.SetActive(false);
        anim.SetBool("run_b", true);
        gameObject.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        if (StartManager.playerCanMove && !HaveFinish)
        {
            anim.SetBool("run_b", true);
            if (!condemned)
            {
                agent.SetDestination(destinations.ElementAt(HadToGo).transform.position);
            }
            else
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
                StartCoroutine(Waiter());
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }


            if (transform.position.y < -10) //Si on tombe, on retourne au checkpoint
            {
                Respawn();
            }

            if (HadToGo >= destinations.Count())
            {
                HadToGo = 0;
            }
        }
    }


    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(1);
        Respawn();
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("chemin"))
        {
            if (!condemned)
            {
                if (destinations.ElementAt(HadToGo).gameObject.transform.position == other.gameObject.transform.position) // on vérifie si on est bien sûr le bon chemin
                {
                    HadToGo += 1;
                }
                else
                {
                    for (int i = 0; i < destinations.Count(); i++)
                    {
                        if (other.gameObject.transform.position == destinations.ElementAt(i).transform.position)
                        {
                            HadToGo = i+1;
                        }
                    }
                }
                if (HadToGo == destinations.Count())
                {
                    HaveFinish = true;
                    StartCoroutine(EndAnim());
                    
                }
            }
        }
        
        
        else if (other.gameObject.CompareTag("challenge"))
        {
            int rng = Random.Range(0, (4 + NextLevel.peopleFinish + StartManager.scene));
            if (rng < 2)
            {
                condemned = true;

            }
        }
        else if (other.gameObject.CompareTag("Checkpoint"))
        {
            checkPoint = other.gameObject.transform.position;
            checkPointHadToGo = HadToGo;
            checkpointParticle.Play();
        }

        else if (other.gameObject.CompareTag("Boulet"))
        {
            condemned = true;
        }
        else if (other.gameObject.CompareTag("BouletIA"))
        {
            condemned = true;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boulet")) //Respawn si on se prend un boulet
            condemned = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            anim.SetTrigger("jump_t");
    }


    private void Respawn()
    {
        gameObject.SetActive(false);
        HadToGo = checkPointHadToGo;
        gameObject.transform.position = checkPoint;
        condemned = false;
        gameObject.SetActive(true);
    }

    private IEnumerator EndAnim()
    {
        NextLevel.Finish();
        anim.SetBool("run_b", false);
        anim.SetTrigger("dance_t");
        yield return new WaitForSeconds(4);
        gameObject.SetActive(false);
        HadToGo = 0; 
    }
}