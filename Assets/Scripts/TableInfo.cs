using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] bool isEating;
    [SerializeField] GameObject hand;
    [SerializeField] GameObject pay;
    [SerializeField] GameObject angry;
    [SerializeField] int scoreOnHold = 0;

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

    public bool GetIsEating()
    {
        return isEating;
    }
    public void SetPlayerOnTable(bool isEating)
    {
        this.isEating = isEating;
    }

    public void ReadyToOrder()
    {
        StartCoroutine(WaitOrder());
    }

    IEnumerator WaitOrder()
    {
        float timeCounter = 0;

        hand.SetActive(true);
        while (timeCounter < 60)
        {
            timeCounter += Time.deltaTime;
            if(playerOnTable != null && Input.GetMouseButtonDown(0) && playerOnTable.GetComponent<PlayerInfo>().GetIDTableNote()==-1)
            {
                Debug.Log("Tomando nota");
                hand.SetActive(false);
                List<int> tableOrders = new List<int>();
                List<GameObject> listSeatsTable = GetListSeatsTable();
                foreach (GameObject seat in listSeatsTable)
                {
                    tableOrders.Add(seat.GetComponent<SeatInfo>().GetOrder1());
                    tableOrders.Add(seat.GetComponent<SeatInfo>().GetOrder2());
                }
                playerOnTable.GetComponent<PlayerInfo>().WriteNotes(id, tableOrders);
                
                List<Item> listItems = gameInfo.GetItems();
                foreach (int tableOrder in tableOrders)
                {
                    foreach(Item item in listItems)
                    {
                        if(tableOrder == item.id)
                        {
                            scoreOnHold += item.points;
                        }
                    }
                }

                yield break;
            }
            yield return null;
        }
        hand.SetActive(false);
        
        CustomersLeave(true, -100);
    }

    public List<GameObject> GetListCustomersTable()
    {
        List<GameObject> listCustomersTable = new List<GameObject>();
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
    private void CustomersLeave(bool isAngry, int score)
    {
        scoreOnHold = 0;
        List<GameObject> listCustomersTable = GetListCustomersTable();
        foreach (GameObject customer in listCustomersTable)
        {
            customer.transform.position = tableDetector.transform.position;
            customer.GetComponent<CustomerController>().SetDestination(-1);
            if(isAngry)
            {
                customer.GetComponent<CustomerController>().GetAngrySprite().SetActive(true);
                gameInfo.SetScore(gameInfo.GetScore() + score);
            }
        }
        gameInfo.SetGroupTablesAvailable(gameInfo.GetGroupTablesAvailable() + 1);
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

    public void StartEating()
    {
        isEating = true;
        StartCoroutine(Eating());
    }

    IEnumerator Eating()
    {
        float timeToWait = UnityEngine.Random.Range(60, 121);
        Debug.Log("Comiendo durante " + timeToWait + " segundos");
        yield return new WaitForSeconds(timeToWait);
        if (!isCounter)
        {
            if (transform.GetChild(2).GetChild(0).GetChild(0).childCount > 0)
            {
                Destroy(transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject);
            }
            if (transform.GetChild(2).GetChild(1).GetChild(0).childCount > 0)
            {
                Destroy(transform.GetChild(2).GetChild(1).GetChild(0).GetChild(0).gameObject);
            }
            if (transform.GetChild(2).GetChild(0).GetChild(1).childCount > 0)
            {
                Destroy(transform.GetChild(2).GetChild(0).GetChild(1).GetChild(0).gameObject);
            }
            if (transform.GetChild(2).GetChild(1).GetChild(1).childCount > 0)
            {
                Destroy(transform.GetChild(2).GetChild(1).GetChild(1).GetChild(0).gameObject);
            }
            if (transform.GetChild(3).GetChild(0).GetChild(0).childCount > 0)
            {
                Destroy(transform.GetChild(3).GetChild(0).GetChild(0).GetChild(0).gameObject);
            }
            if (transform.GetChild(3).GetChild(1).GetChild(0).childCount > 0)
            {
                Destroy(transform.GetChild(3).GetChild(1).GetChild(0).GetChild(0).gameObject);
            }
            if (transform.GetChild(3).GetChild(0).GetChild(1).childCount > 0)
            {
                Destroy(transform.GetChild(3).GetChild(0).GetChild(1).GetChild(0).gameObject);
            }
            if (transform.GetChild(3).GetChild(1).GetChild(1).childCount > 0)
            {
                Destroy(transform.GetChild(3).GetChild(1).GetChild(1).GetChild(0).gameObject);
            }
        } else
        {
            if (transform.GetChild(1).GetChild(0).childCount > 0)
            {
                Destroy(transform.GetChild(1).GetChild(0).GetChild(0).gameObject);
            }
            if (transform.GetChild(1).GetChild(1).childCount > 0)
            {
                Destroy(transform.GetChild(1).GetChild(1).GetChild(0).gameObject);
            }
        }
        
        Debug.Log("Se termino de comer");
        StartCoroutine(WaitPay());
        isEating =false;
    }

    IEnumerator WaitPay()
    {
        float timeCounter = 0;

        Debug.Log("Mesa " + id + " lista para pagar");
        pay.SetActive(true);
        while (timeCounter < 15)
        {
            if (timeCounter >= 10)
            {
                pay.SetActive(false);
                angry.SetActive(true);
            }
            timeCounter += Time.deltaTime;
            if (playerOnTable != null && Input.GetMouseButtonDown(0))
            {
                Debug.Log("Cobrando");
                gameInfo.SetScore(gameInfo.GetScore() + scoreOnHold);
                pay.SetActive(false);
                angry.SetActive(false);
                CustomersLeave(false, 0);
                yield break;
            }
            yield return null;
        }
        pay.SetActive(false);
        angry.SetActive(false);
        CustomersLeave(true, -250);
    }
}
