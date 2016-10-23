using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class WinLose : MonoBehaviour
{
    public enum State
    {
        INPLAY,
        WIN,
        LOSE
    }

    [SerializeField]
    Text text;

    [SerializeField]
    Canvas canvas;

    State state = State.INPLAY;

    void OnGUI()
    {
        if (state == State.WIN)
        {
            Win();
        }

        else if (state == State.LOSE)
        {
            Lose();
        }
    }

    public State GetState() { return state; }
    public void SetState(State _state) { state = _state; }

	void Win()
    {
        canvas.gameObject.SetActive(true);
        text.text = "You Win!";
    }

    void Lose()
    {
        canvas.gameObject.SetActive(true);
        text.text = "You Lose!";
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainPete2");
        canvas.gameObject.SetActive(false);
    }
}