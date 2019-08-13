using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {

	public float lifetime;
	
	private Rigidbody rigidbody;
	
	private float mass = 100f;
	
	private float gravityConstant = 10f;
	
	public bool grounded;
	
	public GameObject target;
	
	private float horizontalVelocity = 100f;
	private float verticalVelocity = 70f;
	
	private float initialVelocity;
	
	// private float releaseAngle = 45f;
	// private float maxAngle = -60f;
	// private float angleTotal;
	
	public float adjustedHorizontalVelocity;
	public float adjustedVerticalVelocity;
	
	public void Awake(){
		rigidbody = gameObject.GetComponent<Rigidbody>();
		// angleTotal = Mathf.Abs(releaseAngle) + Mathf.Abs(maxAngle);
	}
	
	public void Start(){
		
		Vector3 startPoint = new Vector3(.55f, 1f, 0f);
		
		transform.localPosition = startPoint;
		
		Vector3 targetPosition;
		
		adjustedHorizontalVelocity = horizontalVelocity;
		
		adjustedVerticalVelocity = verticalVelocity;
		
		initialVelocity = Mathf.Sqrt(Mathf.Pow(horizontalVelocity, 2f) + Mathf.Pow(verticalVelocity, 2f));
		
		rigidbody.velocity = new Vector3(adjustedHorizontalVelocity, adjustedVerticalVelocity, 0f);
	}
	
	public void Update(){
		Debug.Log(rigidbody.velocity.x);

		
		if(lifetime <= 0){
			Destroy();
		} else {
			lifetime -= Time.deltaTime;
		}
		
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

		}
	}
	
	private void Destroy(){
		GameObject.Destroy(gameObject);
	}
	
	private void Inertia(){
		rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, Vector3.zero, Time.deltaTime);
	}
	
	private bool Ground(){
		
		float distance = 1f;
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
		rigidbody.velocity -= new Vector3(0f, gravityConstant, 0f);
	}
}
