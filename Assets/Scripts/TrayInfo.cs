using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayInfo : MonoBehaviour
{
    [SerializeField] int targetTable;
    [SerializeField] bool isReady = false;
    [SerializeField] Material[] outlinerMaterials;

    

    public int GetTargetTable()
    {
        return targetTable;
    }

    public void SetTargetTable(int targetTable)
    {

        this.targetTable = targetTable;
        if(targetTable == -1)
        {
            targetTable = 0;
        }
        Debug.Log(GetComponent<MeshRenderer>().materials[1]);
        //GetComponent<MeshRenderer>().materials[1] = outlinerMaterials[targetTable];
        if(targetTable ==  1)
        {
            GetComponent<MeshRenderer>().materials[1].color = new Color(8f, 255f, 0, 0);
        } else if(targetTable == 2)
        {
            GetComponent<MeshRenderer>().materials[1].color = new Color(23f, 162f, 255f, 0);
        }
        else if (targetTable == 3)
        {
            GetComponent<MeshRenderer>().materials[1].color = new Color(255f, 0f, 183f, 0);
        }
        else if (targetTable == 4)
        {
            GetComponent<MeshRenderer>().materials[1].color = new Color(115f, 0f, 255f, 0);
        }
        else if (targetTable == 5)
        {
            GetComponent<MeshRenderer>().materials[1].color = new Color(255f, 0f, 0f, 0);
        }
        else if (targetTable == 6)
        {
            GetComponent<MeshRenderer>().materials[1].color = new Color(255f, 206f, 0f, 0);
        }
        Debug.Log(GetComponent<MeshRenderer>().materials[1]);
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
