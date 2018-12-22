using UnityEngine;
using System.Collections;

public class DownloaderImage : MonoBehaviour {

    public GameObject Plane;
    public GameObject Plane2;

    public int x = 132518;     //fixed value here, as we won't change map location
    public int y = 98018;
    public int zoom = 18;

    // Use this for initialization
    IEnumerator Start () {
        
        WWW www = new WWW("http://a.tile.openstreetmap.org/"+zoom.ToString() + "/"+ x.ToString() + "/" + y.ToString() + ".png");
        yield return www;
       
        WWW www2 = new WWW("http://a.tile.openstreetmap.org/" + zoom.ToString() + "/" + (x+1).ToString() + "/" + y.ToString() + ".png");
        yield return www2;

        Texture2D texture = new Texture2D(1, 1, TextureFormat.ARGB32, true);
        www.LoadImageIntoTexture(texture);
        Plane.GetComponent<Renderer>().material.mainTexture = texture;

        Texture2D texture2 = new Texture2D(1, 1, TextureFormat.ARGB32, true);
        www2.LoadImageIntoTexture(texture2);
        Plane2.GetComponent<Renderer>().material.mainTexture = texture2;
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

    // X -> longitude
    // Y -> latitude
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
}