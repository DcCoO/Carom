using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour {

	public SpinController spin;
	public static MouseOrbitImproved orbit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void SetOrbit(bool b){

		if (Input.GetMouseButton (0)) {

		}

		if (orbit == null) {
			orbit = GameObject.Find("Main Camera").GetComponent<MouseOrbitImproved>();
		}
		orbit.enabled = b;
	}

}
