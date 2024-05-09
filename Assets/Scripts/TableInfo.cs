using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableInfo : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] bool isTaken;
    [SerializeField] TableDetector tableDetector;
    [SerializeField] GameInfo gameInfo;
    [SerializeField] bool isCounter;
    [SerializeField] GameObject spawnCustomers;
    [SerializeField] bool customerOnPlace;
    [SerializeField] GameObject playerOnTable;
    
    public int GetId()
    {
        return id;
    }

    public bool GetIsTaken() { 
        return isTaken;
    }

    public void SetIsTaken(bool isTaken)
    {
        this.isTaken = isTaken;
    }

    public bool GetIsCounter()
    {
        return isCounter;
    }

    public GameObject GetPlayerOnTable()
    {
        return playerOnTable;
    }
    public void SetPlayerOnTable(GameObject playerOnTable)
    {
        this.playerOnTable = playerOnTable;
    }
    
    public void ReadyToOrder()
    {
        StartCoroutine(WaitOrder());
    }

    IEnumerator WaitOrder()
    {
        float timeCounter = 0;

        Debug.Log("Mesa " + id + " lista para pedir");
        while (timeCounter < 60)
        {
            timeCounter += Time.deltaTime;
            if(playerOnTable != null && Input.GetMouseButtonDown(0))
            {
                Debug.Log("Tomando nota");
                List<int> tableOrders = new List<int>();
                List<GameObject> listSeatsTable = GetListSeatsTable();
                foreach (GameObject seat in listSeatsTable)
                {
                    tableOrders.Add(seat.GetComponent<SeatInfo>().GetOrder1());
                    tableOrders.Add(seat.GetComponent<SeatInfo>().GetOrder2());
                }
                playerOnTable.GetComponent<PlayerInfo>().WriteNotes(id, tableOrders);
                yield break;
            }
            yield return null;
        }
        
        CustomersLeave();
    }

    public List<GameObject> GetListCustomersTable()
    {
        List<GameObject> listCustomersTable = new List<GameObject>();
        if (!isCounter)
        {
            if (transform.GetChild(2).GetChild(1).childCount == 3)
            {
                listCustomersTable.Add(transform.GetChild(2).GetChild(1).GetChild(2).gameObject);
            }
            if (transform.GetChild(2).GetChild(2).childCount == 3)
            {
                listCustomersTable.Add(transform.GetChild(2).GetChild(2).GetChild(2).gameObject);
            }
            if (transform.GetChild(3).GetChild(1).childCount == 3)
            {
                listCustomersTable.Add(transform.GetChild(3).GetChild(1).GetChild(2).gameObject);
            }
            if (transform.GetChild(3).GetChild(2).childCount == 3)
            {
                listCustomersTable.Add(transform.GetChild(3).GetChild(2).GetChild(2).gameObject);
            }
        }
        else
        {
            if (transform.GetChild(1).childCount == 3)
            {
                listCustomersTable.Add(transform.GetChild(1).GetChild(2).gameObject);
            }
        }
        return listCustomersTable;
    }
    private void CustomersLeave()
    {
        List<GameObject> listCustomersTable = GetListCustomersTable();
        foreach (GameObject customer in listCustomersTable)
        {
            customer.transform.position = tableDetector.transform.position;
            customer.GetComponent<CustomerController>().SetDestination(-1);
        }
        gameInfo.SetGroupTablesAvailable(gameInfo.GetGroupTablesAvailable() + 1);
        isTaken = false;
        
        foreach (GameObject customer in listCustomersTable)
        {
            customer.transform.position = tableDetector.transform.position;
            customer.GetComponent<CustomerController>().SetDestination(-1);
        }
        gameInfo.SetCounterSeatsAvailable(gameInfo.GetCounterSeatsAvailable() + 1);
        isTaken = false;
        spawnCustomers.GetComponent<CustomerSpawn>().SetSeatsTaken(spawnCustomers.GetComponent<CustomerSpawn>().GetSeatsTaken()-listCustomersTable.Count);
        customerOnPlace = false;
        
    }

    public bool GetCustomerOnPlace()
    {
        return customerOnPlace;
    }

    public void SetCustomerOnPlace(bool customerOnPlace)
    {
        this.customerOnPlace = customerOnPlace;
    }

    internal void ThinkingOrder()
    {
        StartCoroutine(TableThinkingOrder());
        
    }

    IEnumerator TableThinkingOrder()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(3,16));
        List<GameObject> listSeatsTable = GetListSeatsTable();
        foreach (GameObject seat in listSeatsTable)
        {
            seat.GetComponent<SeatInfo>().SetRandomOrder();
        }
        ReadyToOrder();
    }

    private List<GameObject> GetListSeatsTable()
    {
        List<GameObject> listSeats = new List<GameObject>();
        List<GameObject> listCustomers = GetListCustomersTable();
        foreach(GameObject customer in listCustomers)
        {
            listSeats.Add(customer.transform.parent.gameObject);
        }
        return listSeats;
    }
}
