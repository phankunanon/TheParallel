using UnityEngine;
using System.Collections.Generic;
using Leap;
public class MainController : MonoBehaviour {
	Controller controller;
	public GameObject _mainmodel;
	public GameObject _model;
	public GameObject _savepoint1;
	public GameObject _savepoint2;
	public GameObject peoplecutrice;
	public GameObject peoplegetwater;
	public float fws = 1f;
	public float forjump = 2f;
	private bool ckjump;
	private bool getitem = false;
	private bool _ckpause = false;
	//private int pausemenu = 1;
	private Hand RHand;
	private Hand LHand;
	public GameObject pastmap;
	public GameObject presentmap;
	private int _itemnum ;
	public AudioClip warp;
	private Animation _animation ;
	private bool _sound = false ;
	
	
	// Use this for initialization
	void Start () {
		controller = new Controller();
		_animation = GetComponentInChildren<Animation> ();
		getitem = false;
		_itemnum = 0;
	}
	void walk(int f)
	{
		if (f == 1)
		{
			gameObject.transform.position += new Vector3 (fws*Time.deltaTime , 0, 0);
			_animation.Play("Walk");
			_mainmodel.transform.rotation = new Quaternion (0,90,0,90*-1);
			//_animation.CrossFade(walkAnwwimation.name,PlayMode.StopAll);
		}
		
		if(f == 2)
		{
			gameObject.transform.position += new Vector3 (fws*-1*Time.deltaTime , 0, 0);
			_animation.Play("Walk");
			_animation.Play("Walk",PlayMode.StopAll);
			_mainmodel.transform.rotation = new Quaternion (0,90,0,90);
		}
		
	}
	void jump()
	{
		if (_mainmodel == true)
			_model.SetActive (true);
		//gameObject.transform.position += new Vector3(0,forjump,0);
		rigidbody.AddForce (0,forjump,0);
		ckjump=false;
		_animation.Play("Jump");
		_animation.Play("Jump",PlayMode.StopAll);
	}
	void pastpre(int j)
	{
		if (j == 1)
		{
			pastmap.SetActive(false);
			presentmap.SetActive(true);
			if(_sound == true)AudioSource.PlayClipAtPoint(warp, transform.position);
			_sound = false;
		}
		if (j == 2)
		{
			pastmap.SetActive(true);
			presentmap.SetActive(false);
			if(_sound == false)AudioSource.PlayClipAtPoint(warp, transform.position);
			_sound = true;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		/*
		if (Input.GetKeyDown (KeyCode.Escape) && _ckpause == false) {
			print("pause");
			_ckpause = true;		
		}
		else if (Input.GetKeyDown (KeyCode.Escape) && _ckpause == true) {
			print("unpause");
			_ckpause = false;		
		}*/
		
		//keybords
		if (_ckpause == false) {
			if (Input.GetKey (KeyCode.Space)&& ckjump == true)
				jump ();
			if(Input.GetKeyDown(KeyCode.J))
				pastpre(2);
			if(Input.GetKeyDown(KeyCode.K))
				pastpre(1);
			if (Input.GetKey (KeyCode.L))
				getitem = true;
			if(Input.GetKeyUp(KeyCode.L))
				getitem = false;
			
			if (Input.GetKey (KeyCode.W))
				walk (1);
			else if (Input.GetKey (KeyCode.S))
				walk (2);	
			else _animation.Play("Idle");
		}
		/*
		if (_ckpause == true) {
			if(Input.GetKeyDown(KeyCode.W)&&pausemenu>1)pausemenu--;
			if(Input.GetKeyDown(KeyCode.S)&&pausemenu<3)pausemenu++;
			print(pausemenu);
			if(Input.GetKeyDown(KeyCode.Return)&&pausemenu==1)
			{

			}
			if(Input.GetKeyDown(KeyCode.Return)&&pausemenu==2)
			{
				
			}
			if(Input.GetKeyDown(KeyCode.Return)&&pausemenu==3)
			{
				
			}
		}
		*/		
		
		
		
		// Leap motion
		/**CONFIG**/
		controller.EnableGesture (Gesture.GestureType.TYPECIRCLE);
		controller.EnableGesture (Gesture.GestureType.TYPESWIPE);
		controller.Config.SetFloat("Gesture.ScreenTap.MinForwardVelocity", 10.0f);
		controller.Config.SetFloat ("Gesture.Circle.MinRadius", 50.0f);
		controller.Config.SetFloat ("Gesture.Circle.MinArc", Mathf.PI * 2);
		
		
		HandList leap_Hands = controller.Frame ().Hands;
		int num_hands = leap_Hands.Count;
		Frame frame=Frame.Invalid, previous=Frame.Invalid;
		CircleGesture circle=CircleGesture.Invalid as CircleGesture;
		if (controller.IsConnected)frame = controller.Frame();
		for (int i=0; i< frame.Gestures ().Count; i++) {
			if(frame.Gestures()[i].Type == Gesture.GestureType.TYPECIRCLE)
			{
				circle= new CircleGesture(frame.Gestures ()[i]);
			}
			
		}
		//if(num_hands == 2)
		{
			//_pausemenu(2);
			for (int i=0; i<num_hands; i++) 
			{
				Hand leap_hand = leap_Hands[i];
				if(leap_hand.IsRight)RHand = leap_hand;
				//if(leap_hand.IsLeft)LHand = leap_hand;
				
				/*if(LHand != null){
							if(leap_hand.PalmPosition.z<-20)walk(1);
							else if(leap_hand.PalmPosition.z>0)walk(2);
							if (leap_hand.PalmPosition.y > 210 && ckjump == true) jump ();
					}*/
				//if(RHand != null)
				{
					
					// past - present
					if (circle.IsValid&&circle.Pointable.Direction.AngleTo(circle.Normal) <= Mathf.PI/2) {
						pastpre(1);
					}
					else{
						pastpre(2);
					}
					
					
					
					
					//Debug.Log("Right"+leap_hand.PalmPosition.y);
					//get item
					if(leap_hand.PalmPosition.y<160)
					{
						getitem = true;
					}
					else if(leap_hand.PalmPosition.y>160)
					{
						getitem = false;
					}
					//Debug.Log(getitem);
				}
				
				RHand = null;
				LHand = null;
				
			}
		}
	}
	
	
	void OnCollisionEnter(Collision _collision) 
	{
		if(_collision.gameObject.name == "dead1")
		{
			print("dead");
			gameObject.transform.position  = _savepoint1.transform.position; 
		}
		if(_collision.gameObject.name == "dead2")
		{
			gameObject.transform.position  = _savepoint2.transform.position; 
		}
		if(_collision.gameObject.tag == "map")
		{
			ckjump = true;
		}
		else if(_collision.gameObject.tag != "map")
		{
			ckjump = false;
		}
		if (_collision.gameObject.tag == "item" && getitem == true) 
		{		
			//Destroy(_collision.gameObject);
			_itemnum = 1;
			ckjump = true;
		}
		if (_collision.gameObject.tag == "item" && getitem == true) 
		{		
			
			Destroy(_collision.gameObject);
			_itemnum = 1;
			ckjump = true;
		}
		if (_collision.gameObject.tag == "puzzle" && getitem == true && _itemnum == 1) 
		{	
			_itemnum = 0;
			Destroy(_collision.gameObject);
			ckjump = true;
		}
		if (_collision.gameObject.name == "rice4" && getitem == true) 
		{	
			peoplecutrice.SetActive(true);
		}
		if (_collision.gameObject.tag == "rice" && getitem == true) 
		{	
			Application.LoadLevel("ending");
		}
		
		if (_collision.gameObject.name == "brick-pond" && getitem == true) 
		{	
			peoplegetwater.SetActive(true);
			_mainmodel.SetActive(false);
		}
		
		
	}
	
	
}
