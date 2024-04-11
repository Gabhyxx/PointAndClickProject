using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatInfo : MonoBehaviour
{
    private int order1 = 0;
    private int order2 = -1;

    public int GetOrder1()
    {
        return order1;
    }
    public int GetOrder2()
    {
        return order2;
    }
}
