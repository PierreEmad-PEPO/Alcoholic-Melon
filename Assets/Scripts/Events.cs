using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Events 
{
    public static UnityEvent<Transform> onCustomerGoen = new UnityEvent<Transform>();
    public static UnityEvent<Drink, Customer> OnplayerClickOncustomer = new UnityEvent<Drink, Customer>();
}
