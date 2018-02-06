using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
	Rigidbody rb;
	public int number;
	public float drag;
	public float spinDuration;
	public Vector3 velocity;
	Transform identity;


	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.maxAngularVelocity = 50;
		rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
		identity = GameObject.Find ("Cue Identity").transform;
	}


	void Update(){
		if (Input.GetKeyDown (KeyCode.A) && number == 0) {
			GameObject.Find ("Cue").GetComponent<Cue> ().Shot ();
		}

		DrawArrow.ForDebug (transform.position, Vector3.up, Color.blue);
		DrawArrow.ForDebug (transform.position, identity.up, Color.red);
	}

	// Update is called once per frame
	void FixedUpdate () {
		velocity = rb.velocity;
		//rb.velocity = new Vector3 (rb.velocity.x, 0, rb.velocity.z);
		if (rb.velocity.magnitude > 0.14f) {
			rb.AddForce (-rb.velocity * drag, ForceMode.Impulse);
		} else
			rb.velocity = Vector3.zero;

		if (rb.angularVelocity.magnitude > 0.205f) {
			rb.angularVelocity *= spinDuration;
		} else
			rb.angularVelocity = Vector3.zero;
	}


	public void Hit(Vector2 direction, float power, Vector2 spin){
		rb.angularVelocity = Vector3.zero;
		//Vector3 effect = new Vector3 (spin.y * rb.maxAngularVelocity, -spin.x * rb.maxAngularVelocity, 0);
		//GameObject.Find ("Cue").GetComponent<Rigidbody> ().AddTorque (effect, ForceMode.Impulse);
		//rb.AddTorque (effect, ForceMode.Impulse);
		rb.AddTorque (Vector3.up  * -spin.x, ForceMode.Impulse);
		rb.AddTorque (identity.up * -spin.y, ForceMode.Impulse);
		rb.AddForce (power * new Vector3(direction.x, 0, direction.y), ForceMode.Impulse);

	}
}
