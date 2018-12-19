using UnityEngine;
using System.Collections;

public class DownloaderImage : MonoBehaviour {

    public GameObject Plane;

    public GameObject Items;

	// Use this for initialization
	IEnumerator Start () {

        //Plane.transform.position = new Vector3(Plane.transform.localScale.x/2, Plane.transform.localScale.y/2, 0);

        int zoom = 18;
 
        Point p = WorldToTilePos(1.986918, 41.275374, zoom);
        print(p.X + " " + p.Y);
        int x = Mathf.FloorToInt((float)p.X);
        int y = Mathf.FloorToInt((float)p.Y);
        print("X " + x + " Y " + y);
		//x=x+1;
        WWW www = new WWW("http://a.tile.openstreetmap.org/"+zoom.ToString() + "/"+ x.ToString() + "/" + y.ToString() + ".png");

		//WWW www = new WWW("http://a.tile.openstreetmap.org/0/0/0.png");
        yield return www;

Debug.Log("Error"+www.error);

        Texture2D texture = new Texture2D(1, 1, TextureFormat.ARGB32, true);
        www.LoadImageIntoTexture(texture);
        Plane.GetComponent<Renderer>().material.mainTexture = texture;



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
}
