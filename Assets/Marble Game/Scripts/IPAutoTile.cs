using UnityEngine;

public class IPAutoTile : MonoBehaviour
{
    [Tooltip("The size of the tiles.")]
    public float tileScale;

    private Vector3 prevScale;
    private float prevTileScale;

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.lossyScale != prevScale || tileScale != prevTileScale)
        {
            float x = transform.lossyScale.x * tileScale;
            float y = transform.lossyScale.z * tileScale;
            GetComponent<Renderer>().material.mainTextureScale = new Vector2(x, y);

            prevScale = transform.lossyScale;
            prevTileScale = tileScale;
        }
    }
}
