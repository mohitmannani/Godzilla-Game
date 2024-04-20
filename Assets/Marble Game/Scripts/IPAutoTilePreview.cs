using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class IPAutoTilePreview : MonoBehaviour {

	[Tooltip("The size of the tiles.")]
	public float tileScale;


	void Update () {
		float x = transform.lossyScale.x * tileScale;
		float y = transform.lossyScale.z * tileScale;
		GetComponent<Renderer>().sharedMaterial.mainTextureScale = new Vector2(x, y);
	}
}
