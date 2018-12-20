using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ShildRotation : MonoBehaviour {
	public int enemyRotate;				//enemyの回転量

	void Update () {
		transform.Rotate(new Vector3(0,enemyRotate,0) * Time.deltaTime); 
	}
}
