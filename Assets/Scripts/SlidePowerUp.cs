using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePowerUp : PickUpParent
{
    public GameObject sign;
    protected override void PickUpPowerUp()
    {
        player.PickUpSlidePowerUp();
        base.PickUpPowerUp();

        sign.SetActive(true);
        Destroy(gameObject);
    }
}
