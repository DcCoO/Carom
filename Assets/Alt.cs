using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alt : MonoBehaviour {

	public Transform target;
	float distance;

	float yMaxLimit = 89f;

	public Cue cue;
	CueAngle cueAngle;

	public float angle;

	void Start () {
		angle = 3;
		cueAngle = cue.GetComponent<CueAngle> ();
	}

	void LateUpdate () {
		angle += Input.GetAxis ("Mouse Y") * Shift.speed;
		angle = ClampAngle(angle, cueAngle.angle, yMaxLimit);

		Quaternion rotation = Quaternion.Euler(angle, transform.eulerAngles.y, 0);

		distance = GetComponent<MouseMove> ().distance;

		Vector3 negDistance = new Vector3(0, 0, -distance);
		Vector3 position = rotation * negDistance + target.position;

		transform.rotation = rotation;
		transform.position = position;
	}

	public static float ClampAngle(float angle, float min, float max){
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}
}