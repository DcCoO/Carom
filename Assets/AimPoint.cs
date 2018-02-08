﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	Vector3 Hitpoint(GameObject source, Vector3 d, GameObject target){

		Vector3 s = source.transform.position;
		Vector3 t = target.transform.position;

		float radius = source.GetComponent<SphereCollider> ().radius;
		float distance = 2 * radius;

		//representar equacao parametrica da reta
		//	x = s.x + p * d.x
		//	y = s.y + p * d.y
		//	z = s.z + p * d.z

		//achar equacao da esfera com centro em target e raio 2*radius
		//	(x - t.x)² + (y - t.y)² + (z - t.z)² = radius²

		//achar os pontos de cruzamento entre a reta e a esfera
		//	http://www.siggraph.org/education/materials/HyperGraph/raytrace/rtinter1.htm
		//	t0 = (- B + (B^2 - 4*C)^1/2) / 2
		//	t1 = (- B - (B^2 - 4*C)^1/2) / 2
		//	float B = 2 * (d.x * (s.x - t.x) + d.y * (s.y - t.y) + d.z * (s.z - t.z));

		float B = -2 * (d.x * (s.x - t.x) + d.y * (s.y - t.y) + d.z * (s.z - t.z));	
		float C = pow2(s.x - t.x) + pow2(s.y - t.y) + pow2(s.z - t.z) - pow2(distance);
		float delta = Mathf.Sqrt(pow2 (B) - 4 * C);
		float p0 = (B + delta) * 0.5f;
		float p1 = (B - delta) * 0.5f;

		Vector3 hit0 = s + p0 * d;
		Vector3 hit1 = s + p1 * d;
		return Vector3.Distance (s, hit0) < Vector3.Distance (s, hit1) ? hit0 : hit1;
	}

	float pow2(float n){
		return n * n;
	}
}
