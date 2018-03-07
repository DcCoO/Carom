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
	Cue cue;

	Vector2 spin;

	float distance;
	float nextDistance;

	public bool shooting = false;
	int power;

	// Use this for initialization
	void Start () {
		camera = GameObject.Find ("Main Camera").transform;
		cue = GetComponent<Cue> ();
		spin = cue.spin;
	}

	Vector3 velocity = Vector3.zero;

	public bool turnedOn = false;
	// Update is called once per frame
	void Update () {
		if (!turnedOn) return;

		if (shooting) {
			transform.position += transform.forward * mouseConstant;
			distance -= mouseConstant;
			if (distance < minDist) {
				shooting = false;
				cue.Shot (power);
			}
			return;			
		}

		nextDistance = Mathf.Clamp(distance - Input.GetAxis("Mouse Y") * mouseConstant, minDist, maxDist);

		transform.position -= transform.forward * (nextDistance - distance);
		distance = nextDistance;

		if (Input.GetKeyDown (KeyCode.Space)) {
			shooting = true;
			power = (int) (20.0f * ((distance - minDist) / (maxDist - minDist)));
			print ("VAI EXPRODIR COM POWER " + power);
		}

	}

	public void Activate(bool state){
		if (!turnedOn && state) {
			distance = 2.1f;
			shooting = false;
		}
		turnedOn = state;
	}
}


