using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Square : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("Square");
    }
}
