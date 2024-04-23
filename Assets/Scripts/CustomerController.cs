using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class CustomerController : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    private GameInfo gameInfo;
    [SerializeField] int tableID;
    private Vector3 destination;

    public void SetTableID(int tableID)
    {
        this.tableID = tableID;
    }

    public void SetDestination(int tableID)
    {
        gameInfo = GameObject.Find("GameInfo").GetComponent<GameInfo>();
        List<GameObject> tables = gameInfo.GetTables();
        foreach (GameObject table in tables)
        {
            
            if (table.GetComponent<TableInfo>().GetId() == tableID)
            {
                tableID = table.GetComponent<TableInfo>().GetId();
                this.tableID = tableID;
                this.destination = table.transform.position;
                
            }
        }

    }

    private void Start()
    {
        agent.SetDestination(destination);
    }



}
