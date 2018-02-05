using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab {

	static Camera mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
	static Camera tabCamera  = GameObject.Find("Tab Camera").GetComponent<Camera>();
	static bool active = false;

	public static void Turn(){
		active = !active;
		mainCamera.enabled = !active;
		tabCamera.enabled = active;
	}
}
