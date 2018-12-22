using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class POIList : MonoBehaviour {

    [SerializeField]
    private GameObject MapLoader;
    private DownloaderImage DI;

    [SerializeField]
    private GameObject currentPOI;
    public List<POI> ListPOI;
    private int currentPOIid;

    // Use this for initialization
    void Start()
    {
        DI = MapLoader.GetComponent<DownloaderImage>();
        if (ListPOI.Count > 0)
        {
            updatePOILocation(0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updatePOILocation(int newPOIid)
    {
        currentPOIid = newPOIid;
        double a = DI.DrawCubeX(ListPOI[currentPOIid].Long, DI.TileToWorldPos(DI.x, DI.y, DI.zoom).X, DI.TileToWorldPos(DI.x + 1, DI.y, DI.zoom).X);
        double b = DI.DrawCubeY(ListPOI[currentPOIid].Lat, DI.TileToWorldPos(DI.x, DI.y + 1, DI.zoom).Y, DI.TileToWorldPos(DI.x, DI.y, DI.zoom).Y);
        currentPOI.transform.position = new Vector3((float)a, (float)b, currentPOI.transform.position.z);
    }

    public string getCurrentPOIuri()
    {
        return ListPOI[currentPOIid].CoAPUri;
    }
}
