using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Cue : MonoBehaviour {
	public Rigidbody cueBall;

	public Vector3 direction;
	public float soften;
	public int power;

	public Vector2 spin;
	public float spinConstant;

	public float cueAngle;


	void Start () {
		soften = 0.55f;
		spinConstant = 5;
		cueBall = GameObject.Find ("White").GetComponent<Rigidbody>();
	}
		

	public void Shot () {
		print ("spin: " + spin + ", spinConst: " + spinConstant); 
		cueBall.gameObject.GetComponent<Ball> ().Hit (direction, soften * power, spin * spinConstant);
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
		c.power = EditorGUILayout.IntField("Power:", c.power); 

		//EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal ();

		EditorGUILayout.Vector2Field ("Spin", c.spin);
		EditorGUILayout.EndHorizontal ();


		if (GUILayout.Button ("Shot")) {
			c.cueBall.gameObject.GetComponent<Ball> ().Hit (c.direction, c.soften * c.power, c.spinConstant * c.spin);
		}


	}

}
#endif