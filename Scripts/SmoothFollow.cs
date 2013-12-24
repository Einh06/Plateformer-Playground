using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour
{
	public Transform target;
	public new Transform transform;
	public Vector2 cameraOffset;
	public Vector2 minXAndY;
	public Vector2 maxXAndY;

	void Awake ()
	{
		transform = gameObject.transform;
	}

	public void LateUpdate ()
	{
		var offset = target.position - transform.position;
		Vector3 newPos = transform.position;
		if (Mathf.Abs (offset.x) > cameraOffset.x) {
			float newX = Mathf.Sign (offset.x) * (Mathf.Abs (offset.x) - cameraOffset.x);
			newPos.x += newX;
			newPos.x = Mathf.Clamp (newPos.x, minXAndY.x, maxXAndY.x);
		}
			
		if (Mathf.Abs (offset.y) > cameraOffset.y) {
			float newY = Mathf.Sign (offset.y) * (Mathf.Abs (offset.y) - cameraOffset.y);
			newPos.y += newY;
			newPos.y = Mathf.Clamp (newPos.y, minXAndY.y, maxXAndY.y);
		}

		transform.position = newPos;
	}
}
