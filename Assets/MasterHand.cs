using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class MasterHand : MonoBehaviour {

    public GameObject highlighter;
    public Planet referencePlanet;

    const string PLANET_NAME     = "planet";
    const string HIGHLIGHT_NAME  = "highlighter";
    const int    LIGHT_RANGE     = 500;
    const int    LIGHT_INTENSITY = 500;
    const int    LIGHT_BOUNCE    = 500;
    const int    LIGHT_ANGLE     = 500;

    private GameObject planet;
    private Light      light;
    private Transform  lightTransform;
    private Transform  planetTransform;

    // Use this for initialization
    void Start () {
        planet          = GameObject.Find(PLANET_NAME);
        highlighter     = GameObject.Find(HIGHLIGHT_NAME);
        referencePlanet = planet.GetComponent<Planet>();
        light           = highlighter.GetComponent<Light>();
        planetTransform = planet.GetComponent<Transform>();
        lightTransform  = highlighter.GetComponent<Transform>();

        light.type            = LightType.Spot;
        light.color           = Color.cyan;
        light.range           = LIGHT_RANGE;
        light.intensity       = 8;
        light.bounceIntensity = 8;
        light.spotAngle       = 10;
    }
}
