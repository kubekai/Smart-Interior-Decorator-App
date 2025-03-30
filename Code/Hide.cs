using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    public bool t=true;
    public GameObject obj;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject obj4;
    public GameObject obj5;

    // Update is called once per frame
    public void Disappear()
    {
        t = !t;
        if(t == true)
        {
            obj.SetActive(true);
            obj2.SetActive(true);
            obj3.SetActive(false);
            obj4.SetActive(false);
            obj5.SetActive(true);
        }
        else
        {
            obj.SetActive(false);
            obj2.SetActive(false);
            obj3.SetActive(true);
            obj4.SetActive(true);
            obj5.SetActive(false);
        }
    }
}
