using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public Transform target;
	Vector3 offset;

	public static Vector3 cameraPos;

	// Use this for initialization
	void Start () {
		cameraPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetMouseButton (0)) {
			transform.position = target.position - offset;
		}
		else offset = target.position - transform.position;
	}
}
