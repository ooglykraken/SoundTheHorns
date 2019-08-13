using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour {

	public List<GameObject> enemies = new List<GameObject>();
	
	public void Awake(){
		
	}
	
	public void Update(){
		
	}
	
	private static Gameplay instance = null;

	public static Gameplay Instance(){
		if(instance == null){
			instance = GameObject.Find("Gameplay").GetComponent<Gameplay>();
		}
		
		return instance;
	}
}

