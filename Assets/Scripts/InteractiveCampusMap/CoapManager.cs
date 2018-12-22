using System;
using UnityEngine;

/// <summary>
/// Class that stores the arguments of the Response Received event.
/// </summary>
public class ResponseReceivedEventArgs : EventArgs
{
    public string Resource { get; set; }
    public string Data { get; set; }
}

/// <summary>
/// Class that manages the CoAP requests and responses.
/// 
/// This class calls the corresponding methods of the CoAP Android
/// library.
/// </summary>
public class CoapManager : MonoBehaviour
{
    /// <summary>
    /// CoAP response received event handler.
    /// </summary>

    public event EventHandler<ResponseReceivedEventArgs> ResponseReceivedHandler;

    //   private AndroidJavaObject playerActivityContext;

    /// <summary>
    /// The object that represents an Android class.
    /// </summary>
    private AndroidJavaObject coapClient;

    /// <summary>
    /// Start method is called automatically the first time the scene is loaded.
    /// This method gets and stores a reference to the Android class.
    /// </summary>
    void Start()
    {
        try
        {
            // Para conseguir el Context de la Activity de Android.
            //using (var actClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            //{
            //    playerActivityContext = actClass.GetStatic<AndroidJavaObject>("currentActivity");
            //}
  //          using (var actClass = new AndroidJavaClass("icarus.edu.californiumunitylibrary.CoapClientManager"))
  //          {
  //              coapClient = actClass.CallStatic<AndroidJavaObject>("getInstance");
  //          }

            coapClient = new AndroidJavaObject("icarus.edu.californiumunitylibrary.CoapClientManager");
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
        }
    }

    /// <summary>
    /// Builds a correct CoAP URI for the specified server and resource.
    /// </summary>
    /// <param name="ip">The IP address of the CoAP server.</param>
    /// <param name="resource">A name of a resource.</param>
    /// <returns>The well-formed CoAP URI for the specified server and resource.</returns>
    public string GetUri(string ip, string resource)
    {
        string res = coapClient.Call<string>("getUri", ip, resource);
        return res;
    }

    /// <summary>
    /// Sends a PUT request with the associated data
    /// to a specific server and resource.
    /// </summary>
    /// <param name="uri">A well-formed CoAP URI.</param>
    /// <param name="data">A new value associated to a resource.</param>
    public void DoPut(string uri, string data)
    {
       coapClient.Call("doPut", uri, data);

    }

    /// <summary>
    /// Sends a GET request to a specific server and resource.
    /// </summary>
    /// <param name="uri">A well-formed CoAP URI.</param>
    public void DoGet(string uri)
    {
        coapClient.Call("doGet", uri);
    }

    /// <summary>
    /// Gets a response from a CoAP request.
    /// 
    /// This method is called automatically by the CoAP library each
    /// time a request is responded.
    /// </summary>
    /// <param name="response">The CoAP response (resource/value).</param>
    public void GetResponse(string response)
    {
        ResponseReceivedEventArgs args = new ResponseReceivedEventArgs();

        if (response != "Error")
        {
            string[] msg = response.Split('/');
        
            args.Resource = msg[0];
            args.Data = msg[1];
            OnResponseReceived(args);
        }
        else
        {
            args.Resource = "Error";
            args.Data = "Error";
            OnResponseReceived(args);
        }
    }

    /// <summary>
    /// This method handles the Response Received event.
    /// </summary>
    /// <param name="e">Arguments for this event call.</param>
    protected virtual void OnResponseReceived(ResponseReceivedEventArgs e)
    {
        EventHandler<ResponseReceivedEventArgs> handler = ResponseReceivedHandler;
        if (handler != null)
        {
            handler(this, e);
        }
    }
}
