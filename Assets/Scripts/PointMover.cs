using UnityEngine;
using System.Collections;


public class PointMover : MonoBehaviour {

	public float SpeedLow;
	public float SpeedHigh;
	

	void StartMoving() {
		float speed = Random.Range (SpeedLow, SpeedHigh);
		rigidbody2D.AddForce(transform.forward * speed);
	}
}
