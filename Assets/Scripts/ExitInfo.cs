using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class ExitInfo : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Customer" && other.gameObject.GetComponent<CustomerController>().GetTableID() == -1)
        {
            Destroy(other.gameObject);
        }
    }
}
