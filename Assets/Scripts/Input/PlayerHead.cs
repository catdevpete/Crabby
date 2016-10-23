using UnityEngine;
using System.Collections;

public class PlayerHead : MonoBehaviour
{
	public float hp;

	void OnTriggerEnter(Collider collider)
	{
		CrabClaw claw = collider.GetComponent<CrabClaw>();

		if (claw && claw.prey && claw.prey.tag == "Edible")
		{
			claw.KillPrey();
			StartCoroutine(Growth());
		}
	}

	IEnumerator Growth()
	{
		Vector3 origSize = transform.root.localScale;
		Vector3 newSize = transform.root.localScale + Vector3.one * 0.10f;

		for (float i = 0; i < 1; i += Time.deltaTime)
		{
			transform.root.localScale = Vector3.Lerp(origSize, newSize, Mathf.SmoothStep(0, 1, i));
			yield return null;
		}
	}

	public void Hit(float damage)
	{
		hp -= damage;
	}
}
