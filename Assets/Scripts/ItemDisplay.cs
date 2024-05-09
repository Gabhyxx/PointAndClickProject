using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplay : MonoBehaviour
{
    public Item item;

    public GameObject model;

    internal void Display()
    {
        if (item != null)
        {
            model = item.model;
            GameObject cloneItem = Instantiate(model, transform);
        }
    }

    void Start()
    {
        
        

        
        
    }
}
