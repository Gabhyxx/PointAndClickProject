using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    private bool isGrabbing = false;
    private bool canGrab = false;
    private bool canServe = false;

    private int serveTable = -1;
    private List<ItemDisplay> listItems;
    private GameObject tray;
    private GameObject traySpot;

    [SerializeField] GameObject gameInfo;
    [SerializeField] GameObject trayHolder;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tray" && !isGrabbing)
        {
            canGrab = true;
            tray = other.gameObject;
        }

        
        if(other.gameObject.tag == "Table" && isGrabbing)
        {
            canServe = true;
            serveTable = other.gameObject.GetComponentInParent<TableInfo>().GetId();
        }

        if(other.gameObject.tag == "TraySpot" && isGrabbing)
        {
            traySpot = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Tray" && !isGrabbing)
        {
            canGrab = false;
            tray = null;
        }
        if (other.gameObject.tag == "Table" && isGrabbing)
        {
            canServe = false;
            serveTable = -1;
        }
        if (other.gameObject.tag == "TraySpot" && isGrabbing)
        {
            traySpot = null;
        }
    }

    public void PlayerInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (canGrab)
            {
                tray.transform.parent = trayHolder.transform;
                tray.GetComponent<Transform>().position = trayHolder.transform.position;
                isGrabbing = true;
                listItems = tray.GetComponent<TrayInfo>().GetItemsOnTray();
            }
            if (canServe && tray.GetComponent<TrayInfo>().GetTargetTable() == serveTable)
            {
                Debug.Log("Sirviendo");
                ServeToTable();
                
            }

            if (isGrabbing && traySpot != null && traySpot.transform.childCount == 0)
            {
                tray.transform.parent = traySpot.transform;
                tray.transform.position = traySpot.transform.position;
                isGrabbing = false;
            }
        }
    }

    public void ServeToTable()
    {
        for (int i = 0; i < listItems.Count; i++)
        {
            SeatInfo[] currentSeats = gameInfo.GetComponent<GameInfo>().GetTables().ElementAt(serveTable - 1).GetComponentsInChildren<SeatInfo>();
            for (int j = 0; j < currentSeats.Length; j++)
            {
                if (currentSeats[j].GetOrder1() == listItems.ElementAt(i).item.id)
                {
                    listItems.ElementAt(i).transform.parent = currentSeats[j].transform.GetChild(0);
                    listItems.ElementAt(i).transform.position = currentSeats[j].transform.GetChild(0).position;
                    listItems = tray.GetComponent<TrayInfo>().GetItemsOnTray();
                    i--;
                    currentSeats[j].SetOrder1(-1);
                    break;

                }
                else if (currentSeats[j].GetOrder2() == listItems.ElementAt(i).item.id)
                {
                    listItems.ElementAt(i).transform.parent = currentSeats[j].transform.GetChild(1);
                    listItems.ElementAt(i).transform.position = currentSeats[j].transform.GetChild(1).position;
                    listItems = tray.GetComponent<TrayInfo>().GetItemsOnTray();
                    i--;
                    currentSeats[j].SetOrder2(-1);
                    break;
                }


            }

        }
        tray.GetComponent<TrayInfo>().SetTargetTable(-1);
    }

    private void Update()
    {
        PlayerInput();
    }

}
