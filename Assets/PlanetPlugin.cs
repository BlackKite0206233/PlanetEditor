using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlanetPlugin : MonoBehaviour {

    [MenuItem("PlanetPlugin/add Planet")]
    static void Start()
    {
        GameObject obj  = new GameObject("planet");
        Planet     p    = obj.AddComponent<Planet>();
    }
}
