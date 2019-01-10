using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_UIDestroy : MonoBehaviour {

	void Start () {
		Destroy(gameObject,1.0f);	//オブジェクトの削除
	}
}
