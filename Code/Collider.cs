using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider : MonoBehaviour
{
    void OnCollisionEnter(Collision Floor)
    {    
        //if (Floor.gameObject.name == "Floor") 
        //{    
            Debug.Log("OK");
        //}
    }
}
