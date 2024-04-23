using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    [SerializeField] List<Item> menuItems = new List<Item>();
    [SerializeField] List<GameObject> tables;
    [SerializeField] int groupTablesAvailable = 4;
    [SerializeField] int counterSeatsAvailable = 4;
    [SerializeField] int maxNumberOfSeatsUsed = -1;
    public List<Item> GetItems()
    {
        return menuItems;
    }
    public List<GameObject> GetTables()
    {
        return tables;
    }
    public int GetGroupTablesAvailable()
    {
        return groupTablesAvailable;
    }
    public void SetGroupTablesAvailable(int groupSeatsAvailable)
    {
        this.groupTablesAvailable = groupSeatsAvailable;
    }
    public int GetCounterSeatsAvailable()
    {
        return counterSeatsAvailable;
    }
    public void SetCounterSeatsAvailable(int counterSeatsAvailable)
    {
        this.counterSeatsAvailable = counterSeatsAvailable;
    }
    public int GetMaxNumberOfSeatsUsed ()
    {
        return maxNumberOfSeatsUsed;
    }
    public void SetMaxNumberOfSeatsUsed(int maxNumberOfSetsUsed)
    {
        this.maxNumberOfSeatsUsed = maxNumberOfSetsUsed;
    }

    public List<int> DestinationCustomers(int numCustomer)
    {
        List<int> listDestinationID = new List<int>();
        if (numCustomer > 2)
        {
            int destinationID = Random.Range(1, 4);
            while (tables.ElementAt(destinationID - 1).GetComponent<TableInfo>().GetIsTaken() || groupTablesAvailable==0)
            {
                Debug.Log("WHILE 1");
                destinationID = Random.Range(1, 4);
            }
            for(int i = 0; i < numCustomer; i++)
            {
                listDestinationID.Add(destinationID);
            }
            tables.ElementAt(destinationID - 1).GetComponent<TableInfo>().SetIsTaken(true);
            groupTablesAvailable--;
        }
        else
        {
            int destinationID = -1;
            if (counterSeatsAvailable >= numCustomer && groupTablesAvailable > 0) {
                Debug.Log("if 1");
                if (numCustomer == 1)
                {
                    if (IsSeatTypeCounter(75))
                    {
                        Debug.Log("Counter");
                        for (int i = 4; i < 8; i++)
                        {
                            Debug.Log("i = "+i);
                            if (!tables.ElementAt(i).GetComponent<TableInfo>().GetIsTaken())
                            {
                                destinationID = i+1;
                                break;
                            }
                        }
                        listDestinationID.Add(destinationID);
                        counterSeatsAvailable--;
                    } else
                    {
                        do
                        {
                            Debug.Log("Do while");
                            destinationID = Random.Range(1, 4);

                        } while (tables.ElementAt(destinationID-1).GetComponent<TableInfo>().GetIsTaken() || groupTablesAvailable == 0);
                        listDestinationID.Add(destinationID);
                        tables.ElementAt(destinationID-1).GetComponent<TableInfo>().SetIsTaken(true);
                        groupTablesAvailable--;
                    }

                } else if(numCustomer == 2)
                {
                    if (IsSeatTypeCounter(40))
                    {
                        for(int i = 4; i<8; i++)
                        {
                            if (!tables.ElementAt(i).GetComponent<TableInfo>().GetIsTaken() && !tables.ElementAt(i+1).GetComponent<TableInfo>().GetIsTaken())
                            {
                                destinationID = i + 1;
                                break;
                            }
                        }
                        listDestinationID.Add(destinationID);
                        listDestinationID.Add(destinationID+1);
                        counterSeatsAvailable -= 2;
                    }
                    else
                    {
                        destinationID = Random.Range(1, 4);
                        for(int i=0; i < 4; i++)
                        {
                            if (!tables.ElementAt(i).GetComponent<TableInfo>().GetIsTaken())
                            {
                                destinationID = i + 1;
                            }
                            
                        }
                        for (int i = 0; i < 2; i++)
                        {
                            listDestinationID.Add(destinationID);
                        }
                        groupTablesAvailable--;
                    }
                }
            }
            else if(counterSeatsAvailable < numCustomer && groupTablesAvailable > 0)
            {
                Debug.Log("if 2");
                destinationID = Random.Range(1, 4);
                Debug.Log(tables.ElementAt(destinationID - 1).GetComponent<TableInfo>().GetIsTaken());
                while (tables.ElementAt(destinationID - 1).GetComponent<TableInfo>().GetIsTaken() || groupTablesAvailable == 0)
                {
                    Debug.Log("WHILE 2");
                    destinationID = Random.Range(1, 4);
                }
                for (int i = 0; i < numCustomer; i++)
                {

                    listDestinationID.Add(destinationID);
                }
                groupTablesAvailable--;
            }
            else {
                Debug.Log("if 3");
                destinationID = Random.Range(5, 8);
                if (numCustomer == 1)
                {
                    for (int i = 4; i < 8; i++)
                    {
                        if (!tables.ElementAt(i).GetComponent<TableInfo>().GetIsTaken())
                        {
                            destinationID = i+1;
                            break;
                        }
                    }
                    listDestinationID.Add(destinationID);
                }
                else
                {
                    for (int i = 4; i < 8; i++)
                    {
                        if (!tables.ElementAt(i).GetComponent<TableInfo>().GetIsTaken())
                        {
                            destinationID = i+1;
                            break;
                        }
                    }
                    listDestinationID.Add(destinationID);
                    listDestinationID.Add(destinationID + 1);
                }

            }
        }
        return listDestinationID;
    }

    private bool IsSeatTypeCounter(float probability)
    {
        int randomNumber = Random.Range(1, 100);
        if (randomNumber <= probability)
        {
            return true;
        } else
        {
            return false;
        }
            
    }
    private void Awake()
    {
        maxNumberOfSeatsUsed = Random.Range(12, 20);
    }
}
