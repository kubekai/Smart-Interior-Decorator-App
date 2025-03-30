using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetData : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject obj;

    public void CountPlusPlus()
    {
        obj = this.transform.parent.gameObject;
        obj = obj.transform.parent.gameObject;
        obj.GetComponent<Data>().count = obj.GetComponent<Data>().count + 1;
    }

    public void GetCount()
    {
        obj = this.transform.parent.gameObject;
        obj = obj.transform.parent.gameObject;
       // Debug.Log(obj.GetComponent<Data>().count);
        Debug.Log(obj.GetComponent<Data>().rssi);
        Debug.Log(obj.GetComponent<Data>().ping1);
        Debug.Log(obj.GetComponent<Data>().ipaddress);
    }
}
