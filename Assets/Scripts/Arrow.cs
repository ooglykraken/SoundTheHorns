using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
	
	public int damage = 1;
	
	public GameObject target;
	
	private Rigidbody rigidbody;
	
	private float mass = 2f;
	
	private float gravityConstant = 10f;
	
	private float horizontalVelocity = 90f;
	private float verticalVelocity = 80f;
	
	private float initialVelocity;
	
	public float adjustedHorizontalVelocity;
	public float adjustedVerticalVelocity;
	
	// private float castleTime = 19f; // Average time for arrow to drop fall to the ground from its descent past starting altitude.
	
	private bool grounded;
	
	private float releaseAngle = 60f;
	private float maxAngle = -80f;
	private float angleTotal;
	
	private float lifetime;
		
	public void Awake(){
		rigidbody = gameObject.GetComponent<Rigidbody>();
		angleTotal = Mathf.Abs(releaseAngle) + Mathf.Abs(maxAngle);
	}
	
	public void Start(){
		Vector3 startPoint = new Vector3(.55f, .55f, 0f);
		
		transform.localPosition = startPoint;
		
		Vector3 targetPosition;
		
		adjustedHorizontalVelocity = horizontalVelocity;
		
		adjustedVerticalVelocity = verticalVelocity;
		
		initialVelocity = Mathf.Sqrt(Mathf.Pow(horizontalVelocity, 2f) + Mathf.Pow(verticalVelocity, 2f));
		
		rigidbody.velocity = new Vector3(adjustedHorizontalVelocity, adjustedVerticalVelocity, 0f);
		
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, releaseAngle);
	}
	
	public void Update(){
		float speedRatio;
		float newAngle;
		
		float yVelocity = rigidbody.velocity.y;
		
		if(yVelocity < 0f){
		} else {
			
		}
		
		lifetime += Time.deltaTime;
	}
	
	public void FixedUpdate(){
		// Inertia();
		
		grounded = Ground();
		
		if(grounded){
			//turn off
			Destroy();
		} else {
			GravityPulledFall();
		}

	}
	
	public void OnCollisionEnter(Collision c){
		if(c.transform.tag == "Enemy"){
			c.gameObject.GetComponent<Enemy>().Die();

			target = null;
			Destroy();
		}
	}
	
	private void Destroy(){
		
		GameObject.Destroy(this.gameObject);
	}
	
	private void Inertia(){
		rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, Vector3.zero, Time.deltaTime * mass);
	}
	
	private bool Ground(){
		
		float distance = .6f;
		RaycastHit hit;
		
		Vector3 ray = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		
		if(Physics.Raycast(ray, -transform.up, out hit)){
			if(Vector3.Distance(transform.position, hit.point) <= distance && hit.transform.tag == "Ground"){
				return true;
			}
			
		}
		
		return false;
	}
	
	private void GravityPulledFall(){
		rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y  - gravityConstant, rigidbody.velocity.z);
	}
}
