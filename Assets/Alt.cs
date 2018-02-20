using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alt : MonoBehaviour {

	public Transform target;
	float distance;

	float yMinLimit = 0f;
	float yMaxLimit = 89f;

	public Cue cue;

	public float angle;

	// Use this for initialization
	void Start () {
		angle = 0;


		// Make the rigid body not change rotation
	}

	void LateUpdate () {
		angle += Input.GetAxis ("Mouse Y") * Shift.speed;
		angle = ClampAngle(angle, yMinLimit, yMaxLimit);

		Quaternion rotation = Quaternion.Euler(angle, transform.eulerAngles.y, 0);

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