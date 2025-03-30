using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Detail : MonoBehaviour
{
    public GameObject obj;

    public TextMeshProUGUI textvieww;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        obj = transform.parent.gameObject;
        obj = obj.transform.parent.gameObject;

        //obj.GetComponent<ObjectGenerator>().Lock = true;

        textvieww.text = "Network IP: "+obj.GetComponent<Data>().ipaddress+'\n'+
                         "Network Delay connecting to Google: "+obj.GetComponent<Data>().ping1+" ms"+'\n'+
                         "Network Delay connecting to YT: "+obj.GetComponent<Data>().ping2+" ms"+'\n'+
                         "Network Delay connecting to FB: "+obj.GetComponent<Data>().ping3+" ms"+'\n'+
                         "Signal Strength: "+obj.GetComponent<Data>().rssi+" dbm"+'\n';
            


       
    }
}
