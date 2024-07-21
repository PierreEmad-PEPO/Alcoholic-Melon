using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink 
{
    public DrinkName name;
    public Dictionary<TapName, float> ingredients = new Dictionary<TapName, float>();

    public Drink(DrinkName name, Dictionary<TapName, float> ingredients)
    {
        this.name = name;
        this.ingredients = ingredients;
    }
}
