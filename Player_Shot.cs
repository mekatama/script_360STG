using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shot : MonoBehaviour {
	public GameObject bulletObject = null;			//弾プレハブ
	public Transform bulletStartPosition = null;	//弾の発射位置を取得する
	public float timeOut = 0.4f;					//弾の連射間隔
	private float timeElapsed = 0.0f;				//弾の連射間隔カウント用

	void Update () {
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
