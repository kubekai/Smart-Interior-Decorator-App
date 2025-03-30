using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewLock : MonoBehaviour
{
    // Start is called before the first frame update
    public void Lock()
    {
        GlobalValue.ViewLock = true;
    }

    // Update is called once per frame
    public void UnLock()
    {
        GlobalValue.ViewLock = false;
    }
}
