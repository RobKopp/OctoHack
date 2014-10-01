using UnityEngine;
using System.Collections.Generic;

public class GridActioner : MonoBehaviour, ISerializable {
		
	public float totalGoodPoints;

	public AnimationClip ShowAnimation;
	public AnimationClip HideAnimation;


	public Dictionary<string, string> Serialize() {
		Dictionary<string,string> props = new Dictionary<string, string>();
		props.Add("SA", ShowAnimation.name);
		props.Add ("HA", HideAnimation.name);
		return props;
	}

	public void DeSerialize(Dictionary<string, object> definition) {
		ShowAnimation = animation.GetClip((string)definition["SA"]);
		HideAnimation = animation.GetClip((string)definition["HA"]);
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
				transform.parent.SendMessage("CompleteLevel");
			}
		}
	}

	public void Show() {
		CalculateGoodPoints();
	}

	public void Hide() {
		gameObject.SetActive(false);
	}
	
}
