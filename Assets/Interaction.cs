using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour {

	public MouseMove mouseMove;
	public Spin spin;
	public Alt alt;
	public Cue cue;
	public Ctrl ctrl;
	public CueAngle cueAngle;


	//CueMove atrelado a camera

	//public static MouseOrbitImproved orbit;

	//shift			: none
	//ctrl
	//alt			: mouse move
	//mouse0		: all
	//mouse1
	//mouse move
	//mouse wheel


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//holding shift
		Shift.Activate (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)); 

		//holding mouse0 (spin)
		spin.Turn(Input.GetMouseButton (0));
		if(spin.enabled) {Set (false, true, false); return;}

		//TODO: holding mouse1 (cue angle)
		cueAngle.Turn(Input.GetMouseButton(1), mouseMove.distance);
		if (cueAngle.enabled) {Set (false, false, false); return;}

		//TODO: holding ctrl (shot)
		ctrl.Activate(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl));
		cue.Activate(!ctrl.turnedOn);
		if (ctrl.turnedOn) {Set (false, true, false); return;}

		//TODO: press tab
		if(Input.GetKeyDown(KeyCode.Tab)) Tab.Turn ();

		//holding alt (camera height)
		alt.enabled = Input.GetKey (KeyCode.LeftAlt) || Input.GetKey (KeyCode.RightAlt);
		if (alt.enabled) {Set (false, false, true); return;}

		//not holding anything
		Set(true, false, false);


	}


	void Set(bool mouseMove, bool spin, bool alt){
		this.mouseMove.enabled = mouseMove;
		this.spin.enabled = spin;
		this.alt.enabled = alt;
	}



}
