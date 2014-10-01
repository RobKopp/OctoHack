using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(EditorControls))]
public class EditorControlsEditor: Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		
		EditorControls levelRoot = (EditorControls)target;
		if(GUILayout.Button("Export Level"))
		{
			levelRoot.ExportLevel();
		}

		if(GUILayout.Button ("Load Level")) {
			levelRoot.LoadLevel("");
		}
	}

}
