using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndComputerController : MonoBehaviour, ISerializable {

	public Color[] colors;

	public AnimationClip lightupAnimation;

	private int type;
	private bool litUp;

	private Color colorToBe;

	public int Type {
		get {
			return type;
		}
	}

	public Dictionary<string,string> Serialize() {
		Dictionary<string,string> def = new Dictionary<string, string>();
		def.Add ("anim",lightupAnimation.name);
		
		return def;
	}
	
	public void DeSerialize(Dictionary<string,object> def) {
		lightupAnimation = animation.GetClip((string)def["anim"]);

	}

	void OnMouseUpAsButton() {
		if(litUp) {
			transform.parent.SendMessage("ComputerClicked", gameObject);
			LockIn();
		}
	}

	public void LockIn() {
		MeshAlpha interpolator = GetComponent<MeshAlpha>();
		interpolator.StartColor = renderer.material.color;
		interpolator.EndColor = colors[1];
		animation.Play (lightupAnimation.name);
	}

	public void LightUp() {
		MeshAlpha interpolator = GetComponent<MeshAlpha>();
		interpolator.StartColor = renderer.material.color;
		interpolator.EndColor = colors[0];
		this.type = 0;
		StartCoroutine(PlayLightupAnimation());

	}

	IEnumerator PlayLightupAnimation() {
		animation.Play(lightupAnimation.name);
		yield return CustomYields.WaitForAnimation(animation);
		litUp = true;
	}
}
