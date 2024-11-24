using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpParent : MonoBehaviour
{
    protected PlayerController player;
    protected SpriteRenderer interactPopup;
    protected bool isPlayerInRange = false;

    protected SpriteRenderer trashCan;

    // Start is called before the first frame update
    protected void Start()
    {
        interactPopup = GameObject.FindGameObjectWithTag("InteractPopup").GetComponent<SpriteRenderer>();
        
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(isPlayerInRange && player.hasFishAlready == false)
        {
            Debug.Log("He's in range, I repeat: He is in range!!!");
            if(Input.GetKeyDown(KeyCode.E))
            {
                PickUpPowerUp();
            }
        }

       

    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        interactPopup.enabled = true;
        interactPopup.transform.position = this.transform.position + new Vector3(0f, 1.25f, 0f);
        isPlayerInRange = true;
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        interactPopup.enabled = false;
        isPlayerInRange = false;
    }

    protected virtual void PickUpPowerUp()
    {
        interactPopup.enabled = false;
        interactPopup.GetComponent<SpriteRenderer>().enabled = false;
        //interactPopup.GetComponent<BoxCollider2D>().enabled = false;
        //Destroy(this.gameObject);
    }

    
    protected virtual void TossEm()
    {
        //if (isPlayerInRange == true && GameObject.FindGameObjectWithTag )
        {

        }
    }
}
