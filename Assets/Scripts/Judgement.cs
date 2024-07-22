using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judgement : MonoBehaviour
{
    [SerializeField] Tap redTap;
    [SerializeField] Tap greenTap;
    [SerializeField] Tap blueTap;
    [SerializeField] Cup cup;

    public string[] likedSentences = new string[0];
    public string[] semilikedSentences = new string[0];
    public string[] dislikedSentences = new string[0];

    Dictionary<TapName, Tap> taps = new Dictionary<TapName, Tap>();
    Customer customer = null;

    private void Start()
    {
        taps.Add(TapName.Tea, blueTap);
        taps.Add(TapName.Ale, redTap);
        taps.Add (TapName.Mead, greenTap);

        Events.OnplayerClickOncustomer.AddListener(InitTaps);
    }

    public void Judge (int correct, int wrong)
    {
        if (correct > wrong)
        {
            int random = Random.Range(0, likedSentences.Length);
            customer.Judge(likedSentences[random]);
        }
        else if (correct < wrong) 
        {
            int random = Random.Range(0, dislikedSentences.Length);
            customer.Judge(dislikedSentences[random]);
        }
        else
        {
            int random = Random.Range(0, semilikedSentences.Length);
            customer.Judge(semilikedSentences[random]);
        }
        
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
