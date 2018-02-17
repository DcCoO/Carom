using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
	Rigidbody rb;
	public int number;
	public float drag;
	public float spinDuration;
	public float wallDamp;
	Vector3 lastVelocity = Vector3.zero;
	Transform identity;
	Vector3 spin;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.maxAngularVelocity = 100;
		rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
		identity = GameObject.Find ("Cue Identity").transform;
		//spinDuration = new Vector3 (0.985f, 1, 0.985f);
	}


	void Update(){
		if (Input.GetKeyDown (KeyCode.LeftControl)){
			Rigidbody[] rbs = FindObjectsOfType<Rigidbody> ();
			foreach (Rigidbody rb in rbs)
				rb.velocity = rb.angularVelocity = Vector3.zero; 
		}

		DrawArrow.ForDebug (transform.position, Vector3.up, Color.blue);
		//DrawArrow.ForDebug (transform.position, identity.up, Color.red);
		//DrawArrow.ForDebug (transform.position, identity.forward, Color.green);
	}

	public Vector3 right 	{ get { return Vector3.Cross (Vector3.up, rb.velocity.normalized); } }
	public Vector3 up	 	{ get { return Vector3.up; } }
	public Vector3 forward	{ get { return rb.velocity.normalized; } }

	public float sideSpin	{ get { return spin.x; } }
	public float topSpin	{ get { return spin.y; } }



	void FixedUpdate () {

		//DrawArrow.ForDebug (transform.position, rb.velocity.normalized);

		//reduzir o velocity
		rb.velocity -= rb.velocity.magnitude > 0.14f ? rb.velocity * drag : rb.velocity;


		//corrigir o velocity de acordo com a rotacao
		//////////// RESETAR K A CADA TACADA (DE ACORDO COM INCLINACAO) E DIMINUIR ELE A CADA FRAME
		float K = 0.04f;
		rb.AddForce(K * forward * SpinIntensityAroundAxis(right) * Time.deltaTime, ForceMode.VelocityChange);
		rb.AddForce(K * right * SpinIntensityAroundAxis(forward) * Time.deltaTime, ForceMode.VelocityChange);


		//ampliar a rotacao de acordo com o velocity
		rb.AddTorque(SpinAddedByVelocity(rb.velocity));


		//reduzir a rotacao
		rb.angularVelocity *= rb.angularVelocity.magnitude > 0.205f ? spinDuration : 0f;

		//parar tudo
		if (rb.velocity.magnitude < 0.006f && rb.angularVelocity.magnitude < 4f) {
			rb.velocity = rb.angularVelocity = Vector3.zero;
		}

		lastVelocity = rb.velocity;
		//print (rb.angularVelocity);
	}
		


	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "wall") {
			ContactPoint cp = col.contacts [0];


			//90 = raspando : 180 = perpendicular
			//90               0
			lastVelocity =  AngleAddedBySpin(180f - Vector3.Angle (lastVelocity, cp.normal)) * 
				Vector3.Reflect (lastVelocity, cp.normal) * wallDamp;
			
			//print ((180f - Vector3.Angle (lastVelocity, rb.velocity))/2f);

			//refletir de acordo com angulo, forca e efeito
			rb.velocity = lastVelocity; 
		}
	}
		



	//spin for running
	float SpinIntensityAroundAxis(Vector3 axis){
		return Vector3.Dot (axis, rb.angularVelocity);
	}

	Vector3 SpinAddedByVelocity(Vector3 velocity){
		//velocidade = intensidade do giro = variavel q multiplica o eixo de rotacao
		Vector3 axis = Vector3.Cross (Vector3.up, velocity.normalized);
		float currentSpin = SpinIntensityAroundAxis(axis);
		//se intensidadeAtual + a ser adicionado > precisa ser = nao faz
		float delta = 40 * Time.deltaTime * velocity.magnitude;
		return currentSpin + delta > 10 * velocity.magnitude ? Vector3.zero : delta * axis;
	}

	//spin for collision
	//0 - 35
	//65 - 0

	float maxContactAngle = 65f, maxAngle = 30f;
	Quaternion AngleAddedBySpin(float contactAngle){
		//print ("sideSpin = " + sideSpin + " --- contato = " + contactAngle +" --- desviada de " + Mathf.Max (0, 35f - 0.684f * contactAngle));
		if (contactAngle > maxContactAngle) return Quaternion.Euler (0, 0, 0);

		print ("CONTACT: " + contactAngle + ", RATE: " + ((maxContactAngle - contactAngle) / maxContactAngle) + ", SPIN: " +
			((maxContactAngle - contactAngle) / maxContactAngle) * 
			(maxAngle * (sideSpin/rb.maxAngularVelocity))); 



		return Quaternion.Euler(
			0, 
			((maxContactAngle - contactAngle) / maxContactAngle) * 
			(maxAngle * (sideSpin/rb.maxAngularVelocity)), 
			0
		);
	}







	public void Hit(Vector2 direction, float power, Vector2 spin){
		rb.angularVelocity = Vector3.zero;
		this.spin = spin * -rb.maxAngularVelocity;
		//Vector3 effect = new Vector3 (spin.y * rb.maxAngularVelocity, -spin.x * rb.maxAngularVelocity, 0);
		//GameObject.Find ("Cue").GetComponent<Rigidbody> ().AddTorque (effect, ForceMode.Impulse);
		//rb.AddTorque (effect, ForceMode.Impulse);
		rb.AddTorque (Vector3.up  * -this.spin.x, ForceMode.VelocityChange);
		rb.AddTorque (identity.up * -this.spin.y, ForceMode.VelocityChange);
		rb.AddForce (power * new Vector3(direction.x, 0, direction.y), ForceMode.VelocityChange);
	}
}
