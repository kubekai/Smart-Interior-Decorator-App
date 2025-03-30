using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
    public GameObject Camera;
    public GameObject Up;
    public GameObject UpExtend;
    public GameObject Down;
    public GameObject DownExtend;

    public void CLock()
    {
        GlobalValue.CameraLock = true;
    }
    public void CUnLock()
    {
        GlobalValue.CameraLock = false;
    }
    public void SetTF()
    {
        if(GlobalValue.CameraLock == true)
        {
            Camera.SetActive(true);
            Up.SetActive(false);
            UpExtend.SetActive(false);
            Down.SetActive(true);
            DownExtend.SetActive(true);
        }
        else
        {
            Camera.SetActive(false);
            Up.SetActive(true);
            UpExtend.SetActive(true);
            Down.SetActive(false);
            DownExtend.SetActive(false);
        }
    }
}
