using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePowerUp : PickUpParent
{
    protected override void PickUpPowerUp()
    {
        player.PickUpSlidePowerUp();
        base.PickUpPowerUp();
    }
}
