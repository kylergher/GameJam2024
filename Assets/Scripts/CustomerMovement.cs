using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMovement : MonoBehaviour
{

    [SerializeField] protected Transform[] waypoints;
    [SerializeField] protected float moveSpeed = 2f;
    protected int waypointIndex = 0;

    public List<GameObject> customers = new List<GameObject>();
    public List<Vector2> customerPosition = new List<Vector2>();

    public int dailyCustomer = 3;
    private int currentDayCustomerCount = 0;
    private int currentDayCustomersFinished = 0;

    public List<GameObject> customerPrefabs = new List<GameObject>();

    public int currentLevelIndex = 0;

    public Transform customerSpawnPosition; //empty object off screen so they can in the door.

    public List<GameObject> leavingCustomers = new List<GameObject>();

    public Transform customerExitPosition;

    private float spawnTimer;

    public float spawnTimeIntervalMin = 15f;
    public float spawnTimeIntervalMax = 30f;

    public bool isTutorialLevel = false;

    private bool isFirstCustomerInPosition = false;
    public PlayerController playerController;

    public int angyBois; 

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if(isTutorialLevel == false)
        {
            StartCoroutine(SpawnCustomerAfterTime(0f));
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {

        if(Input.GetKeyDown(KeyCode.I))
        {
            AddCustomer();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            DeliverToCustomerSuccess();
        }

        Move();

        if(isFirstCustomerInPosition == false)
        {
            if(customers.Count > 0)
            {
                if (Vector3.Distance(customers[0].transform.position, waypoints[0].position) < .05f)
                {
                    isFirstCustomerInPosition = true;
                }
                        
            }
        }
    }

    protected virtual void Move()
    {
        for (int i = 0; i < customers.Count; i++)
        {
            if (customers[i] != null)
            {
                /*if (waypoints[i] != null)
                {
                    //customers[i].transform.position = customerPosition[i];
                    customers[i].transform.position = Vector2.MoveTowards(customers[i].transform.position, waypoints[i].position, moveSpeed * Time.deltaTime);
                }*/
                if (waypoints.Length > i && waypoints[i] != null)
                {
                    customers[i].transform.position = Vector2.MoveTowards(customers[i].transform.position, waypoints[i].position, moveSpeed * Time.deltaTime);
                }
            }
        }

        for (int i = 0; i < leavingCustomers.Count; i++)
        {
            leavingCustomers[i].transform.position = Vector2.MoveTowards(leavingCustomers[i].transform.position, customerExitPosition.position, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(leavingCustomers[i].transform.position, customerExitPosition.position) < .2f)
            {
                Destroy(leavingCustomers[i]);
                leavingCustomers.RemoveAt(i);
            }
        }
        
        /* if (waypointIndex <= waypoints.Length - 1)
         {
             transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);

             if (transform.position == waypoints[waypointIndex].transform.position)
             {
                 waypointIndex += 1;
             }
         }*/

    }

    private void AddCustomer()
    {
        if(currentDayCustomerCount < dailyCustomer)
        {
            currentDayCustomerCount++;

            int randomCustomerIndex = Random.Range(0, currentLevelIndex);

            GameObject newCustomer = Instantiate(customerPrefabs[randomCustomerIndex], customerSpawnPosition.transform.position, Quaternion.identity);

            customers.Add(newCustomer);
            UpdateCustomerIndices();
        }
        else
        {
            //Blow up
        }

        
    }

    public void IncreaseLevel()
    {
        currentLevelIndex++;
        if(currentLevelIndex >= customerPrefabs.Count)
        {
            currentLevelIndex = customerPrefabs.Count - 1;
        }
    }

    public void DeliverToCustomerSuccess()
    {

        leavingCustomers.Add(customers[0]);
        playerController.AddScore(100);
        customers.RemoveAt(0);
        CustomerComplete();
        UpdateCustomerIndices();
        
    }

    private void CustomerComplete()
    {
        
        isFirstCustomerInPosition = false;
        
        currentDayCustomersFinished++;
        
        if(currentDayCustomersFinished == dailyCustomer)
        {
            //It's the end of the day!!!!!!
            playerController.winScreen.enabled = true;
        }
        
    }

    public void SpawnNewCustomerOnTimer()
    {
        float spawnTime = Random.Range(spawnTimeIntervalMin, spawnTimeIntervalMax);

        StartCoroutine(SpawnCustomerAfterTime(spawnTime));
    }   
    
    private IEnumerator SpawnCustomerAfterTime(float timeToSpawn)
    {
        yield return new WaitForSeconds(timeToSpawn);

        AddCustomer();
        SpawnNewCustomerOnTimer();
    }

    public void CustomerAngryLeaves(int index)
    {
        angyBois++;
        Debug.Log(index + " is leaving");
        Debug.Log("Score before subtracting: " + playerController.gameScore);
        playerController.SubtractScore(50);

        Debug.Log("Score after subtracting: " +playerController.gameScore);
        if (customers.Count > index)
        {
            leavingCustomers.Add(customers[index]);
            customers.RemoveAt(index);

            CustomerComplete();

            UpdateCustomerIndices();
        }
    }

    private void UpdateCustomerIndices()
    {
        for (int i = 0; i < customers.Count; i++)
        {
            customers[i].GetComponent<Customer>().SetCustomerIndex(i);
        }
    }

    public void DeliverIngredientToCustomer(IngredientType ingredientType)
    {
        if(customers.Count > 0)
        {
            customers[0].GetComponent<Customer>().DeliverToCustomer(ingredientType);
        }
    }

    public IngredientType GetCurrentCustomerIngredientType()
    {
        if(customers.Count > 0)
        {
            return customers[0].GetComponent<Customer>().customerDemands;
        }
        else
        {
            return IngredientType.None;
        }    
    }

    public int GetCurrentCustomerIngredientAmount()
    {
        if(customers.Count > 0)
        {
            return customers[0].GetComponent<Customer>().GetCustomerDemandsAmount();
        }
        else
        {
            return 0;
        }
    }

    public bool IsFirstCustomerInPosition()
    {
        return isFirstCustomerInPosition;
    }
}
