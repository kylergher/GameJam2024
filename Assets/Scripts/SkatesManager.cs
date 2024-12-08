using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkatesManager : MonoBehaviour
{
    public GameObject iceSkates;
    public float minSpawnTime = 10f;
    public float maxSpawnTime = 27f;

    public float signDurationMin = 10f;
    public float signDurationMax = 27f;
    public GameObject sign;
    void Start()
    {
        iceSkates.SetActive(false);
        StartCoroutine(ShowSkatesAfterDelay());
    }

    private IEnumerator ShowSkatesAfterDelay()
    {
        float delay = Random.Range(minSpawnTime, maxSpawnTime);
        yield return new WaitForSeconds(delay);
        iceSkates.SetActive(true);
        StartHideSignCoroutine();
    }

    private IEnumerator HideSignAfterRandomDelay()
    {
        float delay = Random.Range(signDurationMin, signDurationMax);
        yield return new WaitForSeconds(delay);
        sign.SetActive(false);
        Debug.Log("Block area hidden!!");
    }

    public void StartHideSignCoroutine()
    {
        StartCoroutine(HideSignAfterRandomDelay());
    }
}
