using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_UI_Score : MonoBehaviour {

	void Start () {
	}
	
	void Update () {
		// 自身の向きをカメラに向ける
        transform.rotation = Camera.main.transform.rotation;
	}
}
