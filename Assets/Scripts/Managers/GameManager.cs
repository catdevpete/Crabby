using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public string winScene = "WinScreen";
	public string loseScene = "LoseScreen";

	public void WinTrigger()
	{
		SceneManager.LoadScene(winScene);
	}

	public void LoseTrigger()
	{
		SceneManager.LoadScene(loseScene);
	}

	/*
	 * 
	void Update()
	{
		if ()
		{

		}
		else if ()
		{

		}
		else
		{

		}
	}
	*/
}