using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Cue : MonoBehaviour {
	public Rigidbody cueBall;
	Transform camera;

	public Vector3 direction;
	public float soften;
	//public int power;

	public Vector2 spin;
	float spinConstant = 0.07f;

	public float cueAngle;

	float distanceToBall = 2.1f;

	void Start () {
		cueBall = GameObject.Find ("White").GetComponent<Rigidbody>();
		camera = GameObject.Find ("Main Camera").transform;
	}

	private Vector3 velocity = Vector3.zero;

	bool turnedOn = true;

	void LateUpdate (){
		if (!turnedOn) return;
		Vector3 offset = (transform.right * spin.x + transform.up * spin.y) * spinConstant;

		Vector3 targetPos = cueBall.position + (camera.position - cueBall.position).normalized * distanceToBall;
		targetPos.y -= 0.5f;
		transform.position = Vector3.SmoothDamp (transform.position, targetPos + offset, ref velocity, 0.1f);

		transform.LookAt (cueBall.position + offset);
	}
		

	public void Shot (int power = 0) { 
		cueBall.gameObject.GetComponent<Ball> ().Hit (direction, power, spin);
	}

	public void Activate(bool state){
		turnedOn = state;
	}


}


#if UNITY_EDITOR

[CustomEditor(typeof(Cue))]
class CueEditor : Editor {

	public override void OnInspectorGUI(){
		//DrawDefaultInspector ();
		Cue c = (Cue)target;

		EditorGUIUtility.labelWidth = 70;

		c.cueBall = (Rigidbody)EditorGUILayout.ObjectField ("Cue Ball:", c.cueBall, typeof(Rigidbody), true);

		//EditorGUILayout.BeginHorizontal ();
		c.direction = EditorGUILayout.Vector2Field ("Direction", c.direction);
		//c.power = EditorGUILayout.IntField("Power:", c.power); 

		//EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal ();

		EditorGUILayout.Vector2Field ("Spin", c.spin);
		EditorGUILayout.EndHorizontal ();


		//if (GUILayout.Button ("Shot")) {
		//	c.cueBall.gameObject.GetComponent<Ball> ().Hit (c.direction, c.soften * c.power, c.spin);
		//}


	}

}
#endif