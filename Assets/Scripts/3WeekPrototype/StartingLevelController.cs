using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StartingLevelController : MonoBehaviour {
	private int phase;
	private bool transitioning;

	public AnimationClip[] phaseAnimationChanges;

	// Use this for initialization
	void Start () {
		phase = 1;
	}
	
	// Update is called once per frame
	void Update () {
		switch(phase) {
		case 1:
			//Tap Phase

			if(Input.GetMouseButtonUp(0)) {
				Debug.Log ("Transitioning");
				StartCoroutine(gotoNextPhase());
			}
			break;
		case 2:
			//Flick Phase, flicking controller takes care of everythings
			break;
		case 3:
			//Tap Phase
			if(Input.GetMouseButtonUp(0)) {
				gotoNextPhase();
				transform.parent.SendMessage("CompleteLevel");
			}
			break;
		}
	}

	IEnumerator gotoNextPhase() {

		transitioning = true;
		if(phaseAnimationChanges[phase - 1] != null) {
			animation.Play(phaseAnimationChanges[phase - 1].name);
			yield return CustomYields.WaitForAnimation(animation);
		}
		switch(phase) {
		case 1:
			Transform flickGoal = transform.FindChild("FlickingGoal");
			Vector2 loc = new Vector2(Random.Range(-2, 2), Random.Range(3,4));
			flickGoal.transform.position = loc;
			break;
		case 2:
			break;
		case 3:

			break;
		}

		phase += 1;
		transitioning = false;
	}

	void PerformFlick() {
		StartCoroutine(gotoNextPhase());
	}

	void FinishFlick(bool success) {
		StartCoroutine(gotoNextPhase());
	}

	void Show() {

	}

	void Hide() {
		gameObject.SetActive(false);
	}
}
