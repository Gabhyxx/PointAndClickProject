using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayInfo : MonoBehaviour
{
    [SerializeField] int targetTable;
    [SerializeField] bool isReady = false;

    public int GetTargetTable()
    {
        return targetTable;
    }

    public void SetTargetTable(int targetTable)
    {
        this.targetTable = targetTable;
    }

    public bool GetIsReady()
    {
        return isReady;
    }
    public void SetIsReady(bool isReady)
    {
        this.isReady = isReady;
    }

    public List<ItemDisplay> GetItemsOnTray() {
        ItemDisplay[] items = GetComponentsInChildren<ItemDisplay>();
        List<ItemDisplay> itemsResult = new List<ItemDisplay>();
        for(int i=0; i < items.Length; i++)
        {
            if (items[i].model != null)
            {
                itemsResult.Add(items[i]);
            }
        }
        return itemsResult;
    }

    
}
