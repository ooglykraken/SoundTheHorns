using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseZone : MonoBehaviour {

	public List<GameObject> contents = new List<GameObject>();
	
	public void Update(){
		// contents.RemoveAll(EmptyCull);
	}
	
	private bool EmptyCull(GameObject g){
		if(g != null){
			return false;
			
		}
		
		return true;
	}
	
	public void OnTriggerEnter(Collider c){
		if(c.transform.tag == "Enemy"){
			contents.Add(c.gameObject);
			c.gameObject.GetComponent<Enemy>().invadingZone = this;
		}
	}
	
	public void OnTriggerExit(Collider c){
		if(c.transform.tag == "Enemy"){
			contents.Remove(c.gameObject);
			// c.gameObject.GetComponent<Enemy>().invadingZone = null;
		}
	}	
}
