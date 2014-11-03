using UnityEngine;
using System.Collections.Generic;

public class GridActioner : MonoBehaviour, ISerializable {
		
	public float totalGoodPoints;

	public AnimationClip ShowAnimation;
	public AnimationClip HideAnimation;


	public virtual Dictionary<string, string> Serialize() {
		Dictionary<string,string> props = new Dictionary<string, string>();
		if(ShowAnimation != null) {
			props.Add("SA", ShowAnimation.name);
		}

		if(HideAnimation != null) {
			props.Add ("HA", HideAnimation.name);
		}
		return props;
	}

	public virtual void DeSerialize(Dictionary<string, object> definition) {
		if(definition.ContainsKey("SA")) {
			ShowAnimation = animation.GetClip((string)definition["SA"]);
		}
		if(definition.ContainsKey("HA")) {
			HideAnimation = animation.GetClip((string)definition["HA"]);
		}
	}

	void CalculateGoodPoints() {
		totalGoodPoints = 0;
		foreach(Transform child in transform) {
			if(child.tag == "Good") {
				totalGoodPoints += 1;
			}
		}
	}


	public void Clicked(GameObject objectClicked) {
		if(objectClicked.tag == "Good") {
			totalGoodPoints -= 1;
			if(totalGoodPoints <= 0) {
				finishLevel ();
			}
		}
	}

	protected void finishLevel() {
		GameObject.FindGameObjectWithTag("LevelRoot").SendMessage("CompleteLevel");
	}

	public void Show() {
		CalculateGoodPoints();
	}

	public void Hide() {
		gameObject.SetActive(false);
	}
	
}
