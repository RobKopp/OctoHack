using UnityEngine;
using System.Collections;

public class SquareController : MonoBehaviour {

	public AnimationClip initAnimation;
	public AnimationClip ActivateAnimation;
	public AnimationClip DeactivateAnimation;


	private bool activated = false;

	public bool Activated {
		get {
			return activated;
		}
	}
	void Init() {
		animation.clip = initAnimation;
		animation.Play();
	}

	void ActivateIt() {
		animation.clip = ActivateAnimation;
		animation.Play();
		activated = true;
	}

	void DeactivateIt() {
		activated = false;
		animation.clip = DeactivateAnimation;
		animation.Play();
	}

	void OnMouseUpAsButton() {
		if(activated) {
			DeactivateIt();
		}
	}
}
