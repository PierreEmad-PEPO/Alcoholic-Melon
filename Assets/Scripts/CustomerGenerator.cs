using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject customerPrefab;
    [SerializeField]
    List<Transform > customerStandPos;

    Dictionary<DrinkName,Drink> drinks;

    public string[] firstSentences;
    public string[] secondSentences;
    public string[] likedSentences;
    public string[] hulfLikedSentences;
    public string[] unLikedSentences;

    // Start is called before the first frame update
    void Start()
    {
        Events.onCustomerGoen.AddListener(AddTransfom);
        AddDrinks();
        StartCoroutine(TryGenerateCustomer());
    }

    IEnumerator TryGenerateCustomer()
    {
        while (true)
        {
            float witeTime = UnityEngine.Random.Range(5f, 10f);
            yield return new WaitForSeconds(witeTime);
            DrinkName randomDrink = (DrinkName)UnityEngine.Random.Range(0, Enum.GetValues(typeof(DrinkName)).Length);
            if (customerStandPos.Count > 0 && drinks.ContainsKey(randomDrink))
            {
                Customer customer = Instantiate(customerPrefab).GetComponent<Customer>();
                int randomPosIndex = UnityEngine.Random.Range(0, customerStandPos.Count);
                string fs = firstSentences[UnityEngine.Random.Range(0, firstSentences.Length)];
                string sec = secondSentences[UnityEngine.Random.Range(0, secondSentences.Length)];
                //Debug.Log(customerStandPos.Count + "  " + customerStandPos[randomPosIndex].position);
                customer.intiCustomer(customerStandPos[randomPosIndex], drinks[randomDrink], fs, sec);
                customerStandPos.RemoveAt(randomPosIndex);
                
            }
        }
    }

    public void AddTransfom(Transform t)
    {
        Debug.Log(transform.position);
        customerStandPos.Add(t);
    }

    void AddDrinks()
    {
        
        drinks = new Dictionary<DrinkName, Drink>();
        Drink drink = new Drink(DrinkName.bbb,
           new Dictionary<TapName, float>() {
                { TapName.Red, 25f},
                {TapName.Green, 75f }
           });

        drinks.Add(drink.name, drink);

        drink = new Drink(DrinkName.ccc,
            new Dictionary<TapName, float>() {
                { TapName.Red, 50f},
                {TapName.Green, 50f }
            });
        drinks.Add(drink.name, drink);

        drink = new Drink(DrinkName.aaa,
           new Dictionary<TapName, float>() {
                { TapName.Red, 60f},
                {TapName.Green, 40f }
           });
        drinks.Add(drink.name, drink);
    }

}
