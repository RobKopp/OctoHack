using UnityEngine;
using System.Collections.Generic;

public class PieceConfig : MonoBehaviour {
	public GameObject[] Pieces;

	public List<GameObject> getPieces(int numPieces) {
		List<GameObject> newPieces = new List<GameObject>();
		while(numPieces > 0) {
			newPieces.Add(Instantiate(Pieces[Random.Range(0, Pieces.Length)], Vector3.zero, Quaternion.identity) as GameObject);
			numPieces -= 1;
		}


		return newPieces;
	}
}
