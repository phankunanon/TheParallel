using UnityEngine;
using System.Collections;

public class Usephoto : MonoBehaviour {
	public GameObject pic1;
	public GameObject pic2;
	bool ch=true;
	float time; 
	// Use this for initialization
	void Start () {
	
	}
	// Update is called once per frame
	void Update () 
	{
		time += Time.deltaTime;
		if(time > 0.3f)
		{
			if(ch==true)
			{
				ch=false;
				pic1.SetActive(false);
				pic2.SetActive(true);
				//pic 2.true
				time=0f;
			}
			else if(ch==false)
			{
				ch=true;
				
				pic1.SetActive(true);
				pic2.SetActive(false);
				//pic1.true;
				time=0f;
			}
		}
	}
}
