using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static string winScene = "WinScreen";
	public static string loseScene = "LoseScreen";

	public static void WinTrigger()
	{
		SceneManager.LoadScene(winScene);
	}

	public static void LoseTrigger()
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