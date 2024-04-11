using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplay : MonoBehaviour
{
    public Item item;

    public GameObject model;
    void Start()
    {
        model = item.model;

        
        GameObject cloneItem = Instantiate(model, transform);
    }
}
