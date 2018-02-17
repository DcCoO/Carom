using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueAngle : MonoBehaviour {

	public Transform target;
	float distance;

	float yMinLimit = -20f;
	float yMaxLimit = 89f;

	Rigidbody rigidbody;

	public Cue cue;

	public float angle = 0.0f;

	// Use this for initialization
	void Start () {
		Vector3 angles = transform.eulerAngles;
		angle = angles.x;

		rigidbody = GetComponent<Rigidbody>();

		// Make the rigid body not change rotation
		if (rigidbody != null){
			rigidbody.freezeRotation = true;
		}
	}

	void LateUpdate () {
		angle += Input.GetAxis ("Mouse Y") * Shift.speed;
		angle = ClampAngle(angle, yMinLimit, yMaxLimit);
		angle = Mathf.Max (angle, 2.36f);

		Quaternion rotation = Quaternion.Euler(angle, transform.eulerAngles.y, 0);

		//distance = GameObject.Find ("Main Camera").GetComponent<Alt> ().getDist ();//GetComponent<MouseMove> ().distance;

		Vector3 negDistance = new Vector3(0, 0, -distance);
		Vector3 position = rotation * negDistance + target.position;

		transform.rotation = rotation;
		transform.position = position;

		//cue.direction = new Vector2 (
		//	cue.cueBall.transform.position.x - transform.position.x,
		//	cue.cueBall.transform.position.z - transform.position.z
		//).normalized;
	}

	public void Turn(bool state, float distance = 0f){
		if (state) {
			this.distance = distance;
		} 
		enabled = state;
	}

	public static float ClampAngle(float angle, float min, float max){
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}

}
