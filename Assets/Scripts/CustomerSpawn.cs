using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerSpawn : MonoBehaviour
{
    [SerializeField] GameObject customerPrefab;
    [SerializeField] GameInfo gameInfo;
    private int seatsTaken = 0;

    IEnumerator SpawnCustomers(float time)
    {
        gameInfo.SetMaxNumberOfSeatsUsed(Random.Range(12,20));
        yield return new WaitForSeconds(time);
        while (true)
        {
            Debug.Break();
            Debug.Log("EMPIEZA");
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
                    if (tablesTaken < 4)
                    {
                        if (gameInfo.GetMaxNumberOfSeatsUsed() - seatsTaken < 4)
                        {
                            randomNumber = Random.Range(1, gameInfo.GetMaxNumberOfSeatsUsed() - seatsTaken);
                        } else
                        {
                            randomNumber = Random.Range(1, 4);
                        }
                        
                    }
                    else
                    {
                        randomNumber = Random.Range(1, 2);
                    }
                    
                } else
                {
                    randomNumber = 0;
                }
                
                List<int> destinationIDs = gameInfo.DestinationCustomers(randomNumber);
                seatsTaken += destinationIDs.Count;
                Debug.Log("Random number: " + randomNumber + "\nList tamaño: " + destinationIDs.Count);
                
                for (int i = 0; i < randomNumber; i++)
                {
                    Debug.Log(i + ") " + destinationIDs[i]);
                    GameObject customerInScene = Instantiate(customerPrefab, transform.GetChild(i));
                    Debug.Log("Customer instanciado");
                    customerInScene.GetComponent<CustomerController>().SetDestination(destinationIDs[i]);
                    if (destinationIDs[i] > 4)
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
            }
            time = Random.Range(10, 30);
            Debug.Log("FINAL");
            yield return new WaitForSeconds(time);
        }
    }
    private void Start()
    {

        StartCoroutine(SpawnCustomers(Random.Range(10,30)));
    }
}
