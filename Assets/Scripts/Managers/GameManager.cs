using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public string winScene;
	public string loseScene;

	public void TriggerWin()
	{
		SceneManager.LoadScene(winScene);
	}

	public void TriggerLose()
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