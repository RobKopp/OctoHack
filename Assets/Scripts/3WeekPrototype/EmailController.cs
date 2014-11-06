using UnityEngine;
using System.Collections.Generic;

public class EmailController : MonoBehaviour {

	public GameObject SubjectLine;
	public GameObject FromLine;
	public GameObject DateLine;

	public AnimationClip ActivateAnimation;
	public AnimationClip DeactivateAnimation;

	void SetEmail(Email emailConfig) {
		SubjectLine.GetComponent<TextMesh>().text = emailConfig.subject;
		FromLine.GetComponent<TextMesh>().text = emailConfig.from;
		DateLine.GetComponent<TextMesh>().text = emailConfig.date;
	}

	void Activate() {
		animation.Play (ActivateAnimation.name);
	}

	void Deactivate() {
		animation.Play(DeactivateAnimation.name);
	}
}
