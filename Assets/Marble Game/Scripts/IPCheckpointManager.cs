using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IPCheckpointManager : MonoBehaviour {


	static private List<IPGameObject> gameObjects = new List<IPGameObject>();


	static public void AddIPGameObject(IPGameObject obj) {
		if (!gameObjects.Contains(obj))
			gameObjects.Add(obj);
	}


	static public void RemoveIPGameObject(IPGameObject obj) {
		if (gameObjects.Contains(obj))
			gameObjects.Remove(obj);
	}


	static public void SaveCheckpointState() {
		foreach (IPGameObject obj in gameObjects) {
			if (obj != null)
				obj.SaveCheckpointState();
		}
	}


	static public void RestoreCheckpointState() {
		foreach (IPGameObject obj in gameObjects) {
			if (obj != null)
				obj.RestoreCheckpointState();
		}
	}

}
