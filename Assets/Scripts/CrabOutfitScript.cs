using UnityEngine;
using System.Collections;

public class CrabOutfitScript : MonoBehaviour
{
    // Animator anim;
    public bool isLight, isHeavy, isRanged;

    public GameObject[] augs;

	// Use this for initialization
	void Start ()
    {
        // anim = GetComponent<Animator>();

        float r = Random.Range(0.0f, 1.0f);
        float c = 0.6f;
        foreach(var aug in augs)
        {
            if(r < c)
            {
                c -= 0.2f;
                aug.SetActive(true);
            }
        }
        if(isHeavy == true)
        {
            transform.localScale *= 1.5f;
        }
	}

    // void MoveTest()
    // {
    //     if (Input.GetKey(KeyCode.LeftArrow))
    //     {
    //         anim.SetBool("isStrafing", true);
    //         anim.SetFloat("moveSpeed", -1);
    //     }
    //     else if (Input.GetKey(KeyCode.RightArrow))
    //     {
    //         anim.SetBool("isStrafing", true);
    //         anim.SetFloat("moveSpeed", 1);
    //     }
    //     else
    //         anim.SetBool("isStrafing", false);
    // 
    //     if (Input.GetKey(KeyCode.UpArrow))
    //     {
    //         anim.SetBool("isWalking", true);
    //         anim.SetFloat("moveSpeed", 1);
    //     }
    //     else if (Input.GetKey(KeyCode.DownArrow))
    //     {
    //         anim.SetBool("isWalking", true);
    //         anim.SetFloat("moveSpeed", -1);
    //     }
    //     else
    //         anim.SetBool("isWalking", false);
    // 
    //     if (Input.GetMouseButton(0))
    //     {
    //         anim.SetBool("isPinching", true);
    //     }
    //     else
    //         anim.SetBool("isPinching", false);
    //     if (Input.GetMouseButton(1))
    //     {
    //         anim.SetBool("isSlamming", true);
    //     }
    //     else
    //         anim.SetBool("isSlamming", false);
    // 
    //     if (Input.GetMouseButton(2))
    //     {
    //         anim.SetBool("isTossing", true);
    //     }
    //     else
    //         anim.SetBool("isTossing", false);
    // }
}
