using UnityEngine;
using System.Collections;

public class IPCollapser : IPSwitchableObject {

	[Tooltip("The delay in seconds before the object falls after it is touched.")]
	public float fallDelay;
	[Tooltip("The angle that the object rocks back and forth at before falling.")]
	public float rockAngle;
	[Tooltip("The time in seconds between rocking motions.")]
	public float rockTime;

	private float savedFallDelay;
	private float savedRockAngle;
	private float savedRockTime;

	private bool falling;


	public override void Start () {
		base.Start();
		SaveCheckpointState();
	}


	void Update () {
		if (transform.position.y < MKSceneManager.instance.floor) 
			GetComponent<Rigidbody>().isKinematic = true;
	}

	void OnCollisionEnter(Collision other) {
		if (MKSceneManager.instance.inputLocked || !activated)
			return;


		if (other.gameObject.name.Contains("IPPlayer") && !falling) {
			GetComponent<AudioSource>().Play();
			falling = true;
			StartCoroutine("Collapse");
		}
	}

	IEnumerator Collapse() {
		float startTime = Time.time;
		float rockTimer = 0;


		while (GetComponent<Rigidbody>().isKinematic) {
			rockTimer += Time.deltaTime;
			float phase = Mathf.Sin(rockTimer / rockTime);
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, phase * rockAngle);
			if (Time.time - startTime > fallDelay)
				GetComponent<Rigidbody>().isKinematic = false;
			yield return null;
		}
	}


	public override void SaveCheckpointState () {
		base.SaveCheckpointState();

		savedRockTime = rockTime;
		savedRockAngle = rockAngle;
		savedFallDelay = fallDelay;
		
	}
	
	public override void RestoreCheckpointState () {
		base.RestoreCheckpointState();
	
		fallDelay = savedFallDelay;
		rockAngle = savedRockAngle;
		rockTime = savedRockTime;

		GetComponent<Rigidbody>().isKinematic = true;
		falling = false;
	}

}
