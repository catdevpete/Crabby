using UnityEngine;
using System.Collections;

public class BaseCrab : MonoBehaviour
{
    public enum State
    {
        MOVE,
        ATTACK
    }

    [SerializeField]
    GameObject enemyBase;

    int team;
    float health;
    float att;
    float mS;
    float range;
    State cState;
    Collider[] cols;

    public int GetTeam() { return team; }

    void Start()
    {
        
    }

    void Update()
    {
        switch (cState)
        {
            case State.MOVE:

                Move();

                break;

            case State.ATTACK:

                Attack();

                break;
        }
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, enemyBase.transform.position, mS * Time.deltaTime);
    }

    void Attack()
    {

    }

    void Detect()
    {
        cols = Physics.OverlapSphere(transform.position, range);

        foreach (Collider c in cols)
        {
            if (c.GetComponent<BaseCrab>() != null && c.GetComponent<BaseCrab>().GetTeam() != team)
            {
                cState = State.ATTACK;
            }
        }
    }
}