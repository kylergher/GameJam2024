using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlipSign : MonoBehaviour
{

    public GameObject iceSkates;
    public GameObject sign;

    public float minSpawnTime = 10f;
    public float maxSpawnTime = 27f;

    private GameObject skatesInstance;
    private bool playerInRange = false;

    private PickUpParent pickUpItemScript;
    // Start is called before the first frame update
    void Start()
    {
        iceSkates.SetActive(false);
        sign.SetActive(false);
        StartCoroutine(SpawnCollectibleAfterRandomTime());
    }

    private IEnumerator SpawnCollectibleAfterRandomTime()
    {
        float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        yield return new WaitForSeconds(spawnTime);

        iceSkates.SetActive(true);
        Debug.Log("Skates Spawned");
    }

    void Update()
    {
        if(playerInRange && skatesInstance != null && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("We want to collect!!");
            CollectItem();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("player entered range!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player exited range!");
        }
    }

    private void CollectItem()
    {
        Destroy(skatesInstance);
        Destroy(iceSkates);
        Debug.Log("Skates Destroyed");

        sign.SetActive(true);
        Debug.Log("Block Area Instantiated!");

        playerInRange = false;
    }
}
