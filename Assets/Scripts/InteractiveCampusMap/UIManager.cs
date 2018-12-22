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

    [SerializeField]
    private Text testText;

    void Start()
    {
        coapManager.ResponseReceivedHandler += ResponseReceived;
        bLightState = false;

    }

    public void changeSelectedPOI(Dropdown DD)
    {
        testText.text = "";
        poiList.updatePOILocation(DD.value);
        currentCoAPuri = poiList.getCurrentPOIuri();

        inside_brightness.text = "Brightness: ";
        GetState("light");

        inside_temp.text = "Inside:     ";
        //GetState("temperature");

        light_state.text = "Light:";
        GetState("led");

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
        GetState("led");       
    }

    private void ChangeLightState(string state)
    {
        string uri = coapManager.GetUri(currentCoAPuri, "led");
        coapManager.DoPut(uri, state);
    }

    public void GetState(string resource)
    {
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
        testText.text = testText.text + e.Resource + ":" + e.Data + " "; 
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
        weather_condition.text = "Condition: " + (string)obj["current"]["condition"]["text"];
        sunrise.text = "Sunrise: " + (string)obj["forecast"]["forecastday"][0]["astro"]["sunrise"];
        sunset.text =  "Sunset:  " + (string)obj["forecast"]["forecastday"][0]["astro"]["sunset"];
    }
}
