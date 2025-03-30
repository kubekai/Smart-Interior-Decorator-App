using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Scene2 : MonoBehaviour
{
	public static Scene2 scene2;
	public TMP_InputField inputField;

	public string user_obj;
	private void Awake()
	{
		if(scene2 == null)
		{
			scene2 = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
	public void SetUserObj()
	{
		user_obj = inputField.text;
		SceneManager.LoadSceneAsync("Room");
	}
}
