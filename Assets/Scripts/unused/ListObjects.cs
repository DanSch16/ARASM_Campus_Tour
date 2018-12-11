using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListObjects: MonoBehaviour {

	public List<GameObject> objects ;

	private int pointer;
	private int max;

	// Use this for initialization
	void Start () {
		max = 2;
    		pointer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void NextObject(){
		Debug.Log("Click"+ pointer);
		objects[pointer].SetActive(false);
		pointer ++;
		if (pointer>=max) pointer = 0;
		objects[pointer].SetActive(true);

	}

}
