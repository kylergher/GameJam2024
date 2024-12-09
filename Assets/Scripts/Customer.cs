using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum customerType
{
    Penguin,
    Seal,
    PolarBear
}

public class Customer : MonoBehaviour
{
    public int currentCustomerIndex = 0;
    private CustomerMovement customerManager;

    public Slider customerAngyLevel;
    public float totalTime = 30f;
    private float remainingTime;

    public float angerTimerFrontOfLine = 0f;
    public float timeToAngryLeaveFrontOfLine = 50f;

    public float angerTimerNotFrontOfLine = 0f;
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
    public PlayerController playerController;

    public Sprite[] penguinSprites;
    public Sprite[] sealSprites;
    public Sprite[] polarBearSprites;

    private SpriteRenderer currentCustomer;

    private customerType customerType;

    private bool isLeaving = false;

    void Start()
    {
        int wantAmount = Random.Range(1, 3);
        customerDemandsAmount = wantAmount;

        currentCustomer = GetComponent<SpriteRenderer>();

        customerType = (customerType)Random.Range(0, 3);

        switch (customerType)
        {
            case customerType.Penguin:
                currentCustomer.sprite = penguinSprites[0];
                break;
            case customerType.Seal:
                currentCustomer.sprite = sealSprites[0];
                break;
            case customerType.PolarBear:
                currentCustomer.sprite = polarBearSprites[0];
                break;
        }

        // Add this section to randomize the type of fish customer demands
        int randomFishType = Random.Range(0, 3);
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

        customerDemandsAmount = wantAmount;

        switch (randomFishType)
        {
            case 0:
                customerDemands = IngredientType.Sardine;
                break;
            case 1:
                customerDemands = IngredientType.Mackerel;
                break;
            case 2:
                customerDemands = IngredientType.Bigboy;
                break;
        }

        customerManager = FindFirstObjectByType<CustomerMovement>();

        remainingTime = (currentCustomerIndex == 0) ? timeToAngryLeaveFrontOfLine : timeToAngryLeaveNotFrontOfLine;
        customerAngyLevel.maxValue = remainingTime;
        customerAngyLevel.value = remainingTime;
    }


    // Update is called once per frame
    void Update()
    {
        

        if (currentCustomerIndex == 0)
        {
            angerTimerFrontOfLine += Time.deltaTime;
            remainingTime -= Time.deltaTime;
            customerAngyLevel.value = remainingTime;
            

            if (angerTimerFrontOfLine >= timeToAngryLeaveFrontOfLine / 3 && angerTimerFrontOfLine < 2 * timeToAngryLeaveFrontOfLine /3)
            {
                UpdateSprite(1);
            }
            else if(angerTimerFrontOfLine >= 2 * timeToAngryLeaveFrontOfLine / 3 && angerTimerFrontOfLine < timeToAngryLeaveFrontOfLine)
            {
                UpdateSprite(2);
            }
            else if(angerTimerFrontOfLine >= timeToAngryLeaveFrontOfLine && isLeaving == false)
            {
                GetAngryAndLeave();
            }

        }
        else
        {
            angerTimerNotFrontOfLine += Time.deltaTime;
            remainingTime += Time.deltaTime;
            customerAngyLevel.value = remainingTime;

            if (angerTimerNotFrontOfLine >= timeToAngryLeaveNotFrontOfLine / 3 && angerTimerNotFrontOfLine < 2 * timeToAngryLeaveNotFrontOfLine / 3)
            {
                UpdateSprite(1);
            }
            else if (angerTimerNotFrontOfLine >= timeToAngryLeaveNotFrontOfLine / 3 && angerTimerNotFrontOfLine < timeToAngryLeaveNotFrontOfLine)
            {
                UpdateSprite(2);
            }
            else if (angerTimerNotFrontOfLine >= timeToAngryLeaveNotFrontOfLine && isLeaving == false)
            {
                GetAngryAndLeave();
            }
        }
    }

    private void UpdateSprite(int emotionIndex)
    {
        switch(customerType)
        {
            case customerType.Penguin:
                currentCustomer.sprite = penguinSprites[emotionIndex];
                break;
            case customerType.Seal:
                currentCustomer.sprite = sealSprites[emotionIndex];
                break;
            case customerType.PolarBear:
                currentCustomer.sprite = polarBearSprites[emotionIndex];
                break;
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
                remainingTime = timeToAngryLeaveFrontOfLine;
                customerAngyLevel.value = remainingTime;
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
