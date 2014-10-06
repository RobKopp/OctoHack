using UnityEngine;
using System.Collections.Generic;

public class FinalGridActioner : MonoBehaviour {

	List<Transform> neutralComputers;
	List<Transform> goodComputers;
	List<Transform> badComputers;

	public int UpperBound, LowerBound;

	void FindComputers() {
		foreach(Transform child in transform) {
			if(child.tag=="Computer") {
				neutralComputers.Add(child);
			}
		}
	}

	void Update() {
		int numComputers = Random.Range(LowerBound, UpperBound);
		numComputers = Mathf.Clamp(numComputers, 0, neutralComputers.Count);
		if(numComputers > 0) {

		} else {
			//We're done!
		}
	}

	void Show() {

	}

	void Hide() {
	}

}
