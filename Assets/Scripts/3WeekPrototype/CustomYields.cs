using UnityEngine;
using System.Collections;

public class CustomYields : MonoBehaviour {

	public static IEnumerator WaitForAnimation(Animation animation)
	{
		do
		{
			yield return null;
		} while (animation.isPlaying);
	}
}
