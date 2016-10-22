using UnityEngine;
using System.Collections;

public class CrabNest : MonoBehaviour
{
    [SerializeField]
    int team;

	[SerializeField]
	float hp;

    public float GetHp() { return hp; }
	public void SetHp(float _hp) { hp = _hp; }

	public int GetTeam() { return team; }
	public void SetTeam(int _team) { team = _team; }

    void Start()
    {
		if (hp < 0)
			hp = 1;
    }

    void Update()
    {
        Lose();
    }

    void Lose()
    {
        if (hp <= 0)
        {
            if (team == 1)
            {
                GameManager.LoseTrigger();
            }

            else
            {
                GameManager.WinTrigger();
            }

            Destroy(gameObject);
        }
    }
}