using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	public float tileWidth = 32f;
	public float tileHeight = 32f;

	public GameObject selectedObject;

	void OnDrawGizmos ()
	{
		Vector3 pos = Camera.current.transform.position;
		
		for (float y = pos.y - 800.0f; y < pos.y + 800.0f; y+= tileHeight)
		{
			Gizmos.DrawLine(new Vector3(-1000000.0f, Mathf.Floor(y/tileHeight) * tileHeight, 0.0f),
			                new Vector3(1000000.0f, Mathf.Floor(y/tileHeight) * tileHeight, 0.0f));
		}
		
		for (float x = pos.x - 1200.0f; x < pos.x + 1200.0f; x+= tileWidth)
		{
			Gizmos.DrawLine(new Vector3(Mathf.Floor(x/tileWidth) * tileWidth, -1000000.0f, 0.0f),
			                new Vector3(Mathf.Floor(x/tileWidth) * tileWidth, 1000000.0f, 0.0f));
		}
	}


}
