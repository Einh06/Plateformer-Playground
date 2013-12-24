using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class EditorFolderHelper : MonoBehaviour {

	public static List<T> GetAllObjectsAtPath<T> (string path, bool recursive) where T : UnityEngine.Object
	{
		string[] files = Directory.GetFiles (path);
		var listObjects = new List<T> (); 


		foreach (string filePath in files) {

			var ressource = (T)AssetDatabase.LoadAssetAtPath (filePath, typeof(T));
			if (ressource != null)
				listObjects.Add (ressource);
		}

		if (recursive) {
			string[] directories = Directory.GetDirectories (path);
			foreach (string directoryPath in directories) {

				List<T> addionalObjects = GetAllObjectsAtPath<T> (directoryPath, recursive);
				listObjects.AddRange (addionalObjects);
			}
		}

		return listObjects;

	}

	public static List<UnityEngine.Object> GetUnityObjects (string path, bool recursive)
	{
		return GetAllObjectsAtPath<UnityEngine.Object> (path, recursive);
	}
}
