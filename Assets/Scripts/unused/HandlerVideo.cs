using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;



public class HandlerVideo: MonoBehaviour {
	


	public void Start_V(){
GameObject.Find("Screen_Cube").GetComponent<VideoPlayer>().Play();
	}

	public void Stop_V(){
GameObject.Find("Screen_Cube").GetComponent<VideoPlayer>().Stop();
	}

	public void Pause_V(){
GameObject.Find("Screen_Cube").GetComponent<VideoPlayer>().Pause();
	}


}
