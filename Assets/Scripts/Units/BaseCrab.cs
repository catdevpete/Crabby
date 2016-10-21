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

    bool canAtt;
    int team;
    float hp;
    float att;
    float mS;
    float aS;
    float range;
    State cState;
    GameObject target;
    Collider[] cols;

    public int GetTeam() { return team; }
    public float GetHp() { return hp; }
    
    public void SetHp(float _hp) { hp = _hp; }

    void Start()
    {
        canAtt = true;
        mS = 3;
        aS = 1;
        range = 2;
        cState = State.MOVE;
    }

    void Update()
    {
        Death();
        Detect();

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

    void OnCollisionEnter(Collision _col)
    {
        if (_col.gameObject.GetComponent<CrabNest>() != null)
        {
            _col.gameObject.GetComponent<CrabNest>().SetHp(_col.gameObject.GetComponent<CrabNest>().GetHp() - 1);
            Destroy(gameObject);
        }
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, enemyBase.transform.position, mS * Time.deltaTime);
    }

    void Attack()
    {
        if (target == null)
        {
            cState = State.MOVE;
        }

        if (canAtt)
        {
            target.GetComponent<BaseCrab>().SetHp(target.GetComponent<BaseCrab>().GetHp() - att);
            StartCoroutine(AttCycle(aS));
        }
    }

    void Death()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Detect()
    {
        cols = Physics.OverlapSphere(transform.position, range);

        foreach (Collider c in cols)
        {
            if (c.GetComponent<BaseCrab>() != null && c.GetComponent<BaseCrab>().GetTeam() != team)
            {
                target = c.gameObject;
                cState = State.ATTACK;
            }
        }
    }

    IEnumerator AttCycle(float _t)
    {
        canAtt = false;
        yield return new WaitForSeconds(_t);
        canAtt = true;
    }
}