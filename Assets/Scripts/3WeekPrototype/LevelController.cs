using UnityEngine;
using System.Collections.Generic;

public class LevelController : MonoBehaviour,ISerializable {

	public float TotalTime;
	public float LevelRewardSeconds;

	private GameObject Grids;

	private float currentTime;

	private int currentLevel = 0;

	private bool started = false;

	void Start() {
		Grids = transform.FindChild("Grids").gameObject;
		foreach(Transform child in Grids.transform) {
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
		TotalTime = float.Parse((string)definition["TT"]);
		LevelRewardSeconds = float.Parse((string)definition["LRS"]);
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
		GameObject level = Grids.transform.GetChild(currentLevel).gameObject;
		level.SendMessage("Hide");
		currentLevel += 1;
		currentTime -= LevelRewardSeconds;
		if(currentLevel >= Grids.transform.childCount) {
			Debug.Log("You Win!");
		} else {
			showLevel();
		}
	}

	void showLevel() {
		GameObject level = Grids.transform.GetChild(currentLevel).gameObject;
		Debug.Log (level.name);
		level.SetActive(true);
		level.SendMessage("Show");
	}
}
