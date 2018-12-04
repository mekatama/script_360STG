using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
	GameObject gameController;			//検索したオブジェクト入れる用
	public GameObject[] enemyObject;	//enemyのプレハブを配列で管理
	public GameObject[] bossObject;		//bossのプレハブを配列で管理
	public float timeOut;				//enemyを出現させたい時間間隔
	private float timeElapsed;			//時間を仮に格納する変数
	private int enemyType;				//enemyの種類
	private int bossType;				//bossの種類
	public GameObject enemy;
	public GameObject boss;
	private float x_pos;				//出現位置
	private float z_pos;				//出現位置
	private bool isSpawnTime;			//spawn時間変更制御用flag
	private bool isBossOnce;			//boss出現一回だけ処理

	void Start () {
		gameController = GameObject.FindWithTag ("GameController");	//GameControllerオブジェクトを探す
		enemyType = 0;							//enemyの種類初期化
		bossType = 0;							//bossの種類初期化
		enemy = null;
		boss = null;
		isBossOnce = false;
	}

	void Update () {
		//gcって仮の変数にGameControllerのコンポーネントを入れる
		GameController gc = gameController.GetComponent<GameController>();
		//15体撃破数毎にスポーン時間を短くする
		if((gc.killEnemyNum % 15) == 0){
			if(isSpawnTime == false){
				if(timeOut > 0.1f){
//					Debug.Log("enemy fueru");
					timeOut -= gc.editEnemySpawn;
					isSpawnTime = true;
				}
			}
		}else{
			isSpawnTime = false;
		}

//		Debug.Log("isBossGo : " + gc.isBossGo);
		//isBossGoでenemyとbossを制御
		if(!gc.isBossGo){
			//enemy出現
			isBossOnce = false;
			//時間チェック
			timeElapsed += Time.deltaTime;	//経過時間の保存
			if(timeElapsed >= timeOut) {	//指定した経過時間に達したら
				EnemyGo();
			}
		}else{
			//Boss出現
			if(isBossOnce == false){
				BossGo();
				isBossOnce = true;
			}
		}
	}
	
	public void EnemyGo(){
		//gcって仮の変数にGameControllerのコンポーネントを入れる
		GameController gc = gameController.GetComponent<GameController>();
		enemyType = gc.enemyType;				//GameControllerで決定した値を使用
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
		//enemyを生成する
		enemy = (GameObject)Instantiate(
			enemyObject[enemyType],						//■仮で0を入れている。0～4を想定
			new Vector3(x_pos, transform.position.y, z_pos),
			transform.rotation
		);
		gc.spawnEnemyNum += 1;		//spawn数を加算
		timeElapsed = 0.0f;			//生成時間リセット
		gc.isShildBom = false;		//ShildBom flag初期化
	}

	public void BossGo(){
//		Debug.Log("Go!!");
		//gcって仮の変数にGameControllerのコンポーネントを入れる
		GameController gc = gameController.GetComponent<GameController>();
		bossType = gc.bossType;				//GameControllerで決定した値を使用
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
		//bossを生成する
		boss = (GameObject)Instantiate(
			bossObject[bossType],						//■仮で0を入れている。0～4を想定
			new Vector3(x_pos, transform.position.y, z_pos),
			transform.rotation
		);
		timeElapsed = 0.0f;			//生成時間リセット
	}
}
