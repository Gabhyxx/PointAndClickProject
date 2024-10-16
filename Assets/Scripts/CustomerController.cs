using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class CustomerController : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform exit;
    private GameInfo gameInfo;
    [SerializeField] int tableID;
    private Vector3 destination;

    [SerializeField] GameObject angrySprite;
    [SerializeField] Transform mainCamera;

    public int GetTableID()
    {
        return tableID;
    }
    public void SetTableID(int tableID)
    {
        this.tableID = tableID;
    }

    public GameObject GetAngrySprite() { 
        return angrySprite;
    }

    public void SetDestination(int tableID)
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        GetComponent<NavMeshAgent>().enabled = true;
        if (tableID == -1)
        {
            this.tableID = tableID;

            agent.destination = exit.position;
            
        }

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Customer") && Math.Abs(Quaternion.Angle(collision.gameObject.transform.rotation, transform.rotation)) > 90)
        {
            transform.position += new Vector3(1,0,0);
        }
        
    }

    private void Start()
    {
        mainCamera = Camera.main.gameObject.transform;
        agent.SetDestination(destination);
    }

    private void LateUpdate()
    {
        angrySprite.transform.LookAt(angrySprite.transform.position + mainCamera.forward);
    }

    private void FixedUpdate()
    {
        AnimatingCustomer();
    }

    private void AnimatingCustomer()
    {
        if (GetComponent<NavMeshAgent>().velocity != Vector3.zero)
        {
            transform.GetChild(1).GetComponent<Animator>().SetBool("isWalking", true);
        } else
        {
            transform.GetChild(1).GetComponent<Animator>().SetBool("isWalking", false);
        }
       
    }
}
