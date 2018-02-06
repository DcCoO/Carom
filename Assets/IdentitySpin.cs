using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentitySpin : MonoBehaviour {

	public Transform cue;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (cue.position.x, transform.position.y, cue.position.z);
		transform.eulerAngles = new Vector3 (
			transform.eulerAngles.x,
			cue.eulerAngles.y,
			transform.eulerAngles.z
		);
	}
}
