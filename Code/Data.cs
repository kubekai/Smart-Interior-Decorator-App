using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public int count;
     
    public int rssi;
    public float ping1;
    public float ping2;
    public float ping3;
    public string ipaddress;
    void Start()
    {
        count = 0;
        rssi = 0;
        ping1 = 0;
        ping2 = 0;
        ping3 = 0;
        ipaddress=" ";
    }
}
