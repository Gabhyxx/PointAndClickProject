using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    [SerializeField] List<GameObject> menuItems = new List<GameObject>();
    [SerializeField] List<GameObject> tables = new List<GameObject>();

    public List<GameObject> GetTables()
    {
        return tables;
    }
}
