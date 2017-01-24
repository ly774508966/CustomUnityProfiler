using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class CustomProfiler : EditorWindow
{
    public int m_triangleLimit, m_verticesLimit;
    public GameObject[] m_gameObjects;
    private Vector2 m_scrollPos;
    private bool m_displayMeshLimits = false;
    private bool m_ignoreLimits = false;

    [MenuItem("Window/Custom Profiler %#&P")]

    public static void ShowWindow()
    {
        Debug.Log("Custom Profiler Pressed");
        EditorWindow.GetWindowWithRect(typeof(CustomProfiler), new Rect(0, 0, 600, 800));
    }

    void OnEnable()
    {
        GetObjects();
    }

    void OnGUI()
    {
        // The actual window code goes here
        if (GUILayout.Button("Get All Objects", GUILayout.Width(200), GUILayout.Height(20)))
        {
            GetObjects();

        }

        m_ignoreLimits = EditorGUILayout.Toggle("Ignore all limits", m_ignoreLimits);
        m_triangleLimit = EditorGUILayout.IntField("Triangle Limit", m_triangleLimit);
        m_verticesLimit = EditorGUILayout.IntField("Vertices Limit", m_verticesLimit);

        if (m_gameObjects != null)
        {
            m_scrollPos = EditorGUILayout.BeginScrollView(m_scrollPos);
            //GetObjects();
            DisplayObjects();
            EditorGUILayout.EndScrollView();
        }
    }

    private void OnHierarchyChange()
    {
        Debug.Log("Hierarchy has changed");
    }

    void GetObjects()
    {
        Debug.Log("Getting All Objects");
        m_gameObjects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
    }

    void DisplayObjects()
    {
        if (m_gameObjects.Length > 0)
        {
            //m_gameObjects.Sort();
            for (int i = 0; i < m_gameObjects.Length; i++)
            {
                Debug.Log("Game Object: " + m_gameObjects[i].name);
                if (m_gameObjects[i].gameObject != null)
                {
                    EditorGUILayout.ObjectField(m_gameObjects[i].name, m_gameObjects[i], typeof(GameObject), true);
                    if (m_gameObjects[i].GetComponent<MeshFilter>())
                    {
                        DisplayMeshData(i);
                    }
                }
            }
        }
    }


    void DisplayMeshData(int _index)
    {
        Debug.Log("Has Mesh");
        int tempVertex = m_gameObjects[_index].gameObject.GetComponent<MeshFilter>().mesh.vertexCount;
        int tempTriangleCount = m_gameObjects[_index].gameObject.GetComponent<MeshFilter>().mesh.triangles.Length;
        if (tempVertex > 0)
        {
            if (!m_ignoreLimits)
            {
                if (tempVertex > m_verticesLimit)
                {
                    Debug.Log("Too many vertices");
                    EditorGUILayout.IntField("Vertices Count: ", tempVertex);
                    EditorGUILayout.HelpBox("Warning Object has too many vertices", MessageType.Warning);
                }
            }
            else
            {
                Debug.Log("Has vertices");
                EditorGUILayout.IntField("Vertex Count: ", tempVertex);
            }
            Debug.Log("Has Vertices");
            EditorGUILayout.IntField("Vertexes: ", tempVertex);
        }
        if (tempTriangleCount > 0)
        {
            if (!m_ignoreLimits)
            {
                if (tempTriangleCount > m_triangleLimit)
                {
                    Debug.Log("Too many triangles");
                    EditorGUILayout.IntField("Triangle Count: ", tempTriangleCount);
                    EditorGUILayout.HelpBox("Warning Object has too many triangles", MessageType.Warning);
                }
            }
            else
            {
                Debug.Log("Has Triangles");
                EditorGUILayout.IntField("Triangle Count: ", tempTriangleCount);
            }
        }
    }
}
