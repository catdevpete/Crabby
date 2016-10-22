using UnityEngine;
using System;
using System.Collections;

public class CrabUnitButton : MonoBehaviour
{
	[SerializeField]
	private int unitIndex = 0;
	public int UnitIndex { get; set; }

	void Update()
	{
		if (Camera.main)
			transform.rotation = Camera.main.transform.rotation;
	}
}
