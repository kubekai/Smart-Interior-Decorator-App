using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor1 : MonoBehaviour
{
    public Color newColor;

    public string tag;

    public GameObject Funiture;

    public void ChangeRed()
    {
        Funiture = GameObject.FindWithTag(tag);

        newColor = Color.red;

        Funiture.GetComponent<MeshRenderer>().material.color = newColor;
    }

    public void ChangeOrange()
    {
        Funiture = GameObject.FindWithTag(tag);

        newColor = new Color(1, 165 / 255f, 0);

        Funiture.GetComponent<MeshRenderer>().material.color = newColor;
    }

    public void ChangeYellow()
    {
        Funiture = GameObject.FindWithTag(tag);

        newColor = Color.yellow;

        Funiture.GetComponent<MeshRenderer>().material.color = newColor;
    }

    public void ChangeGreen()
    {
        Funiture = GameObject.FindWithTag(tag);

        newColor = Color.green;

        Funiture.GetComponent<MeshRenderer>().material.color = newColor;
    }

    public void ChangeBlue()
    {
        Funiture = GameObject.FindWithTag(tag);

        newColor = Color.blue;

        Funiture.GetComponent<MeshRenderer>().material.color = newColor;
    }

    public void ChangePurple()
    {
        Funiture = GameObject.FindWithTag(tag);

        newColor = new Color(0.5f, 0, 0.5f);

        Funiture.GetComponent<MeshRenderer>().material.color = newColor;
    }

    public void ChangePink()
    {
        Funiture = GameObject.FindWithTag(tag);

        newColor = new Color(1, 192 / 255f, 203 / 255f);

        Funiture.GetComponent<MeshRenderer>().material.color = newColor;
    }
}