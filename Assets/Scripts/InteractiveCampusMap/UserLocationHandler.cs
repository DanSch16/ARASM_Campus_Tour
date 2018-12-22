using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserLocationHandler : MonoBehaviour {
    [SerializeField]
    private GameObject MapLoader;
    private DownloaderImage DI;

    private GameObject latitude;
    private GameObject longitude;
    
    public float defaultLat;
    public float defaultLon;

    private bool GPSLinkActive;

    // Use this for initialization
    IEnumerator Start()
    {
        GPSLinkActive = false;
        DI = MapLoader.GetComponent<DownloaderImage>();

        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location       -->do query everywhere if its works
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            GPSLinkActive = true;
            // Access granted and location value could be retrieved
            print("First Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        float Long = defaultLon;
        float Lat = defaultLat;

        if (GPSLinkActive)
        {
            Long = Input.location.lastData.longitude;
            Lat = Input.location.lastData.latitude;
        }

        double a = DI.DrawCubeX(Long, DI.TileToWorldPos(DI.x, DI.y, DI.zoom).X, DI.TileToWorldPos(DI.x + 1, DI.y, DI.zoom).X);
        double b = DI.DrawCubeY(Lat, DI.TileToWorldPos(DI.x, DI.y + 1, DI.zoom).Y, DI.TileToWorldPos(DI.x, DI.y, DI.zoom).Y);
        gameObject.transform.position = new Vector3((float)a, (float)b, gameObject.transform.position.z);
    }

    private void OnDestroy()
    {
        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }
}
