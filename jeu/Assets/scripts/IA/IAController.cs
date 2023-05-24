using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class IAController : MonoBehaviour
{
    public bool condemned = false;
    
    public int HadToGo = 0;
    public NavMeshAgent agent;
    public IOrderedEnumerable<GameObject> destinations;
    public Vector3 checkPoint;

    // Start is called before the first frame update
    void Start()
    {
        checkPoint = new Vector3(1, 2, 1);
        GameObject[] FindDestinations= GameObject.FindGameObjectsWithTag("chemin");
        destinations = FindDestinations.OrderBy(gameobject => gameobject.name);
    }


    // Update is called once per frame
    void Update()
    {
       
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
                if (destinations.ElementAt(HadToGo).gameObject.transform.position == other.gameObject.transform.position) // on v�rifie si on est bien s�r le bon chemin
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
                    Destroy(gameObject);
                }
            }
        }
        
        
        else if (other.gameObject.CompareTag("challenge"))
        {
            int rng = Random.Range(0, 4);
            if (rng == 2)
            {
                condemned = true;

            }
        }
        else if (other.gameObject.CompareTag("Checkpoint"))
        {
            checkPoint = other.gameObject.transform.position;
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


    private void Respawn()
    {
        gameObject.SetActive(false);
        gameObject.transform.position = checkPoint;
        condemned = false;
        gameObject.SetActive(true);
    }

}