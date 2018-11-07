using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explotion : MonoBehaviour {
		private float tempScale = 1.0f;

	// Use this for initialization
	void Start () {
		Destroy(gameObject,0.15f);	//オブジェクトの削除
	}
	
	// Update is called once per frame
	void Update () {
		tempScale -= 0.1f;
		this.transform.localScale = new Vector3(tempScale, tempScale, tempScale);
	}
}
