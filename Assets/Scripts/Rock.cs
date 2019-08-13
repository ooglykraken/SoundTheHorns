using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {
	
	
	private Rigidbody rigidbody;
	
	private float mass = 10f;
	
	private float gravityConstant = 10f;
	
	public bool grounded;
	
	public void Awake(){
		rigidbody = gameObject.GetComponent<Rigidbody>();
		
	}
	
	public void Start(){
		Vector3 parentPosition = transform.parent.position;
		Vector3 rockEdges = gameObject.GetComponent<Collider>().bounds.extents;
		Vector3 startPoint = new Vector3(.6f + rockEdges.x/10f, 1f, 0f);
		
		transform.localPosition = startPoint;
		
		rigidbody.velocity = new Vector3(0f, 0f, 0f);
		
	}
	
	public void FixedUpdate(){
		Inertia();
		
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
			if(!grounded)
				GameObject.Destroy(c.gameObject);

		}
	}
	
	private void Destroy(){
		GameObject.Destroy(gameObject);
	}
	
	private void Inertia(){
		rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, Vector3.zero, Time.deltaTime * mass);
	}
	
	private bool Ground(){
		
		float distance = 2f;
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
