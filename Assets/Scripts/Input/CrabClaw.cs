using UnityEngine;
using System.Linq;
using System.Collections;

public class CrabClaw : MonoBehaviour
{
	public Rigidbody attachPoint;
	public Rigidbody prey;

	public Transform pincer;
	public Transform openPincerRot;
	public Transform closedPincerRot;

	SteamVR_TrackedObject trackedObj;
	FixedJoint joint;
	bool open;

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	void Update()
	{
		var device = SteamVR_Controller.Input((int)trackedObj.index);

		if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
		{
			open = false;
		}

		if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
		{
			open = true;
		}

		if (open)
		{
			pincer.rotation = Quaternion.Lerp(pincer.rotation, openPincerRot.rotation, Time.deltaTime * 10);
		}
		else
		{
			pincer.rotation = Quaternion.Lerp(pincer.rotation, closedPincerRot.rotation, Time.deltaTime * 10);
		}
	}

	void FixedUpdate()
	{
		Collider[] colliders;
		if ((colliders = Physics.OverlapSphere(attachPoint.position, 0.05f)).Length > 0)
		{
			colliders = colliders.Where(i => i.tag == "Edible").OrderBy(i => (i.transform.position - attachPoint.position).magnitude).ToArray();

			if (colliders.Length > 0)
			{
				prey = colliders[0].GetComponent<Rigidbody>();
			}
			else
				prey = null;
		}
		else
			prey = null;

		var device = SteamVR_Controller.Input((int)trackedObj.index);

		if (prey && joint == null && device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
		{
			var go = prey.gameObject;

			joint = go.AddComponent<FixedJoint>();
			joint.connectedBody = attachPoint;
		}
		else if (joint != null && device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
		{
			var go = joint.gameObject;
			var rigidbody = go.GetComponent<Rigidbody>();
			Object.DestroyImmediate(joint);
			joint = null;

			// We should probably apply the offset between trackedObj.transform.position
			// and device.transform.pos to insert into the physics sim at the correct
			// location, however, we would then want to predict ahead the visual representation
			// by the same amount we are predicting our render poses.

			var origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
			if (origin != null)
			{
				rigidbody.velocity = origin.TransformVector(device.velocity);
				rigidbody.angularVelocity = origin.TransformVector(device.angularVelocity);
			}
			else
			{
				rigidbody.velocity = device.velocity;
				rigidbody.angularVelocity = device.angularVelocity;
			}

			rigidbody.maxAngularVelocity = rigidbody.angularVelocity.magnitude;
		}
	}

	public void KillPrey()
	{
		Destroy(joint);
		joint = null;
		Destroy(prey.gameObject);
		prey = null;
	}
}
