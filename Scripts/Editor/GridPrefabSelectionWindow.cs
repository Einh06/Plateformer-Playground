using UnityEngine;
using UnityEditor;
using System.Collections;

public class GridPrefabSelectionWindow : EditorWindow {


	public GameObject selectedPrefab;
	
	[MenuItem ("FM/GribPrefabWindow")]
	static void ShowWindow ()
	{
		EditorWindow.GetWindow (typeof(GridPrefabSelectionWindow));
	}
	
	void OnGUI ()
	{
		selectedPrefab = EditorGUILayout.ObjectField ("Object to paint",
		                                              selectedPrefab,
		                                              typeof(GameObject),
		                                              false) as GameObject;



	}
}