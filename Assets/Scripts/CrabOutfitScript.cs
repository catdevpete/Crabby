using UnityEngine;
using System.Collections;

public class CrabOutfitScript : MonoBehaviour
{
    #region Variables
    public bool isHeavy, isRanged;
    public GameObject[] augs;
    public float hp = 10;
    public Transform target, goal;
    public GameObject projectile;

    Animator anim;
    float power = 2;
    #endregion

    // IT'S START YO
    void Start ()
    {
        Dress();
        HandleType();

        anim = GetComponent<Animator>();

        target = goal;

        GetComponent<NavMeshAgent>().SetDestination(target.position);
	}

    // IT'S ONTRIGGERSTAY YO
    // Makes the crabs attack each other and the enemy clam
    void OnTriggerStay(Collider col)
    {
        if((tag == "REDCRAB" && col.tag == "GREEN") || (tag == "GREENCRAB" && col.tag == "RED"))
        {
            transform.LookAt(col.transform);
            Attack();
            col.transform.parent.GetComponent<CrabOutfitScript>().Hit(power * Time.deltaTime);
            target = col.transform;
            GetComponent<NavMeshAgent>().SetDestination(transform.position);
        }

        if ((tag == "REDCRAB" && col.tag == "GREENCLAM") || (tag == "GREENCRAB" && col.tag == "REDCLAM"))
        {
            target = col.transform;
            transform.LookAt(target);
            Attack();
            col.GetComponent<ClamScript>().Hit(power * Time.deltaTime);
            GetComponent<NavMeshAgent>().SetDestination(transform.position);
        }
    }

    // IT'S ONTRIGGER EXIT YO
    // Makes the crabs start moving again if their combatant wanders off
    void OnTriggerExit(Collider col)
    {
        if ((tag == "RED" && col.tag == "GREEN") || (tag == "GREEN" && col.tag == "RED"))
        {
            if(target != null)
                GetComponent<NavMeshAgent>().SetDestination(target.position);
            else
                GetComponent<NavMeshAgent>().SetDestination(goal.position);
            Walk();
        }
    }

    // IT'S UPDATE YO
    // Matches the walk animation's speed to the crab's movement speed
    // kills the crab if it runs out of health
    void Update()
    {
        anim.SetFloat("moveSpeed", GetComponent<NavMeshAgent>().velocity.magnitude);
        Repath();
        
        if (hp <= 0)
        {
            // Instantiate(corpse, transform.position, Quaternion.identity);         -------------------        FOR LATER
            Destroy(gameObject);
        }
    }

    // gets the crabs moving again if their enemy dies
    void Repath()
    {
        if (target == null)
        {
            target = goal;
            GetComponent<NavMeshAgent>().SetDestination(target.position);
            Walk();
        }
    }

    // Tells the animator to do the attack animation
    void Attack()
    {
        if(isHeavy)
            anim.SetBool("isSlamming", true);
        else if(isRanged)
        {
            projectile.SetActive(true);
            anim.SetBool("isTossing", true);
        }
        else
            anim.SetBool("isPinching", true);
    }

    // Tells the animator to stop doing the attack animation
    void Walk()
    {
        if (isHeavy)
            anim.SetBool("isSlamming", false);
        else if (isRanged)
        {
            projectile.SetActive(false);
            anim.SetBool("isTossing", false);
        }
        else
            anim.SetBool("isPinching", false);
    }

    // Puts on the crab's fancy duds, or not
    void Dress()
    {
        float r = Random.Range(0.0f, 1.0f);
        float c = 0.6f;
        foreach (var aug in augs)
        {
            if (r < c)
            {
                c -= 0.2f;
                aug.SetActive(true);
            }
        }
    }

    // Modifies the stats of special crabs
    void HandleType()
    {
        int i = Random.Range(0, 4);
        if (i == 2)
            isHeavy = true;
        if (i == 3)
            isRanged = true;

        if (isHeavy == true)
        {
            transform.localScale *= 1.5f;
            hp = 15;
            power = 3;
            GetComponent<NavMeshAgent>().speed = 0.3f;
            GetComponent<NavMeshAgent>().avoidancePriority = 1;
        }
        if (isRanged == true)
        {
            GetComponent<BoxCollider>().size = new Vector3(0.3f, 0.3f, 1);
            GetComponent<BoxCollider>().center = new Vector3(0, 0, 0.4f);

            GetComponent<NavMeshAgent>().avoidancePriority = 99;
        }
    }

    // Deals damage to the crab
    public void Hit(float dmg)
    {
        hp -= dmg;
    }
}
