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
	private float x_pos;				//出現位置
	private float z_pos;				//出現位置

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
		int spawnPos = Random.Range(0,4);		//ランダムで出現サイドを決める
		//出現位置
		switch(spawnPos){
			case 0:
				x_pos = Random.Range(-4.0f,4.0f); //ランダムで出現位置を決める
				z_pos = 8.0f;
//				Debug.Log("上");
				break;
			case 1:
				x_pos = Random.Range(-4.0f,4.0f); //ランダムで出現位置を決める
				z_pos = -8.0f;
//				Debug.Log("下");
				break;
			case 2:
				x_pos = 4.0f;
				z_pos = Random.Range(-8.0f,8.0f); //ランダムで出現位置を決める
//				Debug.Log("右");
				break;
			case 3:
				x_pos = -4.0f;
				z_pos = Random.Range(-8.0f,8.0f); //ランダムで出現位置を決める
//				Debug.Log("左");
				break;
		}
		//okasiを生成する
		enemy = (GameObject)Instantiate(
			enemyObject[enemyType],						//■仮で0を入れている。0～4を想定
			new Vector3(x_pos, transform.position.y, z_pos),
			transform.rotation
		);
		timeElapsed = 0.0f;			//生成時間リセット
	}
}
