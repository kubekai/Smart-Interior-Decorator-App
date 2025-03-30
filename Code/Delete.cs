using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour
{
    //public GameObject thing;


    public void Del()
    {
        
        Destroy(transform.parent.gameObject);
        GlobalValue.Lock1 = false;
    }
}
