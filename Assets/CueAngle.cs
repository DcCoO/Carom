using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueAngle : MonoBehaviour {


	public Transform target;
	float distance;

	float yMinLimit = 3f;
	float yMaxLimit = 89f;

	public Cue cue;
	Alt alt;

	public float angle = 0.0f;
	float initialAngle;
	float initialAltAngle;

	void Start () {
		alt = GameObject.Find ("Main Camera").GetComponent<Alt> ();
		angle = 0;
	}

	void LateUpdate () {
		angle += Input.GetAxis ("Mouse Y") * 1.5f * Shift.speed;
		angle = ClampAngle(angle, yMinLimit, yMaxLimit);
	}


	public void Turn(bool state){
		if (state) {
			if(alt == null) alt = GameObject.Find ("Main Camera").GetComponent<Alt> ();
			initialAltAngle = alt.angle;
		}
		initialAngle = angle;
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
