using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerGenerator : MonoBehaviour
{
    [SerializeField]private GameObject customerPrefab;
    [SerializeField]private List<Transform > customerStandPos;
    [SerializeField]private string[] firstSentences;
    [SerializeField]private string[] secondSentences;

    private Dictionary<DrinkName,Drink> drinks;

    private void Start()
    {
        CameraFocus.instance.gameStarted += HandleGameStart;
        Events.onCustomerGoen.AddListener(AddTransfom);
        AddDrinks();
    }

    private void HandleGameStart()
    {
        GenerateCustomer();
        StartCoroutine(TryGenerateCustomer());
    }

    private IEnumerator TryGenerateCustomer()
    {
        while (true)
        {
            float waitTime = UnityEngine.Random.Range(5f, 10f);
            yield return new WaitForSeconds(waitTime);
           GenerateCustomer();
        }
    }

    private void GenerateCustomer()
    {
        DrinkName randomDrink = (DrinkName)UnityEngine.Random.Range(0, Enum.GetValues(typeof(DrinkName)).Length);
        if (customerStandPos.Count > 0 && drinks.ContainsKey(randomDrink))
        {
            Customer customer = Instantiate(customerPrefab).GetComponent<Customer>();
            int randomPosIndex = UnityEngine.Random.Range(0, customerStandPos.Count);
            string fs = firstSentences[UnityEngine.Random.Range(0, firstSentences.Length)];
            string sec = secondSentences[UnityEngine.Random.Range(0, secondSentences.Length)];
            customer.InitCustomer(customerStandPos[randomPosIndex], drinks[randomDrink], fs, sec);
            customerStandPos.RemoveAt(randomPosIndex);
        }
    }

    private void AddTransfom(Transform t)=>customerStandPos.Add(t);
    
    private void AddDrinks()
    {
        drinks = new Dictionary<DrinkName, Drink>();

         Drink drink = new Drink(DrinkName.Lemozingy,
           new Dictionary<TapName, float>() {
                { TapName.Ale, 60f},
                {TapName.Mead, 40f }
           });
        drinks.Add(drink.name, drink);
        
        drink = new Drink(DrinkName.GoldBlend,
           new Dictionary<TapName, float>() {
                { TapName.Ale, 25f},
                {TapName.Tea, 75f }
           });

        drinks.Add(drink.name, drink);

        drink = new Drink(DrinkName.Blossom,
            new Dictionary<TapName, float>() {
                { TapName.Tea, 50f},
                {TapName.Mead, 50f }
            });
        drinks.Add(drink.name, drink);

       drink = new Drink(DrinkName.Reverie,
            new Dictionary<TapName, float>() {
                {TapName.Tea, 50f},
                {TapName.Mead, 25f },
                {TapName.Ale, 25f }
            });
        drinks.Add(drink.name, drink);

        drink = new Drink(DrinkName.Serenade,
            new Dictionary<TapName, float>() {
                {TapName.Tea, 70f},
                {TapName.Mead, 20f },
                {TapName.Ale, 10f }
            });
        drinks.Add(drink.name, drink);
    }
}