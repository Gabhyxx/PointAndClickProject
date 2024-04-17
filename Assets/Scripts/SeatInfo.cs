using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatInfo : MonoBehaviour
{
    [SerializeField] int order1 = -1;
    [SerializeField] int order2 = -1;

    public int GetOrder1()
    {
        return order1;
    }
    public void SetOrder1(int order1)
    {
        this.order1 = order1;
    }
    public int GetOrder2()
    {
        return order2;
    }
    public void SetOrder2(int order2)
    {
        this.order2 = order2;
    }
}
