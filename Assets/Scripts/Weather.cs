using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour {

	private AudioSource audioSource;

	public AudioClip nature;
	public AudioClip rain;
	
	public int timeTilChange = 300;
	
	public void Awake(){
		audioSource = gameObject.GetComponent<AudioSource>();
	}
	
	public void Update(){
		timeTilChange--;
		
		if(timeTilChange <= 0){
			if(audioSource.clip == nature){
				audioSource.clip = rain;
				audioSource.Play();
			} else {
				audioSource.clip = nature;
				audioSource.Play();
			}
			timeTilChange = Random.Range(540, 800) - Random.Range(0, 240);
		}
	}
	
	private static Weather instance = null;
	
	public static Weather Instance(){
		if(instance == null){
			instance = GameObject.Find("Weather").GetComponent<Weather>();
		}
		
		return instance;
	}
}
