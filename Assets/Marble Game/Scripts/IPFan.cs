using UnityEngine;
using System.Collections;

public class IPFan : IPSwitchableObject {

	public GameObject blades;
	public IPForceTrigger forceTrigger;

	private float bladeSpeed;
	private float bladeSlowSpeed = 300.0f;


	public override void Start () {
		base.Start();
	}
	
	void Update () {

		forceTrigger.enabled = activated;


		if (activated) {
			if (!GetComponent<AudioSource>().isPlaying)
				GetComponent<AudioSource>().Play();
			bladeSpeed = 850.0f;
		} else {
			if (bladeSpeed > 0)
				bladeSpeed -= bladeSlowSpeed * Time.deltaTime;
			if (GetComponent<AudioSource>().isPlaying)
			    GetComponent<AudioSource>().Stop();
			bladeSpeed = Mathf.Clamp(bladeSpeed, 0.0f, 850.0f);
		}

		blades.transform.Rotate(new Vector3(0, 0, 1), bladeSpeed * Time.deltaTime);
	}

	public override void SaveCheckpointState () {
		base.SaveCheckpointState();
	}
	
	public override void RestoreCheckpointState () {
		base.RestoreCheckpointState();
	}
}
