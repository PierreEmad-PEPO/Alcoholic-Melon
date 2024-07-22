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

    // Start is called before the first frame update
    void Start()
    {
        Events.onCustomerGoen.AddListener(AddTransfom);
        AddDrinks();
        GenerateCustomer();
        StartCoroutine(TryGenerateCustomer());
    }

    IEnumerator TryGenerateCustomer()
    {
        while (true)
        {
            float witeTime = UnityEngine.Random.Range(5f, 10f);
            yield return new WaitForSeconds(witeTime);
           GenerateCustomer();
        }
    }

    void GenerateCustomer()
    {
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
    public void AddTransfom(Transform t)
    {
        Debug.Log(transform.position);
        customerStandPos.Add(t);
    }

    void AddDrinks()
    {
        drinks = new Dictionary<DrinkName, Drink>();

         Drink drink = new Drink(DrinkName.Lemozingy,
           new Dictionary<TapName, float>() {
                { TapName.Ale, 60f},
                {TapName.Mead, 40f }
           });
        drinks.Add(drink.name, drink);
        
        drink = new Drink(DrinkName.Golden_Blend,
           new Dictionary<TapName, float>() {
                { TapName.Ale, 25f},
                {TapName.Tea, 75f }
           });

        drinks.Add(drink.name, drink);

        drink = new Drink(DrinkName.Mead_Blossom,
            new Dictionary<TapName, float>() {
                { TapName.Tea, 50f},
                {TapName.Mead, 50f }
            });
        drinks.Add(drink.name, drink);

       drink = new Drink(DrinkName.Ale_Reverie,
            new Dictionary<TapName, float>() {
                {TapName.Tea, 50f},
                {TapName.Mead, 25f },
                {TapName.Ale, 25f }
            });
        drinks.Add(drink.name, drink);

        drink = new Drink(DrinkName.Ale_Serenade,
            new Dictionary<TapName, float>() {
                {TapName.Tea, 70f},
                {TapName.Mead, 20f },
                {TapName.Ale, 10f }
            });
        drinks.Add(drink.name, drink);

    }

}
