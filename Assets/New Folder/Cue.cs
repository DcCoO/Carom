using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cue : MonoBehaviour {
	public Rigidbody cueBall;
	public Transform camera;
	Transform identity;

	public Vector3 direction;
	//public int power;

	public Vector2 spin;
	float spinConstant = 0.07f;

	CueAngle cueAngle;

	float distanceToBall = 2.1f;

	void Start () {
		cueBall = GameObject.Find ("White").GetComponent<Rigidbody>();
		identity = GameObject.Find ("Cue Identity").transform;
		cueAngle = GetComponent<CueAngle> ();
	}

	private Vector3 velocity = Vector3.zero;

	bool turnedOn = true;

	void LateUpdate (){
		if (!turnedOn) return;
		Vector3 offset = (transform.right * spin.x + transform.up * spin.y) * spinConstant;
		Vector3 camProj = new Vector3 (camera.position.x, cueBall.position.y, camera.position.z);
		Vector3 pos = cueBall.position + (camProj - cueBall.position).normalized * 
			distanceToBall * Mathf.Cos(Mathf.Deg2Rad * cueAngle.angle);
		pos.y = cueBall.position.y + Mathf.Sin( Mathf.Deg2Rad * cueAngle.angle ) * distanceToBall;

		transform.position = Vector3.SmoothDamp (transform.position, pos + offset, ref velocity, 0.04f);

		transform.LookAt (cueBall.position + offset);

		print (Vector3.Distance (cueBall.position, transform.position));
	}

	public void Shot (int power = 0) { 
		cueBall.gameObject.GetComponent<Ball> ().Hit (direction, power, spin);
	}

	public void Activate(bool state){
		turnedOn = state;
	}

	public Vector3 right(float size = 1){return size * -identity.up;}
	public Vector3 up(float size = 1){return size * Vector3.up;}
	public Vector3 forward(float size = 1){return size * identity.forward;}


}
