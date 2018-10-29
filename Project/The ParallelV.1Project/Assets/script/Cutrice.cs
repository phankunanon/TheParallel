 using UnityEngine;
using System.Collections;
using Leap;

public class Cutrice : MonoBehaviour {
	public GameObject _rice;
	public GameObject _allrice;
	public GameObject _mainmodel;
	public GameObject _model;
	public GameObject posaftergetwater;
	public GameObject cutmodel;
	private Animation _animation ;
	private Controller controller;
	// Use this for initialization
	void Start () {
		_animation = GetComponentInChildren<Animation> ();
		controller = new Controller ();
		_mainmodel.SetActive (false);
	}
	IEnumerator MyMethod() {
		yield return new WaitForSeconds(1.3f);
		_mainmodel.SetActive(true);
		_model.SetActive(true);
		Destroy (_allrice);
		_rice.SetActive (true);
		cutmodel.SetActive(false);

	}
	// Update is called once per frame
	void Update () {

				HandList handList = controller.Frame ().Hands;
				Hand rHand = Hand.Invalid;
				for (int i=0; i<handList.Count; i++) {
						if (handList [i].IsRight && !rHand.IsValid)
								rHand = handList [i];
				}
		print (Mathf.Abs (rHand.PalmVelocity.x));
				controller.EnableGesture (Gesture.GestureType.TYPESWIPE);
				SwipeGesture swipe = SwipeGesture.Invalid as SwipeGesture;
				Frame frame = controller.Frame ();
				for (int i=0; i< frame.Gestures ().Count; i++) {
						if (frame.Gestures () [i].Type == Gesture.GestureType.TYPESWIPE) {
								swipe = new SwipeGesture (frame.Gestures () [i]);
						}
				}
		print (Mathf.Abs (rHand.PalmVelocity.x));
		print (rHand.PalmPosition.x);
		if(rHand.PalmPosition.x<-30)//slide hand
		{
				
			_animation.Play("cutrice");
			StartCoroutine(MyMethod());//deley 1.3 sec

		}
		if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.S)) 
		{
			_mainmodel.transform.position = posaftergetwater.transform.position;
			_model.SetActive(true);
			_mainmodel.SetActive(true);
			cutmodel.SetActive(false);
		}


	
	}
}
