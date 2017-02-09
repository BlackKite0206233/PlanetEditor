using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlanetPlugin : MonoBehaviour {

    const string PLANET_NAME    = "planet";
    const string HIGHLIGHT_NAME = "highlighter";

    [MenuItem("PlanetPlugin/add Planet")]
    static void Start()
    {
        GameObject planet      = new GameObject(PLANET_NAME);
        GameObject highlighter = new GameObject(HIGHLIGHT_NAME);
        planet.AddComponent<Planet>();
        highlighter.AddComponent<Light>();
    }
}
