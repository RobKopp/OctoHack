using UnityEngine;
using System.Collections.Generic;
using MiniJSON;

public class DragController : MonoBehaviour, ISerializable {

	public enum DragDirection {
		LeftRight,
		UpDown
	}

	public AnimationClip GoodAnimation;
	public AnimationClip BadAnimation;

	public float RightBound, LeftBound, LowerBound, UpperBound;

	public Color[] colors;

	public DragDirection dragDirection;

	public Transform GoalLoc;
	public Vector3 _goalLoc;


	private bool clicked;
	private bool locked;

	void Start() {
		if(dragDirection == DragDirection.LeftRight) {
			renderer.material.color = colors[0];
		} else if(dragDirection == DragDirection.UpDown) {
			renderer.material.color = colors[1];
		}

		if(GoalLoc != null) {
			_goalLoc = GoalLoc.position;
		}
	}

	public Dictionary<string,string> Serialize() {
		Dictionary<string,string> def = new Dictionary<string, string>();
		Vector3 goalLoc = _goalLoc;
		if(GoalLoc != null) {
			goalLoc = GoalLoc.position;
		}
		def.Add ("G",goalLoc.ToString());
		
		return def;
	}
	
	public void DeSerialize(Dictionary<string,object> def) {
		_goalLoc = Json.ParseVector3((string)def["G"]);
		
	}

	void OnMouseDown() {
		if(!locked) {
			clicked = true;
		}
	}

	void Update() {
		if(clicked) {
			if(Input.GetMouseButtonUp(0)) {
				clicked = false;
			}
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3 pos = transform.position;
			if(dragDirection == DragDirection.LeftRight) {
				pos.x = mousePos.x;
				pos.x = Mathf.Clamp(pos.x, LeftBound, RightBound);
			} else {
				pos.y = mousePos.y;
				pos.y = Mathf.Clamp(pos.y, LowerBound, UpperBound);
			}
			transform.position = pos;
			if((transform.position - _goalLoc).sqrMagnitude <= 0.01) {
				youDidGood();
			}
		}
	}

	void youDidGood() {
		clicked = false;
		locked = true;
		transform.parent.SendMessage("Clicked", gameObject);
		if(GoodAnimation != null) {
			animation.Play (GoodAnimation.name);
		}

	}

	void youDidBad() {
	}
}
