using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private bool isGrabbing = false;
    private bool canGrab = false;
    private bool canServe = false;

    [SerializeField] GameObject trayHolder;
    [SerializeField] GameObject tray;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Tray" && !isGrabbing)
        {
            canGrab = true;
        }

        if(other.gameObject.tag == "Table" && isGrabbing)
        {
            canServe = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Tray" && !isGrabbing)
        {
            canGrab = false;
        }
        if (other.gameObject.tag == "Table" && isGrabbing)
        {
            canServe = false;
        }
    }

    private void Update()
    {
        if (canGrab && Input.GetMouseButtonDown(0))
        {
            tray.transform.parent = trayHolder.transform;
            tray.GetComponent<Transform>().position = trayHolder.transform.position;
            isGrabbing = true;
        }
        if (canServe && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Sirviendo");
        }
    }

}
