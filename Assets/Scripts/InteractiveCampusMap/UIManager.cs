using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

/// <summary>
/// This class manages al the User Interface actions.
/// <para>
/// Button events actions and label writing text.
/// </para>
/// </summary>
public class UIManager : MonoBehaviour
{
    private string currentCoAPuri;
    private bool bLightState;
    private int iRequestCounter;

    [SerializeField]
    private POIList poiList;

    [SerializeField]
    private CoapManager coapManager;

    [SerializeField]
    private Text inside_temp;
    [SerializeField]
    private Text inside_brightness;
    [SerializeField]
    private Text light_state;
    [SerializeField]
    private Text button_light_state;

    [SerializeField]
    private Text outside_temp;
    [SerializeField]
    private Text weather_condition;
    [SerializeField]
    private Text time;
    [SerializeField]
    private Text sunrise;
    [SerializeField]
    private Text sunset;

    //[SerializeField]
    //private Text testText;

    void Start()
    {
        coapManager.ResponseReceivedHandler += ResponseReceived;
        bLightState = false;
        iRequestCounter = 0;
        updateInfos();
    }

    public void changeSelectedPOI(Dropdown DD)
    {
        //testText.text = "";                                 //REMOVE
        poiList.updatePOILocation(DD.value);
        updateInfos();
    }

    public void updateInfos()
    {
        currentCoAPuri = poiList.getCurrentPOIuri();

        inside_temp.text = "Inside:     ";
        StartCoroutine(GetState("temperature", 0));

        inside_brightness.text = "Brightness: ";
        StartCoroutine(GetState("light", 1));

        light_state.text = "Light:";
        StartCoroutine(GetState("led", 2));

        GetGeoInfo();
        UpdateClockTimeInfo();
    }

    public void OnLightSwitchPressed()
    {
        if(bLightState == true)
        {
            ChangeLightState("0");
        }
        else
        {
            ChangeLightState("1");
        }
        StartCoroutine(GetState("led",3));       
    }

    private void ChangeLightState(string state)
    {
        string uri = coapManager.GetUri(currentCoAPuri, "led");
        coapManager.DoPut(uri, state);
    }

    IEnumerator GetState(string resource, int seq)
    {      
        yield return new WaitUntil(()=> (seq == iRequestCounter || seq == 3));          //to avoid deny of service of server -->subsequent requests/gets
        string uri = coapManager.GetUri(currentCoAPuri, resource); 
        coapManager.DoGet(uri);
    }

    public void ResponseReceived(object sender, ResponseReceivedEventArgs e)
    {
        if(Equals("led", e.Resource))
        {
            if(Equals(e.Data,"0"))
            {
                bLightState = false;
                light_state.text = "Light: OFF";
                button_light_state.text = "Switch Light ON";
            }
            else if (Equals(e.Data, "1"))
            {
                bLightState = true;
                light_state.text = "Light:  ON";
                button_light_state.text = "Switch Light OFF";
            }
        }
        else if(Equals("light", e.Resource))
        {
            inside_brightness.text = inside_brightness.text + e.Data;
        }
        else if(Equals("temperature",e.Resource))
        {
            inside_temp.text = inside_temp.text + e.Data;
        }

        if (iRequestCounter < 2)
        {
            iRequestCounter++;
        }
        else
        {
            iRequestCounter = 0;
        }
        //testText.text = testText.text + e.Resource + ":" + e.Data + " ";                        //REMOVE
    }

    public void UpdateClockTimeInfo()
    {
        int hourNow = System.DateTime.Now.Hour;
        int minutesNow = System.DateTime.Now.Minute;
        time.text = "Time:     " + hourNow.ToString("00") + ":" + minutesNow.ToString("00");
    }

    public void GetGeoInfo()
    {
        StartCoroutine("GeoInfo");
    }

    IEnumerator GeoInfo()
    {
        WWW www = new WWW("http://api.apixu.com/v1/forecast.json?key=9302f9d8d4e04d33820192441181712&q=Castelldefels");
        yield return www;

        JObject obj = JObject.Parse(www.text);

        outside_temp.text = "Outside:    " + (string)obj["current"]["temp_c"];
        weather_condition.text = "Weather: " + (string)obj["current"]["condition"]["text"];
        sunrise.text = "Sunrise: " + (string)obj["forecast"]["forecastday"][0]["astro"]["sunrise"];
        sunset.text =  "Sunset:  " + (string)obj["forecast"]["forecastday"][0]["astro"]["sunset"];
    }
}
