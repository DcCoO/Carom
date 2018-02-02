using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinController : MonoBehaviour {
	
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
		print (ball.rectTransform.position);
		angle.rectTransform.position = new Vector3 (ball.rectTransform.rect.width / 2,
			Screen.height - ball.rectTransform.rect.height / 2,
			0
		);
		radius = ball.rectTransform.rect.width / 2 - safeDistance;
		ballOrigin = angle.rectTransform.position;
		spin = Vector2.zero;
	}
	

	void Update () {
		if (Input.GetMouseButton (0)) {
			
			if (ball.enabled) {
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
			else {
				Interaction.SetOrbit (false);
				ball.enabled = angle.enabled = true;
				position = Input.mousePosition;
			}
		} 
		else if (Input.GetMouseButtonUp (0)) {
			ball.enabled = angle.enabled = false;
			Interaction.SetOrbit (true);
		}
	}
}
