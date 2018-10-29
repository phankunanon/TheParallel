using UnityEngine;
using System.Collections;

public class endding : MonoBehaviour {
	private int _time = 3;
	private float timeStart;
	// Use this for initialization
	void Start () {
		guiTexture.pixelInset = new Rect((guiTexture.pixelInset.x/649)*Screen.width,
		                                 (guiTexture.pixelInset.y/183)*Screen.height,
		                                 (guiTexture.pixelInset.width/649)*Screen.width,
		                                 (guiTexture.pixelInset.height/183)*Screen.height);

		timeStart = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - timeStart > _time)
			Application.LoadLevel("Worldselect");
	}
}
