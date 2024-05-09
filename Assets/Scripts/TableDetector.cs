using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TableDetector : MonoBehaviour
{
    [SerializeField] Transform tableSeat0;
    [SerializeField] Transform tableSeat1;
    [SerializeField] Transform tableSeat2;
    [SerializeField] Transform tableSeat3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Customer"))
        {
            if (other.gameObject.GetComponent<CustomerController>().GetTableID() == transform.parent.gameObject.GetComponent<TableInfo>().GetId())
            {
                SitCustomer(other.gameObject);
            }   
        }
        if (other.gameObject.tag.Equals("Player"))
        {
            transform.parent.gameObject.GetComponent<TableInfo>().SetPlayerOnTable(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            transform.parent.gameObject.GetComponent<TableInfo>().SetPlayerOnTable(null);
        }
    }

    private void SitCustomer(GameObject customer)
    {
        GameObject spot = customer.transform.parent.gameObject;
        string spotName = spot.name;
        customer.GetComponent<NavMeshAgent>().enabled = false;
        if (spotName.Equals("Spot0"))
        {
            customer.transform.parent = tableSeat0;
            customer.transform.position = tableSeat0.position;
            customer.transform.parent.gameObject.GetComponent<SeatInfo>().SetIsEmpty(false);
            customer.transform.parent.gameObject.GetComponent<SeatInfo>().SetOrder1(Random.Range(0, 15));

        }
        else if (spotName.Equals("Spot1"))
        {
            customer.transform.parent = tableSeat1;
            customer.transform.position = tableSeat1.position;
            customer.transform.parent.gameObject.GetComponent<SeatInfo>().SetIsEmpty(false);
            customer.transform.parent.gameObject.GetComponent<SeatInfo>().SetOrder1(Random.Range(0, 15));
        }
        else if (spotName.Equals("Spot2"))
        {
            customer.transform.parent = tableSeat2;
            customer.transform.position = tableSeat2.position;
            customer.transform.parent.gameObject.GetComponent<SeatInfo>().SetIsEmpty(false);
            customer.transform.parent.gameObject.GetComponent<SeatInfo>().SetOrder1(Random.Range(0, 15));
        }
        else if (spotName.Equals("Spot3"))
        {
            customer.transform.parent = tableSeat3;
            customer.transform.position = tableSeat3.position;
            customer.transform.parent.gameObject.GetComponent<SeatInfo>().SetIsEmpty(false);
            customer.transform.parent.gameObject.GetComponent<SeatInfo>().SetOrder1(Random.Range(0, 15));
        }
        if (!transform.parent.gameObject.GetComponent<TableInfo>().GetCustomerOnPlace())
        {
            transform.parent.gameObject.GetComponent<TableInfo>().SetCustomerOnPlace(true);
            transform.parent.gameObject.GetComponent<TableInfo>().ThinkingOrder();
            //transform.parent.gameObject.GetComponent<TableInfo>().ReadyToOrderDrink();
        }
        customer.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;


    }

    
}
