using UnityEngine;
using System.Collections.Generic;

public class FinalGridActioner : MonoBehaviour,ISerializable {

	List<GameObject> neutralComputers;
	List<GameObject> goodComputers;
	List<GameObject> badComputers;

	public int UpperBound, LowerBound;
	public float timeToWait;
	private float currentTime;


	public Dictionary<string,string> Serialize() {
		Dictionary<string,string> def = new Dictionary<string, string>();
		def.Add ("UB",UpperBound.ToString());
		def.Add ("LB", LowerBound.ToString());
		def.Add ("TTW", timeToWait.ToString());
		
		return def;
	}
	
	public void DeSerialize(Dictionary<string,object> def) {
		UpperBound = int.Parse((string)def["UB"]);
		LowerBound = int.Parse((string)def["LB"]);
		timeToWait = float.Parse((string)def["TTW"]);
		
	}

	void Start() {
		neutralComputers = new List<GameObject>();
		goodComputers = new List<GameObject>();
		badComputers = new List<GameObject>();
		FindComputers();
	}

	void FindComputers() {
		foreach(Transform child in transform) {
			if(child.tag=="Computer") {
				child.gameObject.SetActive(false);
			}
		}
		List<int> comps = SelectRandomComputers(transform.childCount);
		foreach(int comp in comps) {
			GameObject computer = transform.GetChild(comp).gameObject;
			neutralComputers.Add(computer);
			computer.SetActive(true);
		}
		currentTime = timeToWait;
	}

	void ComputerClicked(GameObject gameObject) {
		neutralComputers.Remove(gameObject);
		if(gameObject.GetComponent<EndComputerController>().Type == 0) {
			goodComputers.Add(gameObject);
		} else {
			badComputers.Add (gameObject);
		}
	}

	List<int> SelectRandomComputers(int numComputers) {
		//We need to make selections from the number available
		List<int> computersToLightUp = new List<int>();
		int numPasses = 0;
		while(computersToLightUp.Count < numComputers) {
			int number = Random.Range(0, numComputers);
			if(!computersToLightUp.Contains(number)) {
				computersToLightUp.Add(number);
			}
			numPasses += 1;
		}

		return computersToLightUp;
	}

	void Update() {
		if(currentTime >= timeToWait) {
			int numComputers = Random.Range(LowerBound, UpperBound);
			numComputers = Mathf.Clamp(numComputers, 0, neutralComputers.Count);
			if(numComputers > 0) {
				List<int> computersToLightUp = SelectRandomComputers(numComputers);
				foreach(int comp in computersToLightUp) {
					neutralComputers[comp].SendMessage("LightUp");
				}
			} else {
				//We're done!
			}
			currentTime = 0;
		} else {
			currentTime += Time.deltaTime;
		}
	}

	void Show() {

	}

	void Hide() {
	}



}
