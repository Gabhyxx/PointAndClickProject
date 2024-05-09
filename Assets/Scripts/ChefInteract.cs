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
    private GameObject playerPresent;
    private bool isCooking = false;
    private List<ItemWait> waitingList = new List<ItemWait>();
    public bool GetIsCooking()
    {
        return isCooking;
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            playerPresent = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            playerPresent = null;
        }
    }

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
            if (spots.ElementAt(i).transform.childCount > 0)
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
    private void PutOnWaitingList(int targetTable, List<int> listItems)
    {
        foreach (int itemID in listItems)
        {
            List<Item> menuList = gameInfo.GetComponent<GameInfo>().GetItems();
            foreach (Item item in menuList)
            {
                if (itemID < 16 && item.id==itemID)
                {
                    waitingList.Add(new ItemWait(itemID, targetTable));
                }
            }
        }
        foreach (int itemID in listItems)
        {
            List<Item> menuList = gameInfo.GetComponent<GameInfo>().GetItems();
            foreach (Item item in menuList)
            {
                if (itemID > 15 && item.id == itemID)
                {
                    waitingList.Add(new ItemWait(itemID, targetTable));
                }
            }
        }
    }
    IEnumerator PrepareNextTray()
    {
        isCooking = true;
        SpawnTray(waitingList.ElementAt(0).GetTargetTable());
        bool isOneItem = false;
        if (waitingList.ElementAt(0).GetTargetTable() >= 5)
        {
            isOneItem = true;
        }
        if (tray1 != null)
        {
            List<Item> menuList = gameInfo.GetComponent<GameInfo>().GetItems();
            int i = 0;
            bool isDrink = false;
            bool isFood = false;
            if (waitingList.ElementAt(0).GetItemID() < 16)
            {
                isDrink = true;
            }
            if (waitingList.ElementAt(0).GetItemID() > 15)
            {
                isFood = true;
            }
            while (i<waitingList.Count)
            {
                ItemWait itemWait = waitingList.ElementAt(i);
                if (waitingList.ElementAt(i).GetItemID() < 16)
                {
                    isDrink = true;
                }
                if (waitingList.ElementAt(i).GetItemID() > 15)
                {
                    isFood = true;
                }
                if (isDrink == isFood)
                {
                    break;
                }
                if (i == waitingList.Count)
                {
                    break;
                }
                foreach(Item item in menuList)
                {
                    if (item.id == itemWait.GetItemID())
                    {
                        double prepareTime = item.waitingTime;
                        double timeSpent = 0f;
                        while (timeSpent < prepareTime)
                        {
                            yield return new WaitForSeconds(0.1f);
                            timeSpent += 0.1;
                        }
                        for (int j = 0; j < 4; j++)
                        {
                            if (tray1.transform.GetChild(j).GetComponent<ItemDisplay>().item == null)
                            {

                                tray1.transform.GetChild(j).GetComponent<ItemDisplay>().item = (Item)item;
                                tray1.transform.GetChild(j).GetComponent<ItemDisplay>().Display();
                                break;
                            }
                        }
                        waitingList.Remove(itemWait);
                        i--;
                        break;

                    }
                }
                i++;
                if(isOneItem)
                {
                    break;
                }
            }
        }
        isCooking = false;
        tray1.gameObject.GetComponent<TrayInfo>().SetIsReady(true);
        yield return null;
    }

    private void FixedUpdate()
    {
        if (playerPresent != null)
        {
            if (Input.GetMouseButtonDown(0) && playerPresent.GetComponent<PlayerInfo>().GetIDTableNote() != -1
                && playerPresent.GetComponent<PlayerInfo>().GetItemsAsked().Count > 0)
            {
                Debug.Log("Items asked in total: " + playerPresent.GetComponent<PlayerInfo>().GetItemsAsked());
                PutOnWaitingList(playerPresent.GetComponent<PlayerInfo>().GetIDTableNote(),
                    playerPresent.GetComponent<PlayerInfo>().GetItemsAsked());
                playerPresent.GetComponent<PlayerInfo>().SetItemsAsked(new List<int>());
                playerPresent.GetComponent<PlayerInfo>().SetIDTableNote(-1);
            }
        }
        if(waitingList.Count > 0 && !isCooking)
        {
            StartCoroutine(PrepareNextTray());
        }
        
    }

    
}
