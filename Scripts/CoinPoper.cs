using UnityEngine;
using System.Collections;

public class CoinPoper : MonoBehaviour {

	public CoinBoxPop box;

	private bool isPoped = false;

	// Use this for initialization
	void Start () 
	{

	}


	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnCollisionEnter2D (Collision2D collision) 
	{
		Debug.Log ("Collision");
		if (!isPoped && collision.gameObject.tag == "Player") {

			isPoped = true;
			box.Pop ();
		}
	}
}
