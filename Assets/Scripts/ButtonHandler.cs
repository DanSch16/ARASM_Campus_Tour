using UnityEngine;
using UnityEngine.Video;

public class ButtonHandler: MonoBehaviour {

    private GameObject InfoScreen;
    public GameObject InfoScreenPrefab;
    private RectTransform panelRectTransform;
       
    // Use this for initialization
    void Start()
    {           
        //showInfoScreen();
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    public void toggleInfoScreen()
    {

        if (InfoScreen != null)
        {
            Destroy(InfoScreen);
        }
        else
        {
            InfoScreen = GameObject.Instantiate(InfoScreenPrefab, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            RectTransform panelRectTransform = InfoScreen.GetComponent<RectTransform>();
            panelRectTransform.anchorMin = new Vector2(1, 1);
            panelRectTransform.anchorMax = new Vector2(1, 1);
            panelRectTransform.pivot = new Vector2(0.5f, 0.5f);
        }
    }
}
