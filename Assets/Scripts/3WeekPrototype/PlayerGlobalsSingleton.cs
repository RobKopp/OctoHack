using UnityEngine;
using System.Collections;

public class PlayerGlobalsSingleton : MonoBehaviour {
	
	public static PlayerGlobalsSingleton Instance
	{
		get
		{
			return _instance;
		}
	}
	private static PlayerGlobalsSingleton _instance;

	
	void OnEnable()
	{
		if (_instance == null)
		{
			_instance = this;
		}
	}
	
	void OnDisable()
	{
		if (_instance == this)
		{
			_instance = null;
		}
	}
}
