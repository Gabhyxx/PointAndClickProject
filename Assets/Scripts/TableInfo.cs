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
    
    public void ReadyToOrderDrink()
    {
        StartCoroutine(WaitToOrderDrink());
    }

    IEnumerator WaitToOrderDrink()
    {
        yield return new WaitForSeconds(60);
        CustomersLeave();
    }

    private void CustomersLeave()
    {
        List<GameObject> listCustomersTable = new List<GameObject>();
        //Comprobar que customer esta presente
        if (!isCounter)
        {
            if (transform.GetChild(2).GetChild(0).childCount == 3)
            {
                listCustomersTable.Add(transform.GetChild(2).GetChild(0).GetChild(2).gameObject);
            }
            if (transform.GetChild(2).GetChild(1).childCount == 3)
            {
                listCustomersTable.Add(transform.GetChild(2).GetChild(1).GetChild(2).gameObject);
            }
            if (transform.GetChild(3).GetChild(0).childCount == 3)
            {
                listCustomersTable.Add(transform.GetChild(3).GetChild(0).GetChild(2).gameObject);
            }
            if (transform.GetChild(3).GetChild(1).childCount == 3)
            {
                listCustomersTable.Add(transform.GetChild(3).GetChild(1).GetChild(2).gameObject);
            }
            foreach (GameObject customer in listCustomersTable)
            {
                customer.transform.position = tableDetector.transform.position;
                customer.GetComponent<CustomerController>().SetDestination(-1);
            }
            gameInfo.SetGroupTablesAvailable(gameInfo.GetGroupTablesAvailable() + 1);
            isTaken = false;
        } else
        {
            if (transform.GetChild(1).childCount == 3)
            {
                listCustomersTable.Add(transform.GetChild(1).GetChild(2).gameObject);
            }
            foreach (GameObject customer in listCustomersTable)
            {
                customer.transform.position = tableDetector.transform.position;
                customer.GetComponent<CustomerController>().SetDestination(-1);
            }
            gameInfo.SetCounterSeatsAvailable(gameInfo.GetCounterSeatsAvailable() + 1);
            isTaken = false;
        }
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
}
