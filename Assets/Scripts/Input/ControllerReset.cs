using UnityEngine;
using System.Collections;

public class ControllerReset : MonoBehaviour
{
	private SteamVR_TrackedObject trackedObj;
	private WinLose winLose;

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		winLose = FindObjectOfType<WinLose>();
	}

	void Update()
	{
		if (winLose.GetState() != WinLose.State.INPLAY)
		{
			var device = SteamVR_Controller.Input((int)trackedObj.index);

			if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
			{
				winLose.Restart();
			}
		}
	}
}
