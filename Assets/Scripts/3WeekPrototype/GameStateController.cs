using UnityEngine;
using System.Collections;

public class GameStateController : MonoBehaviour {

	public enum GameState {
		Hacking,
		Email
	}

	private GameState currentState;

	public GameObject EmailScreen;
	public GameObject LevelScreen;

	// Use this for initialization
	void Start () {
		currentState = GameState.Email;
		EmailScreen.SendMessage("Initialize");
		EmailScreen.SendMessage("LoadToLevel", PlayerGlobalsSingleton.Instance.GetComponent<PlayerProgression>().CurrentLevel);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
