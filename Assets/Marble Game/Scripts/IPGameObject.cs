using UnityEngine;
using System.Collections;

public class IPGameObject : MonoBehaviour {

	private Vector3 savedPosition;
	private Vector3 savedAngles;
	private bool savedActive;


	public virtual void Start () {
		IPCheckpointManager.AddIPGameObject(this);

		SaveCheckpointState();
	}


	public virtual void SaveCheckpointState() {
		savedPosition = transform.position;
		savedAngles = transform.eulerAngles;
		savedActive = gameObject.activeSelf;
	}

	public virtual void RestoreCheckpointState() {
		transform.position = savedPosition;
		transform.eulerAngles = savedAngles;
		gameObject.SetActive(savedActive);
	}

}
