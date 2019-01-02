using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System.IO;

public class VideoOnMouseDown : MonoBehaviour {
	private bool playing = false;
	// Use this for initialization
	void Start () {
	playing = false;
	this.GetComponent<VideoPlayer>().Stop();
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		if (playing== false)
        {
            playing = true;
            this.GetComponent<VideoPlayer>().Play();
		}else{
			playing = false;
			this.GetComponent<VideoPlayer>().Pause();
        }
	}
    //currently not used
	public void PauseVideo(){
		playing = false;
		this.GetComponent<VideoPlayer>().Pause();
	}
    public bool isPlaying()
    {
        return playing;
    }
}
