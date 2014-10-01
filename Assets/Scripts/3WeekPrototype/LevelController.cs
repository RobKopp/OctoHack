using UnityEngine;
using System.Collections.Generic;

public class LevelController : MonoBehaviour,ISerializable {

	public float TotalTime;
	public float LevelRewardSeconds;

	private float currentTime;

	private int currentLevel = 0;

	private bool started = false;

	void Start() {
		foreach(Transform child in transform) {
			child.gameObject.SetActive(false);
		}
		showLevel();
	}

	public Dictionary<string,string> Serialize() {
		Dictionary<string, string> properties = new Dictionary<string,string>();
		properties.Add("TT", TotalTime.ToString());
		properties.Add("LRS",LevelRewardSeconds.ToString());
		return properties;
	}

	public void DeSerialize(Dictionary<string,object> definition) {
		TotalTime = (float)definition["TT"];
		LevelRewardSeconds = (float) definition["LRS"];
	}

	// Update is called once per frame
	void Update () {
		if(started) {
			currentTime += Time.deltaTime;
			if(currentTime >= TotalTime) {
				Debug.Log ("You Lose!");
			}
		}
	}

	public void CompleteLevel() {
		GameObject level = transform.GetChild(currentLevel).gameObject;
		level.SendMessage("Hide");
		currentLevel += 1;
		currentTime -= LevelRewardSeconds;
		if(currentLevel >= transform.childCount) {
			Debug.Log("You Win!");
		} else {
			showLevel();
		}
	}

	void showLevel() {
		GameObject level = transform.GetChild(currentLevel).gameObject;
		level.SetActive(true);
		level.SendMessage("Show");
	}
}
