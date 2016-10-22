using UnityEngine;
using System.Collections;

public class BaseCrab : MonoBehaviour
{
    public enum State
    {
        MOVE,
        ATTACK
    }

    protected bool canAtt;
    protected int team;
    protected float hp;
    protected float att;
    protected float mS;
    protected float aS;
    protected float range;
    protected State cState;
    GameObject target;
    GameObject enemyBase;
    Collider[] cols;

    public int GetTeam() { return team; }
    public float GetHp() { return hp; }
    
    public void SetTeam(int _team) { team = _team; }
    public void SetHp(float _hp) { hp = _hp; }
    public void SetEnemyBase(GameObject _enemyBase) { enemyBase = _enemyBase; }

    protected virtual void Start()
    {
        canAtt = true;
        att = 5;
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
        if (_col.gameObject.GetComponent<CrabNest>() != null && _col.gameObject.GetComponent<CrabNest>().GetTeam() != team)
        {
            _col.gameObject.GetComponent<CrabNest>().SetHp(_col.gameObject.GetComponent<CrabNest>().GetHp() - 1);
            Destroy(gameObject);
        }
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, enemyBase.transform.position, mS * Time.deltaTime / 10);
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
			Debug.Log("KO'ed");
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