using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Design : MonoBehaviour
{
	public void SceneChange()
    {
        SceneManager.LoadScene("Scene2");
    }
}
