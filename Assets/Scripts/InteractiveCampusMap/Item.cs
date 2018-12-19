using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {

    public double Lat;
    public double Long;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void updatePOILocation(Dropdown Value)
    {
        Debug.Log("Updated!");
        Debug.Log(Value.value);
    }
}
