using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    public GameObject Funiture;

    private void Start()
    {
        Funiture = transform.parent.gameObject;
        Funiture = Funiture.transform.parent.gameObject;
    }

    public void mov()
    {
        Funiture.GetComponent<ObjectGenerator>().mov = true;
    }
}
