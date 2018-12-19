using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIList : MonoBehaviour {

    public List<POI> ListPOI;

    private int currentPOIid;
    private int max_pointer;

    // Use this for initialization
    void Start()
    {
        //max_pointer = 2;
        currentPOIid = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
    public void NextObject()
    {
        Debug.Log("Click" + pointer);
        pois[pointer].SetActive(false);
        pointer++;
        if (pointer >= max) pointer = 0;
        pois[pointer].SetActive(true);

    }
    */

}
