using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StartingLevelController : GridActioner {
	private int phase;
	private bool transitioning;

	public AnimationClip[] phaseAnimationChanges;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	}

	void PerformFlick() {
		//StartCoroutine(gotoNextPhase());
	}

	void FinishFlick(bool success) {

	}
}
