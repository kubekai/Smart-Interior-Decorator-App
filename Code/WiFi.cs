using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WiFi : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("SceneDesign");
    }
}
