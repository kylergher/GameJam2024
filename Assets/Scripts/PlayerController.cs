using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float forceMultiplier = 100f; //the force applied to player when on ice
    Rigidbody2D rb; //allows for the slippery material to be used

    private bool hasSlidePowerUp = false; //at the beginning, this tells the game that the player is not on ice skates
    public float slidePowerUpTime = 5f; //this is the length of time in which you wear the skates once you pick it up
    public float slidePowerUpMovementSpeed = 8f; //this is the increased speed in which you will go when you wear the skates

    public IngredientType heldIngredient; //this is the enumeration pulled from the 'ingreident' script

    public bool hasFishAlready = false; //bool to check if the player is holding a fish currently
    public bool canPickUpFish = true; //bool to check if the player can pick up a fish (I don't know how important this actually is)
    public SpriteRenderer holdingFish; //this is the graphic that appears overhead of the player so you can get a visual cue that they are holding a fish

    private List<Ingredient> allIngredients = new List<Ingredient>(); //,,,,,,

    public CustomerMovement customerManager; //this is giving us access to the CustomerMovement script
    public Customer patron;
    public Sprite sardinePic;
    public Sprite mackPic;
    public Sprite bigPic;

    public int gameScore ; //this is the players score
    public TextMeshProUGUI totalScore;

    public Canvas loseScreen;
    public Canvas winScreen;

    

    public MenuUI pauseMenu;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //on start we set rb to be the rigidbody of the object this script is applied to
        customerManager = FindFirstObjectByType<CustomerMovement>(); //this idk man what

        holdingFish.enabled = false; //immediately says that the player is not holding a fish

        foreach (Ingredient ingredient in FindObjectsByType<Ingredient>(FindObjectsSortMode.None)) //this has to do with lists and loops :(
        {
            allIngredients.Add(ingredient); //MAN IDK
        }

        gameScore = 100;

        loseScreen.enabled = false;
        winScreen.enabled = false;
        
    }

    void Update()
    {
        
        if (hasFishAlready == true) //bool checking if player has fish, if the answer is yes:
        {
            holdingFish.enabled = true; //shows holding fish sprite (because if you have a fish, that means you can hold it. obviously)
            canPickUpFish = false; //sets the canPickUpFish bool to false because you can only ever hold one fish at a time
        }

        //totalScore.text = "SCORE: " + gameScore.ToString();
        //displayText.text = "Counter: " + counter.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
        }

        if (hasSlidePowerUp == false) //this is the basic check to see what movement system the game is going to use. in this case, if this check is met then you slide
        {
            if (Input.GetKey(KeyCode.W))
            {
                Debug.Log("Forwards!");
                rb.AddForce(new Vector2(0f, 1f) * forceMultiplier * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                Debug.Log("Backwards!");
                rb.AddForce(new Vector2(0f, -1f) * forceMultiplier * Time.deltaTime);
            }




            if (Input.GetKey(KeyCode.A))
            {
                Debug.Log("To the left!");
                rb.AddForce(new Vector2(-1f, 0f) * forceMultiplier * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Debug.Log("To the right!");
                rb.AddForce(new Vector2(1f, 0f) * forceMultiplier * Time.deltaTime);
            }
        }

        if (hasSlidePowerUp == true) //this check, if matched, makes you not slide and changes the character sprite/animation
        {
            if (Input.GetKey(KeyCode.W))
            {
                Debug.Log("Forwards but insano style!");
                this.transform.position += new Vector3(0f, slidePowerUpMovementSpeed, 0f) * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                Debug.Log("Backwards but crazy style!");
                this.transform.position += new Vector3(0f, -slidePowerUpMovementSpeed, 0f) * Time.deltaTime;
            }




            if (Input.GetKey(KeyCode.A))
            {
                Debug.Log("Left but with style!");
                this.transform.position += new Vector3(-slidePowerUpMovementSpeed, 0f, 0f) * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Debug.Log("Right but swag as hell!");
                this.transform.position += new Vector3(slidePowerUpMovementSpeed, 0f, 0f) * Time.deltaTime;
            }
        }

        if (gameScore == 0 || gameScore <= 100 - 150)
        {
            loseScreen.enabled = true;
        }
    }

    public void PickUpSlidePowerUp() //callable function for when you pick up the ice skates
    {
        Debug.Log("I picked this up wow!"); //just a way to check the logic
        hasSlidePowerUp = true; //changes bool to reflect that you have the skates on
        rb.velocity = Vector3.zero; //changes the rigidbody velocity so you don't slide
        StartCoroutine(WaitAndDisableSlidePowerUp()); //basically a timer for how long it lasts
        //Need to disable sprite as well
    }

    public void PickUpIngredient(IngredientType ingredientType)
    {

        heldIngredient = ingredientType;
        if (hasFishAlready == true)
        {
            canPickUpFish = false;
        }
        else if (hasFishAlready == false)
        {
            canPickUpFish = true;
        }

        if(ingredientType == IngredientType.Sardine && hasFishAlready == false)
        {
            Debug.Log("Just picked up a Sardine, feeling good");
            hasFishAlready = true;
            holdingFish.enabled = true;
            holdingFish.sprite = sardinePic;
        }
        else if(ingredientType == IngredientType.Mackerel && hasFishAlready == false)
        {
            Debug.Log("Oh shoot look at this mackerel");
            hasFishAlready = true;
            holdingFish.enabled = true;
            holdingFish.sprite = mackPic;
        }
        else if(ingredientType == IngredientType.Bigboy && hasFishAlready == false)
        {
            Debug.Log("BIG BOY IN MY HANDS WOWOWOWOWOWOWOW");
            hasFishAlready = true;
            holdingFish.enabled = true;
            holdingFish.sprite = bigPic;
        }
    }

    private IEnumerator WaitAndDisableSlidePowerUp()
    {
        yield return new WaitForSeconds(slidePowerUpTime);
        hasSlidePowerUp = false;
    }

    public void DeliverIngredient()
    {
        customerManager.DeliverIngredientToCustomer(heldIngredient);
        DropIngredient();
    }

    public void TrashIngredient()
    {
        DropIngredient();
    }

    private void DropIngredient()
    {
        foreach (Ingredient ingredient in allIngredients)
        {
            if (ingredient.ingredientType == heldIngredient)
            {
                ingredient.ReenableIngredientSprite();
            }
        }

        heldIngredient = IngredientType.None;
        holdingFish.enabled = false;
        hasFishAlready = false;
       
    }

    public void ThrowAwayFish()
    {
        hasFishAlready = false;
    }

    public void AddScore(int score)
    {
        gameScore += score;
        UpdateScoreUI();
    }

    public void SubtractScore(int score)
    {
        gameScore -= score;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (totalScore != null)
        {
            totalScore.text = "SCORE: " +gameScore.ToString();
        }
    }


}
