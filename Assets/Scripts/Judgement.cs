using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judgement : MonoBehaviour
{
    [SerializeField] Tap redTap;
    [SerializeField] Tap greenTap;
    [SerializeField] Tap blueTap;
    [SerializeField] Cup cup;

    Dictionary<TapName, Tap> taps = new Dictionary<TapName, Tap>();
    Customer customer = null;

    private void Start()
    {
        taps.Add(TapName.Blue, blueTap);
        taps.Add(TapName.Red, redTap);
        taps.Add (TapName.Green, greenTap);

        Events.OnplayerClickOncustomer.AddListener(InitTaps);
    }

    public void Judge (Drink drink, Drink newDrink)
    {
        customer.Judge("asdfasdf");
        customer = null;
    }

    public void InitTaps(Drink drink, Customer customer)
    {
        if (this.customer == null) 
        {
            this.customer = customer;
            foreach (var tap in taps)
            {
                if (drink.ingredients.ContainsKey(tap.Key))
                {
                    tap.Value.RemainingPercentage = drink.ingredients[tap.Key];
                }
                else
                    tap.Value.RemainingPercentage = 0f;
            }

            cup.UpdateCurrentTap();
        }
        
    }
}
