using UnityEngine;
using System.Collections;

public class CrabNest : MonoBehaviour
{
    float hp;

    public float GetHp() { return hp; }

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
            Destroy(gameObject);
            // GG;
        }
    }
}