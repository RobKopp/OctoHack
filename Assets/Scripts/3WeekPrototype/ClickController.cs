using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ClickController : MonoBehaviour,ISerializable {

	public AnimationClip ClickAnimation;
	bool clicked = false;

	public Dictionary<string,string> Serialize() {
		Dictionary<string,string> def = new Dictionary<string, string>();
		def.Add ("anim",ClickAnimation.name);
		
		return def;
	}
	
	public void DeSerialize(Dictionary<string,object> def) {
		ClickAnimation = animation.GetClip((string)def["anim"]);
		
	}

	//When it gets clicked!
	void OnMouseUpAsButton() {
		if(!clicked) {
			StartCoroutine(playAnimation());
		}
	}

	IEnumerator playAnimation() {
		animation.Play(ClickAnimation.name);
		yield return CustomYields.WaitForAnimation(animation);
		transform.parent.SendMessage("Clicked",gameObject);
	}
}
