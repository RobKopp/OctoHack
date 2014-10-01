using UnityEngine;
using System.Collections.Generic;

public class GridController : MonoBehaviour {

	public PieceConfig config;

	public float Width;
	public float Height;

	List<GameObject> pieces;

	// Use this for initialization
	void Start () {
		pieces = config.getPieces(10);
		foreach(GameObject piece in pieces) {
			piece.transform.parent = transform;
			Vector2 point;
			int maxTries = 100;
			int numObj = 0;
			do {
				point = Random.insideUnitCircle;
				point.Normalize();
				point.x *= Width /2;
				point.y *= Height/2;
				maxTries -= 1;
				Collider2D[] results = new Collider2D[1];
				numObj = Physics2D.OverlapCircleNonAlloc(point, (piece.collider2D as CircleCollider2D).radius, results);
			} while(numObj > 0 && maxTries > 0);

			piece.transform.localPosition = (Vector3)point;
			piece.transform.LookAt(transform.position);
			piece.SendMessage("StartMoving");
		}
	}
}
