using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System.IO;

// https://youtu.be/ylqrb2_1MsY
public class VideoManager : MonoBehaviour {
	private int stop = 0;
	// Use this for initialization
	void Start () {
	stop = 0;
	this.GetComponent<VideoPlayer>().Stop();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		if (stop==0){
			stop = 1;
			this.GetComponent<VideoPlayer>().Play();
		}else{
			stop = 0;
			this.GetComponent<VideoPlayer>().Pause();
		}
	}
    //currently not used
	public void PauseVideo(){
		stop = 0;
		this.GetComponent<VideoPlayer>().Pause();
	}
}
