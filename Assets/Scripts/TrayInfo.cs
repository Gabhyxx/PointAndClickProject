using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayInfo : MonoBehaviour
{
    private int targetTable;

    public int GetTargetTable()
    {
        return targetTable;
    }

    public void setTargetTable(int targetTable)
    {
        this.targetTable = targetTable;
    }

    public ItemDisplay[] GetItemsOnTray() {
        return GetComponentsInChildren<ItemDisplay>();
    }

    private void Start()
    {
        targetTable = 1;
    }
}
