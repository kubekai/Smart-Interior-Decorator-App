using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    public string tag;

    public GameObject Funiture;

    public Material material0;

    public void ChangeMateriall()
    {
        Funiture = GameObject.FindWithTag(tag);

        Funiture.GetComponent<MeshRenderer>().material = material0;
    }

    
}