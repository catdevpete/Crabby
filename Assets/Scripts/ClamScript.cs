using UnityEngine;
using System.Collections;

public class ClamScript : MonoBehaviour
{
    Animator anim;
    bool isDead = false;
    float t = 4;

    public GameObject pearl, prefrab, enemyClam, hpBar;
    public float hp = 100.0f;
    public Transform[] spawns;
	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(isDead == false)
        {
            HandleDeath();
            Spawn();
            hpBar.transform.localScale = new Vector3(1, hp / 100, 1);
        }
	}

    void HandleDeath()
    {
        // if (hp < 100)
        //     hp += Time.deltaTime;
        // if (hp > 100)
        //     hp += 100 - hp;
        if (hp <= 0)
        {
            isDead = true;
            pearl.SetActive(true);
            anim.SetBool("isCracked", true);
        }
    }

    // Spawns new crabs
    void Spawn()
    {
        t += Time.deltaTime * 5;
        int i = Random.Range(0, spawns.Length);
        if(t >= 5)
        {
            GameObject crab = Instantiate(prefrab, spawns[i].transform.position, Quaternion.identity) as GameObject;
            crab.GetComponent<CrabOutfitScript>().goal = enemyClam.transform;
            t = 0;
        }
    }

    public void Hit(float _dmg)
    {
        hp -= _dmg * Time.deltaTime;
    }
}
