using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{

    public Camera Camera1;
    public Camera Camera2;
    public Camera Camera3;
    public Camera Camera4;

    public void switchcma(int x)
    {
        deactivateall();
        if(x == 1)
        {
            Camera1.enabled = true;
        }
        else if(x == 2)
        {
            Camera2.enabled = true;
        }
        else if(x == 3)
        {
            Camera3.enabled = true;
        }
        else
        {
            Camera4.enabled = true;
        }
    }

    public void deactivateall()
    {
        Camera1.enabled = false;
        Camera2.enabled = false;
        Camera3.enabled = false;
        Camera4.enabled = false;
    }
}
