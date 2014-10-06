using UnityEngine;
using System.Collections;

public class ClickController : MonoBehaviour {

	public AnimationClip ClickAnimation;
	bool clicked = false;

	//When it gets clicked!
	void OnMouseUpAsButton() {
		if(!clicked) {
			StartCoroutine(playAnimation());
		}
	}

	IEnumerator playAnimation() {
		animation.Play(ClickAnimation.name);
		yield return CustomYields.WaitForAnimation(animation);
		transform.parent.SendMessage("Clicked",gameObject);
	}
}
