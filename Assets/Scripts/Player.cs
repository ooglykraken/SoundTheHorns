using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public int health;
	
	public int damage; 
	
	private AudioSource audioSource;
	
	public AudioClip rock;
	public AudioClip bolt;
	public AudioClip arrow;
	public AudioClip fire;
	
	public GameObject rockZone;
	public GameObject arrowZone;
	public GameObject boltZone;
	public GameObject catapaultZone;
	
	public float playCooldown = 1f;
	public float playTimer;
	
	public void Awake(){
		audioSource = gameObject.GetComponent<AudioSource>();
		
	}
	
	public void Update(){
		playTimer -= Time.deltaTime;
		
		if(playTimer > 0f){
			return;
		}
		
		if(Input.GetKeyDown("q")){
			
			Rock();
			audioSource.clip = rock;
			audioSource.Play();
			playTimer = playCooldown;
		}
		
		if(Input.GetKeyDown("w")){
			Bolt();
			audioSource.clip = bolt;
			audioSource.Play();
			playTimer = playCooldown;
		}
		
		if(Input.GetKeyDown("e")){
			
			Arrow();
			audioSource.clip = arrow;
			audioSource.Play();
			playTimer = playCooldown;
		}
		
		if(Input.GetKeyDown("r")){
			Catapault();
			
			audioSource.clip = fire;
			audioSource.Play();
			playTimer = playCooldown;
		}
	}
	
	private void Arrow(){
		GameObject arrow = Instantiate(Resources.Load("Arrow", typeof (GameObject)) as GameObject) as GameObject;
		arrow.transform.parent = this.transform;
		
		if(arrowZone.GetComponent<DefenseZone>().contents.Count > 0){
			arrow.GetComponent<Arrow>().target = arrowZone.GetComponent<DefenseZone>().contents[0];
		}
	}
	private void Rock(){
		GameObject rock = Instantiate(Resources.Load("Rock", typeof (GameObject)) as GameObject) as GameObject;
		rock.transform.parent = this.transform;
	}
	
	private void Bolt(){
		GameObject bolt = Instantiate(Resources.Load("Bolt", typeof (GameObject)) as GameObject) as GameObject;
		bolt.transform.parent = this.transform;
		
		if(boltZone.GetComponent<DefenseZone>().contents.Count > 0){
			bolt.GetComponent<Bolt>().target = boltZone.GetComponent<DefenseZone>().contents[0];
		}
	}
	
	private void Catapault(){
		GameObject shot = Instantiate(Resources.Load("Shot", typeof (GameObject)) as GameObject) as GameObject;
		shot.transform.parent = this.transform;
	
		if(catapaultZone.GetComponent<DefenseZone>().contents.Count > 0){
			// shot.GetComponent<Shot>().target = catapaultZone.GetComponent<DefenseZone>().contents[0];
		}
	}
	
	private static Player instance = null;
	
	public static Player Instance(){
		if(instance == null){
			instance = GameObject.Find("Player").GetComponent<Player>();
		}
		
		return instance;
	}
}
