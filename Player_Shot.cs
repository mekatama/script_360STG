﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shot : MonoBehaviour {
	GameObject gameController;						//検索したオブジェクト入れる用
	public GameObject bulletObject = null;			//弾プレハブ
	public Transform bulletStartPosition = null;	//弾の発射位置を取得する
	public float timeOut = 0.4f;					//弾の連射間隔
	private float timeElapsed = 0.0f;				//弾の連射間隔カウント用

	void Start () {
		gameController = GameObject.FindWithTag ("GameController");	//GameControllerオブジェクトを探す
	}

	void Update () {
		//gcって仮の変数にGameControllerのコンポーネントを入れる
		GameController gc = gameController.GetComponent<GameController>();
		if(gc.shotLevel == 1){
			//アタッチしたオブジェクト名で分岐
			if(this.gameObject.name == "ShotPoint1"){
				Shot();
			}
		}else if(gc.shotLevel == 2){
			//アタッチしたオブジェクト名で分岐
			if(this.gameObject.name == "ShotPoint2" || this.gameObject.name == "ShotPoint3"){
				Shot();
			}
		}else if(gc.shotLevel == 3){
			//アタッチしたオブジェクト名で分岐
			if(this.gameObject.name == "ShotPoint4" || this.gameObject.name == "ShotPoint5" || this.gameObject.name == "ShotPoint1"){
				Shot();
			}
		}
	}

	void Shot(){
		//弾の自動連射
		timeElapsed += Time.deltaTime;
        if(timeElapsed >= timeOut) {
			//弾を生成する位置を指定する
			Vector3 vecBulletPos	= bulletStartPosition.position;
			//弾を生成する
			Instantiate( bulletObject, vecBulletPos, transform.rotation);
			timeElapsed = 0.0f;
		}
	}
}
