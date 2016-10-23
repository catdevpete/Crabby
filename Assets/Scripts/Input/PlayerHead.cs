using UnityEngine;
using System.Linq;
using System.Collections;

public class PlayerHead : MonoBehaviour
{
	public WinLose winLose;
	public float hp = 10;

    AudioSource crunch;

	void Start()
	{
        crunch = GetComponent<AudioSource>();
		winLose = FindObjectOfType<WinLose>();
		StartCoroutine(CheckHealth());
	}

	IEnumerator CheckHealth()
	{
		while (true)
		{
			if (hp <= 0)
			{
				Lose();
				yield break;
			}

			yield return null;
		}
	}

	public void Win()
	{
		winLose.SetState(WinLose.State.WIN);
	}

	void Lose()
	{
		winLose.SetState(WinLose.State.LOSE);

		CrabClaw[] claws = FindObjectsOfType<CrabClaw>();
		Transform[] clawObjects = claws.Select(i => i.clawTransform).ToArray();

		foreach (var item in claws)
		{
			item.enabled = false;
		}

		foreach (var item in clawObjects)
		{
			item.gameObject.SetActive(false);
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		CrabClaw claw = collider.GetComponent<CrabClaw>();

		if (claw && claw.prey && claw.prey.tag == "Edible" && transform.localScale.x < 3)
		{
			claw.KillPrey();
			StartCoroutine(Growth());
            crunch.Play();
		}
	}

	IEnumerator Growth()
	{
		Vector3 origSize = transform.root.localScale;
		Vector3 newSize = transform.root.localScale + Vector3.one * 0.10f;

		for (float i = 0; i < 1; i += Time.deltaTime)
		{
			transform.root.localScale = Vector3.Lerp(origSize, newSize, Mathf.SmoothStep(0, 1, i));
			hp += 10;
			yield return null;
		}
	}

	public void Hit(float damage)
	{
		hp -= damage;
	}
}
