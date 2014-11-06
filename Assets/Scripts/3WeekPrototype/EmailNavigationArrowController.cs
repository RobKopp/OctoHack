using UnityEngine;
using System.Collections;

public class EmailNavigationArrowController : MonoBehaviour {

	public enum ArrowDirection {
		Up,
		Down
	}

	public ArrowDirection MyDirection;

	public GameObject EmailController;

	public AnimationClip clickAnimation;

	public void OnMouseUpAsButton() {
		EmailController.SendMessage("MoveCursor", MyDirection);
		animation.Play(clickAnimation.name);
	}
}
