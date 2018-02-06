using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl : MonoBehaviour {

	public Transform cueBall;
	Transform camera;
	float minDist = 1.7f;
	float maxDist = 5;
	float mouseConstant = 0.1f;
	float mouseY;

	Vector2 spin;

	float distance = 2;
	// Use this for initialization
	void Start () {
		camera = GameObject.Find ("Main Camera").transform;
		spin = GetComponent<Cue> ().spin;
	}

	Vector3 velocity = Vector3.zero;

	public bool turnedOn = false;
	// Update is called once per frame
	void Update () {
		if (!turnedOn) return;

		distance = Mathf.Clamp(distance - Input.GetAxis("Mouse Y") * 0.48f, minDist, maxDist);

		Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
		Vector3 position = transform.rotation * negDistance + cueBall.position;

		transform.position = position;
		transform.LookAt (cueBall.position + 
			transform.right * spin.x * 0.07f +
			transform.up    * spin.y * 0.07f
		);

	}

	public void Activate(bool state){
		if(!turnedOn && state) this.mouseY = mouseY;
		turnedOn = state;
	}
}
