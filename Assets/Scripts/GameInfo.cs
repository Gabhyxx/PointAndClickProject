using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    [SerializeField] List<Item> menuItems = new List<Item>();
    [SerializeField] List<GameObject> tables = new List<GameObject>();

    public List<Item> GetItems()
    {
        return menuItems;
    }
    public List<GameObject> GetTables()
    {
        return tables;
    }
}
