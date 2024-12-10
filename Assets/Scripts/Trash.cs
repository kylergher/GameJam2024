using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    protected PlayerController player;
    protected SpriteRenderer interactPopup;
    protected bool isPlayerInRange = false;

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        interactPopup = GameObject.FindGameObjectWithTag("InteractPopup").GetComponent<SpriteRenderer>();

        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && player.hasFishAlready == true)
        {
            Debug.Log("Ooooo you want to throw something away so badddddd ooooooooo");
            if (Input.GetKeyDown(KeyCode.E))
            {
                getHimOut();
            }
        }
    }

    public void getHimOut()
    {
        player.TrashIngredient();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        interactPopup.enabled = true;
        interactPopup.transform.position = this.transform.position + new Vector3(0f, 1.25f, 0f);
        isPlayerInRange = true;
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        //interactPopup.enabled = false;
        isPlayerInRange = false;
        interactPopup.enabled = false;
    }
}
