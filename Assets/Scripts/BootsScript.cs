using UnityEngine;
using System.Collections;

public class BootsScript : MonoBehaviour
{
    public GameObject[] boots;
	// Use this for initialization
	void Start ()
    {
	    foreach(var boot in boots)
        {
            boot.SetActive(true);
        }
	}
}
