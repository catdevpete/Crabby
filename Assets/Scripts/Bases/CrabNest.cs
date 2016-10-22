using UnityEngine;
using System.Collections;

public class CrabNest : MonoBehaviour
{
    [SerializeField]
    int team;
    float hp;

    public float GetHp() { return hp; }

    public void SetTeam(int _team) { team = _team; }
    public void SetHp(float _hp) { hp = _hp; }

    void Start()
    {
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