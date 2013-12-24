using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public int score = 0;

	public void AddPoint (int points)
	{
		score += points;
		UpdateUI ();
	}

	public void UpdateUI ()
	{
		Debug.Log ("Score: " + score);
	}
}
