using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour {

    public float lat;
    public float lon;
    public string desc;

    public void OnMouseDown()
    {
        Destroy(gameObject);    //gameObject this script is assigned to
    }
}
