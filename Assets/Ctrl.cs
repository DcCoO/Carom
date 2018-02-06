using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl : MonoBehaviour {

	public Transform cueBall;
	Transform camera;
	float minDist = 1.7f;
	float maxDist = 5;
	float mouseConstant = 0.2f;
	float mouseY;

	Vector2 spin;

	float distance;
	float nextDistance;

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

		nextDistance = Mathf.Clamp(distance - Input.GetAxis("Mouse Y") * mouseConstant, minDist, maxDist);
		print ("DISTANCE = " + distance + "      OFFSET = " + (Input.GetAxis("Mouse Y") * mouseConstant));

		transform.position -= transform.forward * (nextDistance - distance);
		distance = nextDistance;

	}

	public void Activate(bool state){
		if(!turnedOn && state) distance = 2.1f;
		turnedOn = state;
	}
}



/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl : MonoBehaviour {

	public Transform cueBall;
	Transform camera;
	float minDist = 1.7f;
	float maxDist = 5;
	float mouseConstant = 0.2f;
	float mouseY;

	Vector2 spin;

	float distance;

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

		distance = Mathf.Clamp(distance - Input.GetAxis("Mouse Y") * mouseConstant, minDist, maxDist);
		print ("DISTANCE = " + distance + "      OFFSET = " + (Input.GetAxis("Mouse Y") * mouseConstant));

		transform.position -= transform.forward * (distance - Vector3.Distance (transform.position, cueBall.position));

	}

	public void Activate(bool state){
		if(!turnedOn && state) distance = 2.1f;
		turnedOn = state;
	}
}
*/