using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableInfo : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] bool isTaken;
    
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
}
