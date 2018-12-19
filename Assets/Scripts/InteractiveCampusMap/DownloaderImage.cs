using UnityEngine;
using System.Collections;

public class DownloaderImage : MonoBehaviour {

    public GameObject Plane;
    public GameObject Plane2;
    public GameObject Items;

    //public double LonReference;
    //public double LatReference;

    private GameObject currentPOI;
    private int x = 132518;
    private int y = 98018;
    private int zoom = 18;

    // Use this for initialization
    IEnumerator Start () {
        
        WWW www = new WWW("http://a.tile.openstreetmap.org/"+zoom.ToString() + "/"+ x.ToString() + "/" + y.ToString() + ".png");
        yield return www;
        WWW www2 = new WWW("http://a.tile.openstreetmap.org/" + zoom.ToString() + "/" + (x+1).ToString() + "/" + y.ToString() + ".png");
        yield return www2;

        Debug.Log("Error"+www.error);
        Debug.Log("Error" + www2.error);

        Texture2D texture = new Texture2D(1, 1, TextureFormat.ARGB32, true);
        www.LoadImageIntoTexture(texture);
        Plane.GetComponent<Renderer>().material.mainTexture = texture;

        Texture2D texture2 = new Texture2D(1, 1, TextureFormat.ARGB32, true);
        www2.LoadImageIntoTexture(texture2);
        Plane2.GetComponent<Renderer>().material.mainTexture = texture2;



        foreach (Transform t in Items.transform) {
            GameObject item = t.gameObject;
            print("Location of Item " + item.GetComponent<Item>().Long + " " + item.GetComponent<Item>().Lat);
            double a = DrawCubeX(item.GetComponent<Item>().Long, TileToWorldPos(x, y, zoom).X, TileToWorldPos(x + 1, y, zoom).X);
            double b = DrawCubeY(item.GetComponent<Item>().Lat, TileToWorldPos(x, y + 1, zoom).Y, TileToWorldPos(x, y, zoom).Y);
            print("Calculated " + a + " " + b);
            item.transform.position = new Vector3((float)a, (float)b, item.transform.position.z);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public struct Point {
        public double X;
        public double Y;
    }

    public Point WorldToTilePos(double lon, double lat, int zoom) {
        Point p;
        p.X = ((lon + 180.0) / 360.0 * System.Math.Pow(2.0, zoom));
        p.Y = ((1.0 - System.Math.Log(System.Math.Tan(lat * System.Math.PI / 180.0) +
            1.0 / System.Math.Cos(lat * System.Math.PI / 180.0)) / System.Math.PI) / 2.0 * System.Math.Pow(2.0, zoom));

        return p;
    }

    // X -> longitud
    // Y -> latitud
    // devuelve la esquina superior izquierda del tile
    public Point TileToWorldPos(double tile_x, double tile_y, int zoom) {
        Point p = new Point();
        double n = System.Math.PI - ((2.0 * System.Math.PI * tile_y) / System.Math.Pow(2.0, zoom));

        p.X = ((tile_x / System.Math.Pow(2.0, zoom) * 360.0) - 180.0);
        p.Y = (180.0 / System.Math.PI * System.Math.Atan(System.Math.Sinh(n)));

        return p;
    }

    public double DrawCubeY(double targetLat, double minLat, double maxLat) {
        double pixelY = ((targetLat - minLat) / (maxLat - minLat)) * Plane.transform.localScale.x;
        return pixelY;
    }

    public double DrawCubeX(double targetLong, double minLong, double maxLong) {
        double pixelX = ((targetLong - minLong) / (maxLong - minLong)) * Plane.transform.localScale.y;
        return pixelX;
    }

    //public void set
}
