using UnityEngine;
using System.Collections;

public class CoinBoxPop : MonoBehaviour {

	//the sprite that replace the box when activated
	public Sprite replacement;
	// the coin to show when poping
	public GameObject coin;

	public float coinVerticalStartPoint;
	public int numberOfRaycast = 6;
	public float raycastDistance = 1f;

	public LayerMask activatorMask;

	private SpriteRenderer spriteRenderer;
	private Vector2 raycastOrigin;
	private bool isEnabled = true;


	// Use this for initialization
	void Start () 
	{
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		raycastOrigin = new Vector2 (transform.position.x, transform.position.y);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isEnabled)
			CheckCollision ();
	}

	void CheckCollision ()
	{
		float raycastSeparatorWidth = spriteRenderer.bounds.size.x/numberOfRaycast ;
		Vector2 origin = raycastOrigin;

		for (int i = 0; i < raycastSeparatorWidth; i++) {

			DrawRay (new Vector3 (origin.x, origin.y, 0), Vector3.down, Color.red);
			bool raycastHit = Physics2D.Raycast (origin, -Vector2.up, raycastDistance, activatorMask);

			if (raycastHit) {

				isEnabled = false;
				Pop ();
				break;
			}
			origin.x += raycastSeparatorWidth;
		}
	}

	private void DrawRay (Vector3 start, Vector3 dir, Color color)
	{
		Debug.DrawRay (start, dir, color);
	}


	public void Pop ()
	{
		Debug.Log ("Pop!");
		spriteRenderer.sprite = replacement;
		GameObject myCoin = (GameObject)Instantiate (coin, 
		                                             transform.position + transform.up * coinVerticalStartPoint, 
		                                             Quaternion.identity);
	}
}
