using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Color[] colors;
	public int NumSpaces;
	public float timeBetweenSpawns;
	private float countedTime;

	private bool StartGame;

	private int activeSquareCount = 0;


	IEnumerator Start() {
		yield return StartCoroutine(initBoard());
		StartGame = true;
	}

	IEnumerator initBoard() {
		for(int i = 1; i<=NumSpaces; ++i) {
			GameObject.Find(i.ToString()).SendMessage("Init");
			yield return new WaitForSeconds(0.1f);
		}
	}

	void Update() {
		if(StartGame) {
			if(itsTime()) {
				countedTime = 0;
				bool done = false;
				GameObject square = null;
			
					int space = Random.Range(1,15);
					square = GameObject.Find(space.ToString());
					done = square.GetComponent<SquareController>().Activated;
			

				activeSquareCount += 1;
				
				if(square != null){
					square.SendMessage("ActivateIt");
				}

				if(activeSquareCount == NumSpaces) {
					StartGame = false;
				}
			} else {
				countedTime += Time.deltaTime;
			}
			
		}
	}

	bool itsTime() {
		return countedTime > timeBetweenSpawns;
	}
}
