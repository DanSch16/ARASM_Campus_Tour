using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour {
    public GameObject spherePrefab;
	
    public void NewSphereObject()
    {
        Instantiate(spherePrefab);
    }
}
