using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerSpawn : MonoBehaviour
{
    [SerializeField] GameObject customerPrefab;
    [SerializeField] GameObject[] customersModel;
    [SerializeField] GameInfo gameInfo;
    [SerializeField] int seatsTaken = 0;

    public int GetSeatsTaken()
    {
        return seatsTaken;
    }
    public void SetSeatsTaken(int seatsTaken)
    {
        this.seatsTaken = seatsTaken;
    }
    IEnumerator SpawnCustomers(float time)
    {
        gameInfo.SetMaxNumberOfSeatsUsed(Random.Range(12,24));
        yield return new WaitForSeconds(time);
        while (true) 
        {
            if (gameInfo.GetGroupTablesAvailable() + gameInfo.GetCounterSeatsAvailable() > 0)
            {
                List<GameObject> listTables = gameInfo.GetTables();
                int tablesTaken = 0;
                foreach (GameObject table in listTables)
                {
                    if (table.GetComponent<TableInfo>().GetIsTaken())
                    {
                        tablesTaken++;
                    }
                }
                int randomNumber;
                if(seatsTaken < gameInfo.GetMaxNumberOfSeatsUsed())
                {
                    if (tablesTaken < 6)
                    {
                        if (gameInfo.GetMaxNumberOfSeatsUsed() - seatsTaken < 4)
                        {
                            randomNumber = Random.Range(1, gameInfo.GetMaxNumberOfSeatsUsed() - seatsTaken + 1);
                        } else
                        {
                            randomNumber = Random.Range(1, 5);
                        }
                        
                    }
                    else
                    {
                        randomNumber = Random.Range(1, 3);
                    }
                    
                } else
                {
                    randomNumber = 0;
                }

                List<int> destinationIDs = gameInfo.DestinationCustomers(randomNumber);
                seatsTaken += destinationIDs.Count;
                
                for (int i = 0; i < randomNumber; i++)
                {
                    
                    GameObject customerInScene = Instantiate(customerPrefab, transform.GetChild(i));
                    GameObject customerModelInScene = Instantiate(customersModel[Random.Range(0, 8)], customerInScene.transform);
                    customerInScene.GetComponent<CustomerController>().SetDestination(destinationIDs[i]);
                    if (destinationIDs[i] > 6)
                    {
                        foreach (GameObject table in listTables)
                        {
                            if (table.GetComponent<TableInfo>().GetId() == destinationIDs[i])
                            {
                                table.GetComponent<TableInfo>().SetIsTaken(true);
                            }
                        }
                    }
                }

                if (destinationIDs.Count > 1)
                {
                    if (destinationIDs.ElementAt(0) == destinationIDs.ElementAt(1))
                    {
                        foreach (GameObject table in listTables)
                        {
                            if (table.GetComponent<TableInfo>().GetId() == destinationIDs[0])
                            {
                                table.GetComponent<TableInfo>().SetIsTaken(true);
                            }
                        }
                    }
                }
                if (destinationIDs.Count == 1)
                {
                    foreach (GameObject table in listTables)
                    {
                        if (table.GetComponent<TableInfo>().GetId() == destinationIDs[0])
                        {
                            table.GetComponent<TableInfo>().SetIsTaken(true);
                        }
                    }
                }

            }
            time = Random.Range(10, 31);
            yield return new WaitForSeconds(time);
        }
    }
    private void Start()
    {

        StartCoroutine(SpawnCustomers(Random.Range(10,31)));
    }
}
