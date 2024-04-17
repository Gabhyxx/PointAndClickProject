using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public double price;
    public int points;
    public double waitingTime;

    public GameObject model;
}
