using UnityEngine;
using System.Collections;

public class DragController : MonoBehaviour {

	public enum DragDirection {
		Left,
		Right,
		Bad
	}

	public AnimationClip GoodAnimation;
	public AnimationClip BadAnimation;

	public float RightBound, LeftBound;

	public Color[] colors;

	public DragDirection dragDirection;

	private Transform startingLoc;

	private bool clicked;
	private bool locked;

	void Start() {
		if(dragDirection == DragDirection.Left) {
			renderer.material.color = colors[0];
		} else if(dragDirection == DragDirection.Right) {
			renderer.material.color = colors[1];
		} else {
			renderer.material.color = colors[2];
		}
	}

	void OnMouseDown() {
		if(!locked) {
			if(dragDirection == DragDirection.Bad) {
				youDidBad();
			} else {
				clicked = true;
			}
		}
	}

	void Update() {
		if(clicked) {
			Vector2 mousePos = Input.mousePosition;
			Vector3 pos = transform.position;
			pos.x = mousePos.x;
			pos.x = Mathf.Clamp(pos.x, LeftBound, RightBound);
			if(pos.x >= RightBound) {
				if(dragDirection == DragDirection.Right) {
					youDidGood();
				} else {
					//You did bad
					youDidBad();
				}

			} else if(pos.x <= LeftBound) {
				if(dragDirection == DragDirection.Left) {
					youDidGood();
				} else {
					//You did bad
					youDidBad();
				}
			}
			transform.position = pos;
		}
	}

	void youDidGood() {
		clicked = false;
		locked = true;
		animation.Play (GoodAnimation.name);

	}

	void youDidBad() {
	}
}
