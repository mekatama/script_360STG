using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour {
	GameObject gameController;			//検索したオブジェクト入れる用
	public int enemyHp;					//EnemyのHP
	public int enemy_score;				//enemyの点数
	public float enemyMoveSpeed = 5.0f;	//1秒間にenemyが進む距離
	public GameObject item1;			//Enemyから出現させるアイテム
	GameObject targetPos;
	private Vector3 vecAddPos;
	private bool isHitStop;
	public float hitStoptime = 0.2f;	//HitStop間隔
	private float timeElapsed = 0.0f;	//HitStopカウント用

	void Start () {
		gameController = GameObject.FindWithTag ("GameController");	//GameControllerオブジェクトを探す
		targetPos = GameObject.FindWithTag ("Target");				//
		isHitStop = false;
	}
	
	void Update () {
		Vector3 target = targetPos.transform.position; // キャラクタ位置
		transform.LookAt(target);
		//1秒間の移動量
		vecAddPos = (Vector3.forward * enemyMoveSpeed);
		//移動
		if(isHitStop == false){
			//移動量、回転量には Time.deltaTime をかけて実行環境(フレーム数の差)による違いが出ないようにします
			transform.position += ((transform.rotation * vecAddPos) * Time.deltaTime);
		}else{
			//HitStop中はtransform.position処理に行かない
			timeElapsed += Time.deltaTime;
			if(timeElapsed >= hitStoptime) {
				timeElapsed = 0.0f;
				isHitStop = false;
			}
		}
	}

	//他のオブジェクトとの当たり判定
	void OnTriggerEnter( Collider other) {
		//gcって仮の変数にGameControllerのコンポーネントを入れる
		GameController gc = gameController.GetComponent<GameController>();
		if(other.tag == "Bullet"){
			enemyHp = enemyHp - gc.attackPower;	//攻撃力をHPから引く
			isHitStop = true;					//on
			//死亡判定
			if(enemyHp <= 0){
				//スコア加算
				gc.total_Score += enemy_score;
				Destroy(gameObject);	//このGameObjectを［Hierrchy］ビューから削除する
				//アイテムを落とす
				Instantiate (item1, transform.position, transform.rotation);
			}
		}
	}
}
