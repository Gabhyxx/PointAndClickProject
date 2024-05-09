using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] int idTableNote = -1;
    [SerializeField] List<int> itemsAsked = new List<int>();

    public int GetIDTableNote()
    {
        return idTableNote;
    }
    public void SetIDTableNote(int idTableNote)
    {
        this.idTableNote = idTableNote;
    }

    public List<int> GetItemsAsked()
    {
        return itemsAsked;
    }
    public void SetItemsAsked(List<int> itemsAsked)
    {
        this.itemsAsked = itemsAsked;
    }

    public void WriteNotes(int idTable, List<int> listOrder)
    {
        idTableNote = idTable;
        itemsAsked = listOrder;
    }
}
