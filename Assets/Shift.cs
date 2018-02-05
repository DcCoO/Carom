using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shift {

	public static float speed = 1;

	public static void Activate(bool slow){
		speed = slow ? 0.2f : 1;
	}
}
