using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour {

	public float movingSpeed = 2.0f;
	public float maxTime = 3.0f;

	private float currentAnimationTime = 0.0f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () 
	{
		currentAnimationTime += Time.deltaTime;
		if (currentAnimationTime >= maxTime) 
		{
			Destroy (gameObject);
		} else {

			transform.position += transform.up * movingSpeed * Time.deltaTime;
		}
	}
}
