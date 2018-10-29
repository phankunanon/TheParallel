using UnityEngine;
using System.Collections;
using Leap;
public class Newmenu : MonoBehaviour {
	public GameObject _play;
	int ckpause;
	public GameObject _setting;
	public GameObject _exit;
	Controller controller;

	// Use this for initialization
	void Start () {
		controller = new Controller();
	
	}
	// Update is called once per frame
	void Update () {
		HandList leap_Hands = controller.Frame ().Hands;
		Hand leap_hand;
		int num_hands = leap_Hands.Count;
			for (int i=0; i<num_hands; i++) 
			{
			if(leap_Hands[i].IsRight)leap_hand=leap_Hands[i];
			}
		Debug.Log(leap_hand.PalmPosition.z);
		if(leap_hand.PalmPosition.y >120)
		{
			ckpause = 1;
			_play.SetActive(true);
			_setting.SetActive(false);
			_exit.SetActive(false);
		}
		
		else if(leap_hand.PalmPosition.y >80&&leap_hand.PalmPosition.y<110)
		{
			ckpause = 2;
			
			_play.SetActive(false);
			_setting.SetActive(true);
			_exit.SetActive(false);
		}
		
		else if(leap_hand.PalmPosition.y<60)
		{
			ckpause = 3;
			
			_play.SetActive(false);
			_setting.SetActive(false);
			_exit.SetActive(true);
		}
		if(leap_hand.PalmPosition.z <-70)
		{
			if(ckpause == 3)
			{	print("h");
				Application.Quit();
				
			}
			if(ckpause == 1)
			{
				Application.LoadLevel("Worldselect");
			}
			
		}
		}
}
