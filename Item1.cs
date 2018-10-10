using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1 : MonoBehaviour {
	public float itemMoveSpeed = 5.0f;	//1秒間にenemyが進む距離
	private Vector3 vecAddPos;
	GameObject targetPos;
	private bool isStop;
	public float stopTime = 0.2f;		//stop間隔
	private float timeElapsed = 0.0f;	//Stopカウント用

	void Start () {
		targetPos = GameObject.FindWithTag ("Target");	//
		isStop = true;
	}
	
	void Update () {
		Vector3 target = targetPos.transform.position; // キャラクタ位置
		transform.LookAt(target);
		//1秒間の移動量
		vecAddPos = (Vector3.forward * itemMoveSpeed);
		//移動
		if(isStop == false){
			//移動量、回転量には Time.deltaTime をかけて実行環境(フレーム数の差)による違いが出ないようにします
			transform.position += ((transform.rotation * vecAddPos) * Time.deltaTime);
		}else{
			//HitStop中はtransform.position処理に行かない
			timeElapsed += Time.deltaTime;
			if(timeElapsed >= stopTime) {
				timeElapsed = 0.0f;
				isStop = false;
			}
		}
	}
}
