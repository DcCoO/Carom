using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alt : MonoBehaviour {

	public Transform target;
	float distance;

	public float yMinLimit = -20f;
	public float yMaxLimit = 80f;

	Rigidbody rigidbody;

	public Cue cue;

	float y = 0.0f;

	// Use this for initialization
	void Start () {
		Vector3 angles = transform.eulerAngles;
		y = angles.x;

		rigidbody = GetComponent<Rigidbody>();

		// Make the rigid body not change rotation
		if (rigidbody != null){
			rigidbody.freezeRotation = true;
		}
	}

	void LateUpdate () {
		y += Input.GetAxis ("Mouse Y") * Shift.speed;
		y = ClampAngle(y, yMinLimit, yMaxLimit);
		y = Mathf.Max (y, 2.36f);

		Quaternion rotation = Quaternion.Euler(y, transform.eulerAngles.y, 0);

		distance = GetComponent<MouseMove> ().distance;

		Vector3 negDistance = new Vector3(0, 0, -distance);
		Vector3 position = rotation * negDistance + target.position;

		transform.rotation = rotation;
		transform.position = position;

		cue.direction = new Vector2 (
			cue.cueBall.transform.position.x - transform.position.x,
			cue.cueBall.transform.position.z - transform.position.z
		).normalized;
	}

	public static float ClampAngle(float angle, float min, float max){
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}
}
