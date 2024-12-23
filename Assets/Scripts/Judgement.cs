using System.Collections.Generic;
using UnityEngine;

public class Judgement : MonoBehaviour
{
    [SerializeField] private Tap redTap;
    [SerializeField] private Tap greenTap;
    [SerializeField] private Tap blueTap;
    [SerializeField] private Cup cup;
    //TODO: add more sentences in the inspector
    [SerializeField] private string[] likedSentences = new string[0];
    [SerializeField] private string[] semilikedSentences = new string[0];
    [SerializeField] private string[] dislikedSentences = new string[0];

    private Dictionary<TapName, Tap> taps = new Dictionary<TapName, Tap>();
    private Customer customer = null;

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

    private void InitTaps(Drink drink, Customer customer)
    {
        //TODO: outline the player to show that they are being seletced
        if (this.customer == null) 
        {
            this.customer = customer;
            foreach (var tap in taps)
            {
                if (drink.ingredients.ContainsKey(tap.Key))
                    tap.Value.RemainingPercentage = drink.ingredients[tap.Key];
                
                else
                    tap.Value.RemainingPercentage = 0f;
            }
            cup.UpdateCurrentTap();
        }
    }
}