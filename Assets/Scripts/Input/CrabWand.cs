using UnityEngine;
using System.Linq;
using System.Collections;

public class CrabWand : MonoBehaviour
{
	public Transform tip;
	SteamVR_TrackedObject trackedObj;
	SpawnCrab spawnCrab;

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		spawnCrab = GetComponent<SpawnCrab>();
	}

	// Update is called once per frame
	void Update()
	{
		var device = SteamVR_Controller.Input((int)trackedObj.index);

		if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
		{
			Time.timeScale = Time.timeScale == 1 ? 0 : 1;
		}

		Collider[] colliders;
		if ((colliders = Physics.OverlapSphere(tip.position, 0.5f)).Length > 0)
		{
			colliders = colliders.Where(i => i.GetComponent<CrabUnitButton>()).OrderBy(i => (i.transform.position - tip.position).magnitude).ToArray();

			if (colliders.Length > 0)
			{
				CrabUnitButton button = colliders[0].GetComponent<CrabUnitButton>();
				
				if (button && device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
				{
					spawnCrab.SpawnPlayerCrab(button.UnitIndex, 5);
				}
			}
		}
	}
}
