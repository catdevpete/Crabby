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

	private SteamVR_TrackedObject trackedObj;
	private FixedJoint joint;
	private bool open;

	[SerializeField]
	private Rigidbody clawPhysics;
	[SerializeField]
	private Transform clawTransform;

	[SerializeField]
	private float _paddleMaxSpeed = 350f;
	public float PaddleMaxSpeed
	{
		get	{ return _paddleMaxSpeed; }
	}

	[SerializeField]
	private float _paddleMaxRotationSpeed = 10f;
	public float PaddleMaxRotationSpeed
	{
		get	{ return _paddleMaxRotationSpeed; }
	}

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	void Update()
	{
		var device = SteamVR_Controller.Input((int)trackedObj.index);

		if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
		{
			Time.timeScale = Time.timeScale == 1 ? 0 : 1;
		}

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
			colliders = colliders.Where(i => i.tag == "REDCRAB" || i.tag == "GREENCRAB" || i.tag == "Edible").OrderBy(i => (i.transform.position - attachPoint.position).magnitude).ToArray();

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
			if (prey.GetComponent<NavMeshAgent>())
			{
				prey.GetComponent<CrabOutfitScript>().enabled = false;
				prey.GetComponent<NavMeshAgent>().enabled = false;
				prey.GetComponent<Rigidbody>().isKinematic = false;
				prey.GetComponent<Rigidbody>().useGravity = true;
				prey.tag = "Edible";
			}

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

		SetPaddlePositionToController();
		SetPaddleRotationToController();
	}

	public void KillPrey()
	{
		Destroy(joint);
		joint = null;
		Destroy(prey.gameObject);
		prey = null;
	}

	private void SetPaddlePositionToController()
	{
		if ((clawTransform.position - transform.position).magnitude > 0.35f)
		{
			clawTransform.position = transform.position;
			clawPhysics.velocity = Vector3.zero;
		}
		else if ((clawTransform.position - transform.position).magnitude > 0.001f)
			clawPhysics.velocity = (transform.position - clawTransform.position) * PaddleMaxSpeed * Mathf.Clamp((clawTransform.position - transform.position).magnitude, 0.1f, 1f);
	}

	private void SetPaddleRotationToController()
	{
		if (PaddleAngularDifference() > 0.1f)
			clawTransform.rotation = Quaternion.RotateTowards(clawTransform.rotation, transform.rotation, PaddleMaxRotationSpeed);
	}

	private float PaddleAngularDifference()
	{
		return Quaternion.Angle(clawTransform.rotation, transform.rotation);
	}
}
