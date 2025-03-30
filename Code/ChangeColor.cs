using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Color newColor;

    public GameObject Funiture;
    public GameObject[] FunitureObject;

    public int count;

    private void Start()
    {
        Funiture = transform.parent.gameObject;
        Funiture = Funiture.transform.parent.gameObject;
        Funiture = Funiture.transform.parent.gameObject;

        if(Funiture.tag != "roomobj")
        {
            count = Funiture.transform.childCount - 3;
            FunitureObject = new GameObject[count + 1];
            for(int i = 0; i <= count;i++)
            {
                FunitureObject[i] = Funiture.transform.GetChild(i).gameObject;
            }
        }
    }

    public void ChangeRed()
    {
        newColor = Color.red;

        if(Funiture.tag != "roomobj")
        {
            for (int i = 0; i <= count; i++)
            {
                FunitureObject[i].GetComponent<MeshRenderer>().material.color = newColor;
            }
        }
        else
        {
            Funiture.GetComponent<MeshRenderer>().material.color = newColor;
        }
    }

    public void ChangeOrange()
    {
        newColor = new Color(1, 165 / 255f, 0);

        if(Funiture.tag != "roomobj")
        {
            for (int i = 0; i <= count; i++)
            {
                FunitureObject[i].GetComponent<MeshRenderer>().material.color = newColor;
            }
        }
        else
        {
            Funiture.GetComponent<MeshRenderer>().material.color = newColor;
        }
    }

    public void ChangeYellow()
    {
        newColor = Color.yellow;

        if(Funiture.tag != "roomobj")
        {
            for (int i = 0; i <= count; i++)
            {
                FunitureObject[i].GetComponent<MeshRenderer>().material.color = newColor;
            }
        }
        else
        {
            Funiture.GetComponent<MeshRenderer>().material.color = newColor;
        }
    }

    public void ChangeGreen()
    {
        newColor = Color.green;

        if(Funiture.tag != "roomobj")
        {
            for (int i = 0; i <= count; i++)
            {
                FunitureObject[i].GetComponent<MeshRenderer>().material.color = newColor;
            }
        }
        else
        {
            Funiture.GetComponent<MeshRenderer>().material.color = newColor;
        }
    }

    public void ChangeBlue()
    {
        newColor = Color.blue;

        if(Funiture.tag != "roomobj")
        {
            for (int i = 0; i <= count; i++)
            {
                FunitureObject[i].GetComponent<MeshRenderer>().material.color = newColor;
            }
        }
        else
        {
            Funiture.GetComponent<MeshRenderer>().material.color = newColor;
        }
    }

    public void ChangePurple()
    {
        newColor = new Color(0.5f, 0, 0.5f);

        if(Funiture.tag != "roomobj")
        {
            for (int i = 0; i <= count; i++)
            {
                FunitureObject[i].GetComponent<MeshRenderer>().material.color = newColor;
            }
        }
        else
        {
            Funiture.GetComponent<MeshRenderer>().material.color = newColor;
        }
    }

    public void ChangePink()
    {
        newColor = new Color(1, 192 / 255f, 203 / 255f);

        if(Funiture.tag != "roomobj")
        {
            for (int i = 0; i <= count; i++)
            {
                FunitureObject[i].GetComponent<MeshRenderer>().material.color = newColor;
            }
        }
        else
        {
            Funiture.GetComponent<MeshRenderer>().material.color = newColor;
        }
    }
}