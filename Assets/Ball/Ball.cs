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
	CueAngle cueAngle;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.maxAngularVelocity = 100;
		rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
		identity = GameObject.Find ("Cue Identity").transform;
		cueAngle = GameObject.Find ("Cue").GetComponent<CueAngle> ();
	}


	void Update(){
		if (Input.GetKeyDown (KeyCode.LeftControl)){
			Rigidbody[] rbs = FindObjectsOfType<Rigidbody> ();
			foreach (Rigidbody rb in rbs)
				rb.velocity = rb.angularVelocity = Vector3.zero; 
		}
	}

	public Vector3 right 	{ get { return Vector3.Cross (Vector3.up, rb.velocity.normalized); } }
	public Vector3 up	 	{ get { return Vector3.up; } }
	public Vector3 forward	{ get { return rb.velocity.normalized; } }

	public float sideSpin	{ get { return spin.x; } }
	public float topSpin	{ get { return spin.y; } }

	Vector3 hitSide, hitForward;

	void FixedUpdate () {

		DrawArrow.ForDebug (transform.position, hitSide.normalized, Color.green);
		DrawArrow.ForDebug (transform.position, hitForward.normalized, Color.red);

		//reduzir o velocity
		rb.velocity -= rb.velocity.magnitude > 0.14f ? rb.velocity * drag : rb.velocity;


		//corrigir o velocity de acordo com a rotacao
		//////////// RESETAR K A CADA TACADA (DE ACORDO COM INCLINACAO) E DIMINUIR ELE A CADA FRAME
		//float K = Mathf.Max(spinIntensity, 0.005f);
		//print (K);
		//rb.AddTorque(K * right * SpinIntensityAroundAxis(right) * Time.deltaTime, ForceMode.VelocityChange);
		//rb.AddTorque(K * forward * SpinIntensityAroundAxis(forward) * Time.deltaTime, ForceMode.VelocityChange);

		//os angulos de efeito abaixo tem q ser MANTIDOS, esse right eh calculado de acordo com o velocity
		//entao ele muda a todo frame, quando dar o hit, o angulo tem q ser salvo (TESTAR)

		rb.AddForce(hitForward, ForceMode.VelocityChange);
		rb.AddForce(hitSide, ForceMode.VelocityChange);
		//spinIntensity -= Time.deltaTime;
		float K = 0.95f;
		hitSide *= K; hitForward *= K;	

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
			lastVelocity = AngleAddedBySpin (180f - Vector3.Angle (lastVelocity, cp.normal)) *
			Vector3.Reflect (lastVelocity, cp.normal) * wallDamp;

			//COLOCAR SPIN FOR ANGLE PRA MODIFICAR O ANGULO QUE A BOCA RICOCHETEIA
			//	SpinForAngle();
			
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

		//print ("CONTACT: " + contactAngle + ", RATE: " + ((maxContactAngle - contactAngle) / maxContactAngle) + ", SPIN: " +
		//	((maxContactAngle - contactAngle) / maxContactAngle) * 
		//	(maxAngle * (sideSpin/rb.maxAngularVelocity))); 



		return Quaternion.Euler(
			0, 
			((maxContactAngle - contactAngle) / maxContactAngle) * 
			(maxAngle * (sideSpin/rb.maxAngularVelocity)), 
			0
		);
	}







	public void Hit(Vector2 direction, float power, Vector2 spin){
		//seno do angulo do taco
		float angleForce = sin (cueAngle.angle);

		//quanto maior o angulo do taco, menor o efeito pro lado
		//spin.x *= (1 - angleForce);

		//zera rotacao
		rb.angularVelocity = Vector3.zero;
		//corrige a escala do efeito
		this.spin = spin * -rb.maxAngularVelocity;

		//adiciona efeito lateral
		rb.AddTorque (Vector3.up  * this.spin.x, ForceMode.VelocityChange);
		//adiciona efeito frontal
		rb.AddTorque (identity.up * this.spin.y, ForceMode.VelocityChange);
		//adiciona forca de acordo com angulo do taco
		rb.AddForce (power * (1 - angleForce) * new Vector3(direction.x, 0, direction.y), ForceMode.VelocityChange);

		//direcao frontal gerada pelo efeito
		hitForward = new Vector3 (direction.x, 0, direction.y).normalized;
		if(spin.y < 0) hitForward = rotatePointAroundAxis(hitForward);
		//direcao lateral gerada pelo efeito
		hitSide = Vector3.Cross (Vector3.up, hitForward);
		print (hitSide);
		if(spin.x < 0) hitSide = rotatePointAroundAxis(hitSide);
		if(spin.y < 0) hitSide = rotatePointAroundAxis(hitSide);
		print (hitSide);
		//escalando os vetores do efeito
		hitForward *= Mathf.Abs (spin.y) * 1 * angleForce;
		hitSide	   *= Mathf.Abs (spin.x) * 1 * CurveForAngle();
		print (hitSide);
	}

	float CurveForAngle(){
		return cueAngle.angle < 45 ? 0 : (cueAngle.angle - 45f)/45f;
	}

	float SpinForAngle(){
		float spinIncrease = 1.2f;
		return cueAngle.angle <= 45 ? 
			((spinIncrease - 1) * cueAngle.angle) / 45f + 1f : 
			(spinIncrease * (90 - cueAngle.angle)) / 45;
	}

	Vector3 rotatePointAroundAxis(Vector3 point) {
		return Quaternion.AngleAxis(180f, Vector3.up) * point;
	}

	float sin(float angle){
		return Mathf.Sin (angle * Mathf.Deg2Rad);
	}
}
