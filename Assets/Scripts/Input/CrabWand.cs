﻿using UnityEngine;
using System.Collections;

public class CrabWand : MonoBehaviour
{
	SteamVR_TrackedObject trackedObj;

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	// Update is called once per frame
	void Update()
	{
		var device = SteamVR_Controller.Input((int)trackedObj.index);

		if (device.GetTouchDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
		{
			Time.timeScale = Time.timeScale == 1 ? 0 : 1;
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		/*
		if (collision.gameObject.tag == "base")
		{
			Base base = collision.gameObject.GetComponent<Base>();
			base.Spawn();
		}
		*/
	}
}