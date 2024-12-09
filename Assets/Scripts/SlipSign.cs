using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlipSign : MonoBehaviour
{
    public GameObject sign;
    private SkatesManager SkatesManager;
    private bool playerInRange = false; //checks if the player is within range
    private PlayerController playerController;

    void Start()
    {
        SkatesManager = FindAnyObjectByType<SkatesManager>();
        playerController = FindAnyObjectByType<PlayerController>();
    }

    void Update()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            CollectSkates();
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

    public void CollectSkates()
    {
        gameObject.SetActive(false);
        sign.SetActive(true);
        Debug.Log("Man I jus twant this to work");
        SkatesManager.StartHideSignCoroutine();

        playerController.hasSlidePowerUp = true;
        playerController.StartCoroutine(playerController.WaitAndDisableSlidePowerUp());
    }

    
}
