using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeliveryArea : MonoBehaviour
{
    protected PlayerController player;
    protected GameObject deliverPopup;
    public SpriteRenderer fishSprite;
    public TextMeshPro deliveryNumText;

    public Sprite mackSprite;
    public Sprite sardineSprite;
    public Sprite BIGGUYSprite;

    private CustomerMovement customerManager;

    protected bool isPlayerInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        customerManager = FindFirstObjectByType<CustomerMovement>();
        deliverPopup = GameObject.FindGameObjectWithTag("DeliverPopup");
        deliverPopup.SetActive(false);

        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && player.heldIngredient != IngredientType.None)
        {
            Debug.Log("Ooooo you want to DELIVER something away so badddddd ooooooooo");
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.DeliverIngredient();

                deliverPopup.SetActive(false);
            }
        }
        if (isPlayerInRange == true)
        {
            IngredientType customerWants = customerManager.GetCurrentCustomerIngredientType();
            if (customerWants == IngredientType.Sardine)
            {
                fishSprite.sprite = sardineSprite;
            }
            else if (customerWants == IngredientType.Mackerel)
            {
                fishSprite.sprite = mackSprite;
            }
            else if (customerWants == IngredientType.Bigboy)
            {
                fishSprite.sprite = BIGGUYSprite;
            }


            deliveryNumText.text = "x" + customerManager.GetCurrentCustomerIngredientAmount();
        }

    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player")
        {
            return;
        }

        if (customerManager.IsFirstCustomerInPosition() == true)
        {
            deliverPopup.SetActive(true);
        }

        /* IngredientType customerWants = customerManager.GetCurrentCustomerIngredientType();
        if (customerWants == IngredientType.Sardine)
        {
            fishSprite.sprite = sardineSprite;  
        }
        else if (customerWants == IngredientType.Mackerel)
        {
            fishSprite.sprite = mackSprite;
        }
        else if (customerWants == IngredientType.Bigboy)
        {
            fishSprite.sprite = BIGGUYSprite;
        }*/

        deliveryNumText.text = "x" + customerManager.GetCurrentCustomerIngredientAmount();

        isPlayerInRange = true;
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            return;
        }
        //interactPopup.enabled = false;
        isPlayerInRange = false;
        if (deliverPopup != null)
        {
            deliverPopup.SetActive(false);
        }
          
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            return;
        }

        IngredientType customerWants = customerManager.GetCurrentCustomerIngredientType();

        if(customerWants != IngredientType.None)
        {
            if(customerManager.IsFirstCustomerInPosition() == true)
            {
                deliverPopup.SetActive(true);
            }
        }
        else
        {
            deliverPopup.SetActive(false);
        }

        deliveryNumText.text = "x" + customerManager.GetCurrentCustomerIngredientAmount();

    }
}
