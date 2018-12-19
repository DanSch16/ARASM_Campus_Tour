using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {
    public POIList poiList;
    public double Lat;
    public double Long;
    public GameObject MapLoader;
    private DownloaderImage DI;

    private int x = 132518;
    private int y = 98018;
    private int zoom = 18;

    // Use this for initialization
    void Start () {
        DI = MapLoader.GetComponent<DownloaderImage>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void updatePOILocation(Dropdown DD)
    {
        Lat = poiList.ListPOI[DD.value].Lat;
        Long = poiList.ListPOI[DD.value].Long;

        
        print("Location of Item " + Long + " " + Lat);
        double a = DI.DrawCubeX(Long, TileToWorldPos(x, y, zoom).X, TileToWorldPos(x + 1, y, zoom).X);
        double b = DI.DrawCubeY(Lat, TileToWorldPos(x, y + 1, zoom).Y, TileToWorldPos(x, y, zoom).Y);
        print("Calculated " + a + " " + b);
        gameObject.transform.position = new Vector3((float)a, (float)b, gameObject.transform.position.z);
        

    }

    public struct Point
    {
        public double X;
        public double Y;
    }

    // X -> longitud
    // Y -> latitud
    // devuelve la esquina superior izquierda del tile
    public Point TileToWorldPos(double tile_x, double tile_y, int zoom)
    {
        Point p = new Point();
        double n = System.Math.PI - ((2.0 * System.Math.PI * tile_y) / System.Math.Pow(2.0, zoom));

        p.X = ((tile_x / System.Math.Pow(2.0, zoom) * 360.0) - 180.0);
        p.Y = (180.0 / System.Math.PI * System.Math.Atan(System.Math.Sinh(n)));

        return p;
    }
}
