using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public int currentCustomerIndex = 0;
    private CustomerMovement customerManager;

    private float angerTimerFrontOfLine = 0f;
    public float timeToAngryLeaveFrontOfLine = 50f;

    private float angerTimerNotFrontOfLine = 0f;
    public float timeToAngryLeaveNotFrontOfLine = 75f;

    public IngredientType customerDemands;
    public int customerDemandsAmount = 1;
    private int numIngredientsDelivered = 0;
    public Sprite customerDemandSprite;
    public Sprite mackSprite;
    public Sprite sardineSprite;
    public Sprite BIGGUYSprite;

    private SpriteRenderer deliveryPopup;

    public float wrongDeliveryTimePenalty = 5f;

    private bool isLeaving = false;
    // Start is called before the first frame update
    void Start()
    {
        int wantAmount = Random.Range(1, 3);
        customerDemandsAmount = wantAmount;

        // Add this section to randomize the type of fish customer demands
        int randomFishType = Random.Range(0, 2);
        switch (randomFishType)
        {
            case 0:
                customerDemands = IngredientType.Sardine;
                customerDemandSprite = sardineSprite;
                break;
            case 1:
                customerDemands = IngredientType.Mackerel;
                customerDemandSprite = mackSprite;
                break;

            case 2:
                customerDemands = IngredientType.Bigboy;
                customerDemandSprite = BIGGUYSprite;
                break;
        }

        customerManager = FindFirstObjectByType<CustomerMovement>();
    }


    // Update is called once per frame
    void Update()
    {
        if(currentCustomerIndex == 0)
        {
            angerTimerFrontOfLine += Time.deltaTime;

            if(angerTimerFrontOfLine >= timeToAngryLeaveFrontOfLine && isLeaving == false)
            {
                GetAngryAndLeave();
            }


        }
        else
        {
            angerTimerNotFrontOfLine += Time.deltaTime;

            if (angerTimerNotFrontOfLine >= timeToAngryLeaveNotFrontOfLine && isLeaving == false)
            {
                GetAngryAndLeave();
            }
        }
    }

    public void SetCustomerIndex(int index)
    {
        currentCustomerIndex = index;
    }

    private void GetAngryAndLeave()
    {
        isLeaving = true;
        customerManager.CustomerAngryLeaves(currentCustomerIndex);
    }

    public void DeliverToCustomer(IngredientType ingredientType)
    {
        if(ingredientType == customerDemands)
        {
            numIngredientsDelivered++;

            if(numIngredientsDelivered == customerDemandsAmount)
            {
                LeaveHappy();
            }
            else
            {
                angerTimerFrontOfLine = 0f;
                //This is where we can randomize the next ingredient wanted
            }
        }
        else
        {
            angerTimerFrontOfLine += wrongDeliveryTimePenalty;
        }
    }

    private void LeaveHappy()
    {
        customerManager.DeliverToCustomerSuccess();
    }

    public int GetCustomerDemandsAmount()
    {
        return customerDemandsAmount - numIngredientsDelivered;
    }
}
