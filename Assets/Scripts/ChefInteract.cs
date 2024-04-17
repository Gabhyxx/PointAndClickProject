using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ChefInteract : MonoBehaviour
{
    [SerializeField] GameObject gameInfo;
    [SerializeField] List<GameObject> spots = new List<GameObject>();
    private GameObject tray1;

    public void OrderReady(int targetTable, List<int> listItems)
    {
        SpawnTray(targetTable);

        if (tray1 != null)
        {
            foreach (int itemID in listItems)
            {
                GameObject itemDisplay = new GameObject();
                List<Item> menuList = gameInfo.GetComponent<GameInfo>().GetItems();
                foreach (Item item in menuList)
                {
                    
                    if (itemID==item.id)
                    {
                        for(int j=0; j<4; j++)
                        {
                            if (tray1.transform.GetChild(j).GetComponent<ItemDisplay>().item == null)
                            {
                                tray1.transform.GetChild(j).GetComponent<ItemDisplay>().item = (Item)item;
                                break;
                            }
                        }

                    }
                }
            }
        }

    }

    private void SpawnTray(int targetTable)
    {
        for (int i = 0; i < spots.Count; i++)
        {
            if (spots.ElementAt(i).transform.GetChild(0).GetComponent<TrayInfo>().GetTargetTable() == -1)
            {
                tray1 = spots.ElementAt(i).transform.GetChild(0).gameObject;
                tray1.GetComponent<TrayInfo>().SetTargetTable(targetTable);
                break;
            }
        }
    }
}
