using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judgement : MonoBehaviour
{
    [SerializeField] Tap redTap;
    [SerializeField] Tap greenTap;
    [SerializeField] Tap blueTap;


    Dictionary<TapName, Tap> taps = new Dictionary<TapName, Tap>();
    Customer customer;

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
    }

    public void InitTaps(Drink drink, Customer customer)
    {
        foreach (var tap in taps) 
        {
/*            if (drink.ingredients.ContainsKey(tap.Key))
            {
                tap.Value.RemainingPercentage = drink.ingredients[tap.Key];
            }
            else
                tap.Value.RemainingPercentage = 0f;*/
        }
    }
}
