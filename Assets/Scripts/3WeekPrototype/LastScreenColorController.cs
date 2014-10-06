using UnityEngine;
using System.Collections;

public class LastScreenColorController : MonoBehaviour {

	public enum ComputerType {
		Good,
		Bad,
		Neutral
	}

	Color[] TypeColors;
	
	void UpdateColor(ComputerType type) {
		switch(type) {
		case ComputerType.Good:
			renderer.material.color = TypeColors[0];
			break;
		case ComputerType.Bad:
			renderer.material.color = TypeColors[1];
			break;
		case ComputerType.Neutral:
			renderer.material.color = TypeColors[2];
			break;
		}
	}
}
