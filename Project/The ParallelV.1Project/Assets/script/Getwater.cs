using UnityEngine;
using System.Collections;
using Leap;

public class Getwater : MonoBehaviour {
	public GameObject _water;
	public GameObject _mainmodel;
	public GameObject _model;
	public GameObject watermodel;
	public GameObject posaftergetwater;
	private Animation _animation ;
	private Controller controller;
	private bool played=false;
	int rUp = 0, lUp = 0;
	// Use this for initialization
	void Start () {
		_animation = GetComponentInChildren<Animation> ();
		controller = new Controller ();
		//_model.SetActive(false);
	}
	IEnumerator MyMethod() {
		yield return new WaitForSeconds(3.8f);
		_water.SetActive (true);
		_mainmodel.SetActive(true);
		_model.SetActive (true);
		_mainmodel.transform.position = posaftergetwater.transform.position;
		watermodel.SetActive(false);
	}
	// Update is called once per frame
	void Update () { 
		Hand rHand = Hand.Invalid as Hand, lHand=Hand.Invalid as Hand;
		HandList hands=controller.Frame().Hands;
		int sth = 0;
		for (int i=0; i<hands.Count; i++) {
			if(hands[i].IsLeft)lHand=hands[i];
			if(hands[i].IsRight)rHand=hands[i];
		}
		if (rHand.PalmVelocity.y < -10F) {
						if (rUp != -1)sth++;
						rUp = -1;
				} else if (rHand.PalmVelocity.y > 10F) {
						if(rUp!=1)sth++;
						rUp = 1;
				}
				else
						rUp = 0;
		if (lHand.PalmVelocity.y < -10F) {
						if (lUp != -1)
								sth++;			
						lUp = -1;
				} else if (lHand.PalmVelocity.y > 10F) {
						if(lUp!=1)sth++;
						lUp = 1;
				}
				else
						lUp = 0;
		if (sth == 2)
						played = false;
		if(rUp+lUp==0&&rUp!=0&&!played)//riding hand
		{
			played=true;
			_animation.Play("Jump");
			StartCoroutine(MyMethod());//deley 1.3 sec
		}
		
	}
}
