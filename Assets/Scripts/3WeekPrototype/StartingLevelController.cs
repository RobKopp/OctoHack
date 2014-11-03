using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StartingLevelController : GridActioner,ISerializable {


	public float TimeTarget;
	public float BonusThreshold;

	public float phase2Timer;
	private bool startTimer;
	private int currentPhase;
	private bool transitioning;

	public AnimationClip[] phaseAnimationChanges;

	// Use this for initialization
	void Start () {
		currentPhase = 1;
	}

	public override Dictionary<string,string> Serialize() {
		Dictionary<string,string> def = base.Serialize();
		def.Add ("TT",TimeTarget.ToString());
		def.Add ("BT", BonusThreshold.ToString());
		def.Add ("PT", phase2Timer.ToString());

		return def;
	}

	public override void DeSerialize(Dictionary<string,object> def) {
		base.DeSerialize(def);
		TimeTarget = float.Parse((string)def["TT"]);
		BonusThreshold = float.Parse((string)def["BT"]);
		phase2Timer = float.Parse((string)def["PT"]);
	}

	void Clicked() {
		switch(currentPhase) {
		case 1:
			gotoNextPhase();
			break;
		case 2:
			startTimer = true;
			phase2Timer = 0;
			gotoNextPhase();
			break;
		case 3:
			startTimer = false;
			if(phase2Timer - TimeTarget < BonusThreshold) {
				youDidGood();
			} else {
				itsFine();
			}
			break;
		}
	}

	void Update() {
		if(startTimer) {
			phase2Timer += Time.deltaTime;
		}
	}

	void gotoNextPhase() {
		currentPhase += 1;

	}

	void youDidGood() {
		finishLevel();
	}

	void itsFine() {
		finishLevel();
	}
}
