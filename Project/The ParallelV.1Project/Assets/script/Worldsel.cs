using UnityEngine;
using System.Collections;
using Leap;
public class Worldsel : MonoBehaviour {
	public GameObject _camthai;
	public GameObject _camjapan;
	Controller controller;
	int ckpause;
	
	// Use this for initialization
	void Start () {
		controller = new Controller();
		
	}
	// Update is called once per frame
	void Update () {
		HandList leap_Hands = controller.Frame ().Hands;
		int num_hands = leap_Hands.Count;
		for (int i=0; i<num_hands; i++) 
		{
			Hand leap_hand = leap_Hands[i];
			//Debug.Log(leap_hand.PalmPosition.x);

			if(leap_hand.PalmPosition.x <-10)
			{
				ckpause = 1;
				_camthai.SetActive(true);
				_camjapan.SetActive(false);
			}
			
			else if(leap_hand.PalmPosition.x>90)
			{
				ckpause = 2;
				
				_camthai.SetActive(false);
				_camjapan.SetActive(true);
			}
			if(leap_hand.PalmPosition.z <-70)
			{
				if(ckpause == 1)
				{
					Application.LoadLevel("Thailand");
				}
			}
			
		}	
		
		
	}
	
}
