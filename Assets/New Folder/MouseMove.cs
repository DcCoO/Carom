using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class MouseMove : MonoBehaviour {

	public Transform target;
	public float distance = 10.0f;
	public float zSpeed = 2.4f;

	public float distanceMin = 0.5f;
	public float distanceMax = 1f;

	private Rigidbody rigidbody;

	public Cue cue;

	float x = 0;

	// Use this for initialization
	void Start () {
		Vector3 angles = transform.eulerAngles;
		x = angles.y;

		rigidbody = GetComponent<Rigidbody>();

		// Make the rigid body not change rotation
		if (rigidbody != null){
			rigidbody.freezeRotation = true;
		}
	}

	void LateUpdate () {
		x -= Input.GetAxis ("Mouse X") * distance * Shift.speed;

		Quaternion rotation = Quaternion.Euler(transform.eulerAngles.x, x, 0);

		distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 30 * Shift.speed, distanceMin, distanceMax);
		distance = Mathf.Clamp(distance - Input.GetAxis("Mouse Y") * 0.48f * Shift.speed, distanceMin, distanceMax);

		Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
		Vector3 position = rotation * negDistance + target.position;

		transform.rotation = rotation;
		transform.position = position;

		cue.direction = new Vector2 (
			cue.cueBall.transform.position.x - transform.position.x,
			cue.cueBall.transform.position.z - transform.position.z
		).normalized;


		DrawArrow.ForDebug (cue.cueBall.transform.position,
			(cue.cueBall.transform.position - new Vector3 (
				transform.position.x, 
				cue.cueBall.transform.position.y,
				transform.position.z
			)).normalized * 5
		);

	}

	public static float ClampAngle(float angle, float min, float max){
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}
}