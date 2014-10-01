using UnityEngine;
using System.Collections;

public class FlickingController : MonoBehaviour {

	Vector3 startingPos;
	Vector2 lastMouse, firstMouse, flickDirection;
	bool dragging, flying;
	public float FlickPower;
	

	void OnMouseDown() {
		if(!flying) {
			dragging = true;
			firstMouse = Input.mousePosition;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(dragging) {
			if(Input.GetMouseButtonUp(0)) {
				lastMouse = Input.mousePosition;
				flickDirection = (lastMouse - firstMouse);
				if(flickDirection.y < 0) {
					flickDirection.y = -flickDirection.y;
				}
				flickDirection.Normalize();
				rigidbody2D.AddForce(flickDirection * FlickPower);
				dragging = false;
				flying = true;
				transform.parent.SendMessage("PerformFlick");
			}
		} else if(flying) {
			if(transform.position.x > 3.3 || transform.position.x < -3.3 ||
			   transform.position.y < -5.5) {
				transform.parent.SendMessage("FinishFlick", false);
			}
		}


	}

	void OnTriggerEnter2D(Collider2D collider) {

	}
}
