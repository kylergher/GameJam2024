using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepThemCalm : MonoBehaviour
{
    public Customer patronScript;
    public GameObject resetButton;

    public bool closeEnough = false;

    public void OnEnable()
    {
        Debug.Log("Proof this is working");
        
    }

    [System.Obsolete]
    void Update()
    {
        if (closeEnough == true && Input.GetKeyDown(KeyCode.E))
        {
            patronScript = FindObjectsByType<Customer>();
            Debug.Log("Chief! ITs working!!");
            patronScript.remainingTime = patronScript.customerAngyLevel.maxValue;
            patronScript.angerTimerNotFrontOfLine = 0f;
            //patronScript.angerTimerFrontOfLine = 0f;
            //patronScript.angerTimerNotFrontOfLine = 0f;
            resetButton.SetActive(false);
        }
    }

    private T FindObjectsByType<T>()
    {
        throw new NotImplementedException();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("He's in range");
            closeEnough = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            closeEnough = false;
            Debug.Log("He is no longer in range");
        }
    }


}
