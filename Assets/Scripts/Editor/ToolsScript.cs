using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ToolsScript : MonoBehaviour {

    [MenuItem("Scripts/Assign Tile Material")]
    public static void AssignTileMaterial()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        Material material = Resources.Load<Material>("Tile");

        foreach (GameObject t in tiles)
        {
            t.GetComponent<Renderer>().material = material;
        }
    }

    [MenuItem("Scripts/Assign Tile Script")]
    public static void AssignTileScript()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject t in tiles)
        {
            t.AddComponent<Tile>();
        }
    }

    [MenuItem("Scripts/Remove Duplicate Tile Scripts")]
    public static void RemoveDuplicateScripts()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject t in tiles)
        {
            if (t.GetComponents<Tile>().Length > 1)
            {
                DestroyImmediate(t.GetComponents<Tile>()[0]);
            }
        }
    }
}