using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDeadEffect : MonoBehaviour {
	ParticleSystem particle;			//爆発ParticleSystem

	void Start () {
		particle = GetComponent<ParticleSystem> ();
	}
	
	void Update () {
		//再生が終わったら
		if(particle.isPlaying == false){
			Destroy(gameObject);
		}
	}
}
