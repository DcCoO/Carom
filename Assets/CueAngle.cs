using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueAngle : MonoBehaviour {


	public Transform target;
	float distance;

	float yMinLimit = 0f;
	float yMaxLimit = 89f;

	float offset;

	public Cue cue;
	CueAngle cueAngle;

	public float angle = 0.0f;

	GameObject camera;
	MouseMove mouseMove;

	// Use this for initialization
	void Start () {
		//Vector3 angles = transform.eulerAngles;
		//angle = angles.x;
		angle = 0;
		cueAngle = cue.GetComponent<CueAngle> ();

		camera = GameObject.Find ("Main Camera");
		mouseMove = camera.GetComponent<MouseMove> ();

	}

	void LateUpdate () {
		angle += Input.GetAxis ("Mouse Y") * Shift.speed;
		angle = ClampAngle(angle, yMinLimit, yMaxLimit);
		//angle = Mathf.Max (angle, /*cueAngle.angle*/3);


		/*
		Quaternion rotation = Quaternion.Euler(angle, camera.transform.eulerAngles.y, 0);

		distance = GameObject.Find("Main Camera").GetComponent<MouseMove> ().distance;

		Vector3 negDistance = new Vector3(0, 0, -distance);
		Vector3 position = rotation * negDistance + target.position;

		camera.transform.rotation = rotation;
		camera.transform.position = position;


		cue.direction = new Vector2 (
			cue.cueBall.transform.position.x - camera.transform.position.x,
			cue.cueBall.transform.position.z - camera.transform.position.z
		).normalized;
		*/
	}


	public void Turn(bool state, float altAngle){
		if (state) {
			offset = altAngle - angle;
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
