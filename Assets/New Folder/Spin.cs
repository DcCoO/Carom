using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Spin : MonoBehaviour {
	
	public Image ball;
	public Image angle;
	public Cue cue;
	Vector2 ballOrigin;
	Vector2 position;
	float radius;
	public Vector2 spin;
	float safeDistance = 3;

	// Use this for initialization
	void Start () {
		ball.enabled = angle.enabled = false;
		ball.rectTransform.position = new Vector3 (
			ball.rectTransform.rect.width/2f,
			Screen.height - ball.rectTransform.rect.height/2,
			0
		);
		angle.rectTransform.position = new Vector3 (ball.rectTransform.rect.width / 2,
			Screen.height - ball.rectTransform.rect.height / 2,
			0
		);
		radius = ball.rectTransform.rect.width / 2 - safeDistance;
		ballOrigin = angle.rectTransform.position;
		spin = Vector2.zero;
	}
	
	public void Turn(bool state){
		if (state) {
			ball.enabled = angle.enabled = true;
		} else {
			ball.enabled = angle.enabled = false;
		}
		enabled = state;
	}


	void Update () {
		Vector2 delta = (Vector2) Input.mousePosition - this.position;
		if (delta == Vector2.zero)
			return;
		Vector2 newPos = (Vector2) angle.rectTransform.position + delta;
		float distance = (newPos - ballOrigin).magnitude;

		if (distance > radius) {
			//print ("VECTOR: " + ((radius / distance) * ((Vector2)Input.mousePosition - ballOrigin)));
			//newPos = ballOrigin + ((radius / distance) * ((Vector2)Input.mousePosition - ballOrigin));
			//spin.rectTransform.position = newPos;
			//print("NEW POS: " + newPos);
		} else {
			angle.rectTransform.position = newPos;
			spin.x = (angle.rectTransform.position.x - ballOrigin.x) / radius;
			spin.y = (angle.rectTransform.position.y - ballOrigin.y) / radius;
			cue.spin = spin;
		}
		this.position = Input.mousePosition;
	}
}
