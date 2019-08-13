using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour {

	public float lifetime = 3f;
	
	private Rigidbody rigidbody;
	
	private float mass = 7f;
	
	private float gravityConstant = 10f;
	
	public bool grounded;
	
	public GameObject target;
	
	private float horizontalVelocity = 50f;
	private float verticalVelocity = 50f;
	
	private float initialVelocity;
	
	private float releaseAngle = 60f;
	private float maxAngle = -60f;
	private float angleTotal;
	
	public float adjustedHorizontalVelocity;
	public float adjustedVerticalVelocity;
	
	public void Awake(){
		rigidbody = gameObject.GetComponent<Rigidbody>();
		angleTotal = Mathf.Abs(releaseAngle) + Mathf.Abs(maxAngle);
	}
	
	public void Start(){
		
		Vector3 startPoint = new Vector3(0f, 1f, 0f);
		
		transform.localPosition = startPoint;
		
		Vector3 targetPosition;
		
		adjustedHorizontalVelocity = horizontalVelocity;
		
		adjustedVerticalVelocity = verticalVelocity;
		
		initialVelocity = Mathf.Sqrt(Mathf.Pow(horizontalVelocity, 2f) + Mathf.Pow(verticalVelocity, 2f));
		
		rigidbody.velocity = new Vector3(adjustedHorizontalVelocity, adjustedVerticalVelocity, 0f);
		
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, releaseAngle);
	}
	
	public void Update(){
		
		if(lifetime <= 0){
			Destroy();
		} else {
			lifetime -= Time.deltaTime;
		}
		
		float speedRatio = rigidbody.velocity.x / horizontalVelocity;
		
		float angleFromSpeed = speedRatio * angleTotal;
		
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, maxAngle + angleFromSpeed);
	}
	
	public void FixedUpdate(){
		// Inertia();
		
		grounded = Ground();
		
		if(grounded){
			rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			
		} else {
			rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
			GravityPulledFall();
		}

	}
	
	public void OnCollisionEnter(Collision c){
		if(c.transform.tag == "Enemy"){
			if(!grounded)
				GameObject.Destroy(c.gameObject);

		} else if(c.transform.tag == "Bolt"){
			// GameObject.Destroy(c.gameObject);
		}
	}
	
	private void Destroy(){
		GameObject.Destroy(gameObject);
	}
	
	private void Inertia(){
		rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, Vector3.zero, Time.deltaTime * mass);
	}
	
	private bool Ground(){
		
		float distance = gameObject.GetComponent<Collider>().bounds.extents.y * 1.1f;
		RaycastHit hit;
		
		Vector3 ray = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		
		if(Physics.Raycast(ray, new Vector3(0f, -1f, 0f), out hit)){
			if(Vector3.Distance(transform.position, hit.point) <= distance && hit.transform.tag == "Ground"){
				return true;
			}
			
		}
		
		return false;
	}
	
	private void GravityPulledFall(){
		rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y  - mass, rigidbody.velocity.z);
	}
}
