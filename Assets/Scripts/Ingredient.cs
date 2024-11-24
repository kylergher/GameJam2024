using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IngredientType
{
    Sardine,
    Mackerel,
    Bigboy,
    None
}

public class Ingredient : PickUpParent
{
    public IngredientType ingredientType;
    public SpriteRenderer theFish;

    protected override void Update()
    {
        base.Update();
        if(this.ingredientType == IngredientType.Sardine)
        {
            
        }
    }
    protected override void PickUpPowerUp()
    {
        base.PickUpPowerUp();
        player.PickUpIngredient(ingredientType);
        //theFish = gameObject.GetComponent<SpriteRenderer>();
        theFish.enabled = false;
    }

    public void ReenableIngredientSprite()
    {
        theFish.enabled = true;
    }
}
