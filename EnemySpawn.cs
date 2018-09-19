using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
//	GameObject gameController;			//検索したオブジェクト入れる用
	public GameObject[] enemyObject;	//enemyのプレハブを配列で管理
	public float timeOut;				//enemyを出現させたい時間間隔
	private float timeElapsed;			//時間を仮に格納する変数
	private int enemyType;				//enemyの種類
	public GameObject enemy;

	void Start () {
//		gameController = GameObject.FindWithTag ("GameController");	//GameControllerオブジェクトを探す
		enemyType = 0;							//(仮)okasiの種類
		enemy = null;
	}

	void Update () {
		//時間チェック
		timeElapsed += Time.deltaTime;	//経過時間の保存
        if(timeElapsed >= timeOut) {	//指定した経過時間に達したら
			EnemyGo();
		}
	}
	
	public void EnemyGo(){
		enemyType = 0;	//仮
//		float x_pos = Random.Range(-3.5f,3.5f); //ランダムで出現位置を決める
		//okasiを生成する
		enemy = (GameObject)Instantiate(
			enemyObject[enemyType],						//■仮で0を入れている。0～4を想定
			new Vector3(transform.position.x, transform.position.y, transform.position.z),
			transform.rotation
		);
		Debug.Log("enemy1 go");
		timeElapsed = 0.0f;			//生成時間リセット
	}
}
