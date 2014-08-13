
using System.Collections;
using UnityEngine;

public class MeshAlpha : MonoBehaviour
{
	public float time;
	public bool overrideColor;

	public Color StartColor;

	public Color EndColor;

	
	void LateUpdate()
	{
		if(overrideColor) {
			renderer.material.color = Color.Lerp(StartColor, EndColor, time);
		}
	}

}
