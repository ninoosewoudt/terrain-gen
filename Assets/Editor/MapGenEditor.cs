using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (MapGen))]
public class MapGenEditor : Editor {

	public override void OnInspectorGUI() {
		MapGen mapGen = (MapGen)target;

		if (DrawDefaultInspector ()) {
			if (mapGen.AutoUpdate) {
				mapGen.GenMap ();
			}
		}

		if (GUILayout.Button ("Generate")) {
			mapGen.GenMap ();
		}
	}
}