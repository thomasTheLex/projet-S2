using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLevel : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject introCamPrefab;
    public int nbIA = 0;
    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnLevelWasLoaded(int level)
    {
        Instantiate(playerPrefab, new Vector3(0, 0, 0), playerPrefab.transform.rotation);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject playerCam = GameObject.FindGameObjectWithTag("MainCamera");
        playerCam.SetActive(false);
        


        for (int i = 0; i < nbIA; i++) //Boucle pour faire spawn les ia sur la map
        {

        }

        if (level == 0) //Map 1
        {
            Instantiate(introCamPrefab, new Vector3(4, 8, -23), introCamPrefab.transform.rotation);
            GameObject introCam = GameObject.FindGameObjectWithTag("IntroCam");
            Vector3 movement = new Vector3(speed, 0, 0);
            
            while (introCam.transform.position.x < 100)
            {
                introCam.transform.Translate(movement * Time.deltaTime);
            }

            playerCam.SetActive(true);
            introCam.SetActive(false);
        }
    }
}
