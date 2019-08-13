using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	
	private float mass = 2f;
	public float velocity = 6f;
	
	public bool grounded;
	
	public int health = 1;
	
	public int damage = 1;
	
	private Rigidbody rigidbody;
	
	public List<GameObject> enemiesContainer = new List<GameObject>();
	
	public GameObject enemiesParent;
	
	public DefenseZone invadingZone = null;
	
	public void Awake(){
		rigidbody = gameObject.GetComponent<Rigidbody>();
		
		enemiesContainer = Gameplay.Instance().enemies;
		enemiesParent = Gameplay.Instance().gameObject;
		
		velocity *= Random.Range(.9f, 1.1f);
	}
	
	public void FixedUpdate(){
		Inertia();
		
		grounded = Ground();
		
		if(grounded){
			March();
		} else {
			GravityPulledFall();
		}
	}
	
	public void Update(){
		
	}
	
	public void OnCollisionEnter(Collision c){
		if(c.transform.tag == "Player"){
			Die();
		}
	}
	
	private void March(){
		rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, new Vector3(-velocity, 0f, 0f), Time.deltaTime * mass);
	}
	
	private void Inertia(){
		rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, Vector3.zero, Time.deltaTime * .25f * mass);
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
		rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y  - mass, rigidbody.velocity.z);
	}
	
	public void Die(){
		if(invadingZone != null){
			invadingZone.contents.Remove(this.gameObject);
		}
		
		GameObject.Destroy(this.gameObject);
	}
}
