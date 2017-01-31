using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(LevelLoader))]
public class LevelLoaderEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.HelpBox("Destroy Level will only work in play mode", MessageType.Info);
        LevelLoader myScript = (LevelLoader)target;
        if (GUILayout.Button("Build Level"))
        {
            myScript.LoadMap();
        }
        if (GUILayout.Button("Destroy Level"))
        {
            myScript.EmptyMap();
        }
    }
}
