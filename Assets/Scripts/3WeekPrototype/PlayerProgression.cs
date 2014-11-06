using UnityEngine;
using System.Collections;

public class PlayerProgression : MonoBehaviour {

	public int currentLevel = 0;
	
	public int CurrentLevel {
		get {
			return currentLevel;
		}

		set {
			currentLevel = value;
		}
	}
}
