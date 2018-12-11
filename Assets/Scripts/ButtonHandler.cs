using UnityEngine;
using UnityEngine.Video;

public class ButtonHandler: MonoBehaviour {
    private int pause = 0;
    // Use this for initialization
    void Start()
    {
        pause = 0;
        GameObject.Find("Video_Quad").GetComponent<VideoPlayer>().Stop();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayButtonPressed()
    {
        /*
        if (pause == 0)
        {
            pause = 1;
            */
            GameObject.Find("Video_Quad").GetComponent<VideoPlayer>().Play();
        //}
    }
    public void PauseButtonPressed()
    {
        /*
        if (pause == 1)
        {
            pause = 0;
            */
            GameObject.Find("Video_Quad").GetComponent<VideoPlayer>().Pause();
        //}
    }
    public void StopButtonPressed()
    {
        GameObject.Find("Video_Quad").GetComponent<VideoPlayer>().Stop();
    }
}
