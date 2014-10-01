using UnityEngine;
using System.Collections;

public class ClickController : MonoBehaviour {

	public AnimationClip ClickAnimation;

	//When it gets clicked!
	void OnMouseUpAsButton() {
		StartCoroutine(playAnimation());
	}

	IEnumerator playAnimation() {
		animation.Play(ClickAnimation.name);
		yield return CustomYields.WaitForAnimation(animation);
		transform.parent.SendMessage("Clicked",gameObject);
	}
}
