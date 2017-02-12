using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MasterHand))]
public class Control : Editor {

    MasterHand t;
    GameObject highlighter;
    Camera     cam;
    Light      light;
    bool       isSelect = false;

    const float   DIS_OFFSET = 5;
    const float   SPEED      = 0.1f;
    const KeyCode PLUS       = KeyCode.KeypadPlus;
    const KeyCode MINUS      = KeyCode.KeypadMinus;

    void OnSceneGUI()
    {
        cam         = Camera.current;
        t           = target as MasterHand;
        highlighter = t.highlighter;
        light       = highlighter.GetComponent<Light>();
        

        if (Selection.activeTransform.gameObject.name.Equals(t.referencePlanet.name))
        {
            setPosition();
            if (!isSelect)
                setAngle();

            if (Event.current.keyCode == PLUS || Event.current.keyCode == MINUS)
                changeTerrain();

            isSelect = true;
        }
        else
            isSelect = false;
    }

    void setPosition()
    {
        Transform lightTransform = highlighter.GetComponent<Transform>();

        lightTransform.position = cam.transform.position;
        lightTransform.LookAt(t.referencePlanet.transform);
    }

    void setAngle()
    {
        Mesh mesh     = t.referencePlanet.GetComponent<Mesh>();
        float dis     = Vector3.Distance(highlighter.transform.position, t.referencePlanet.transform.position);
        float radious = t.referencePlanet.Radious + DIS_OFFSET;
        float angle   = Mathf.Asin(radious / dis) * 180 / Mathf.PI;

        light.spotAngle = angle * 2;
    }

    void changeTerrain()
    {
        MeshFilter mf = t.referencePlanet.GetComponent<MeshFilter>();
        Mesh mesh = mf.mesh;
        List<Vector3> vert = new List<Vector3>();

        foreach (var v in mesh.vertices)
        {
            float r          = Vector3.Distance(v, t.referencePlanet.transform.position);
            float dis        = Vector3.Distance(highlighter.transform.position, t.referencePlanet.transform.position);
            float hypotenuse = Vector3.Distance(v, t.highlighter.transform.position);
            float angle      = Mathf.Acos((Mathf.Pow(hypotenuse, 2) + Mathf.Pow(dis, 2) - Mathf.Pow(r, 2)) / (2 * dis * hypotenuse)) * 180 / Mathf.PI;

            if (angle < light.spotAngle / 2 && hypotenuse < dis)
            {
                Vector3 changeVert;
                
                if (Event.current.keyCode == PLUS)
                    changeVert = Vector3.MoveTowards(v, highlighter.transform.position, SPEED);
                else
                    changeVert = Vector3.MoveTowards(v, t.referencePlanet.transform.position, SPEED);
                vert.Add(changeVert);
            }
            else
                vert.Add(v);
        }
        mesh.vertices = vert.ToArray();
        mf.mesh = mesh;
    }
}
