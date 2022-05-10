using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIStartButton : MonoBehaviour
{
	public void TaskOnClick()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
