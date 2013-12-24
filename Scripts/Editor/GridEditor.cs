using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof (Grid))]
public class GridEditor : Editor {

	Grid grid;
	string paintingChar = "";

	bool isPaintingPressed = false;
	Transform parentObject;

	public void OnEnable ()
	{
		grid = (Grid)target;
		SceneView.onSceneGUIDelegate = GripGUIUpdate;
	}

	public override void OnInspectorGUI ()
	{
		GUILayout.BeginHorizontal();
		GUILayout.Label(" Grid Width ");
		grid.tileWidth = EditorGUILayout.FloatField ( grid.tileWidth, GUILayout.Width(50));
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
		GUILayout.Label (" Grid Height ");
		grid.tileHeight = EditorGUILayout.FloatField ( grid.tileHeight, GUILayout.Width(50));
		GUILayout.EndHorizontal();

		grid.selectedObject = EditorGUILayout.ObjectField ("Object to paint",
		                                                   grid.selectedObject, 
		                                                   typeof(GameObject),
		                                                   false) as GameObject;

		paintingChar = GUILayout.TextField (paintingChar, 1);

		SceneView.RepaintAll ();
	}

	void GripGUIUpdate (SceneView sceneView)
	{
		Event e = Event.current;

		Ray ray = HandleUtility.GUIPointToWorldRay (e.mousePosition);
		Vector3 newMousePos = ray.origin;

		int xIndex = Mathf.FloorToInt (newMousePos.x / grid.tileWidth);
		int yIndex = Mathf.FloorToInt (newMousePos.y / grid.tileHeight);

		if (paintingChar.Length > 0) {
			if (e.isKey && 
			    e.type == EventType.KeyDown && 
			    e.character == paintingChar[0]) {

				isPaintingPressed = !isPaintingPressed;
			}
		}

		if (isPaintingPressed && e.type == EventType.mouseMove) {
			if (grid.selectedObject) {
				
				this.CreateObjectAtPosition (new Vector2 (xIndex, yIndex), grid.selectedObject);
			}
		}

		if (e.type == EventType.mouseDown && e.button == 1) {

			Debug.Log ("xIndex: " + xIndex + " ; yIndex: " + yIndex);

			if (grid.selectedObject) {

				CreateObjectAtPosition (new Vector2 (xIndex, yIndex), grid.selectedObject);
			}
		}

		SceneView.RepaintAll ();
	}

	GameObject CreateObjectAtPosition (Vector2 position, GameObject objectType)
	{
		GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab (objectType);
		string name = ("GO_" + position.x + "_" + position.y + "_" + obj.layer);


		GameObject foundObject = GameObject.Find (name);
		if (foundObject) {
			DestroyImmediate (obj);
			return null;
		}
		
		obj.name = name;
		
		Vector3 aligned = new Vector3((position.x * grid.tileWidth) + (grid.tileWidth / 2f), 
		                              (position.y * grid.tileHeight) + (grid.tileHeight / 2f));
		
		obj.transform.position = aligned;

		return obj;
	}
}
