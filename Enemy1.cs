using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour {
	GameObject gameController;	//検索したオブジェクト入れる用
	public int enemyHp;			//EnemyのHP
	public int enemy_score;		//enemyの点数
//	public GameObject item;		//Enemyから出現させるアイテム

	void Start () {
		gameController = GameObject.FindWithTag ("GameController");		//GameControllerオブジェクトを探す
	}
	
	// Update is called once per frame
	void Update () {
	}

	//他のオブジェクトとの当たり判定
	void OnTriggerEnter( Collider other) {
		if(other.tag == "Bullet"){
//			enemyHp = enemyHp - s.attackPower;			//Bulletスクリプトの攻撃力をHPから引く
//			if(enemyHp <= 0){
				//スコア加算
				//gcって仮の変数にGameControllerのコンポーネントを入れる
				GameController gc = gameController.GetComponent<GameController>();
				gc.total_Score += enemy_score;
				Debug.Log("score:" + gc.total_Score);
				Destroy(gameObject);	//このGameObjectを［Hierrchy］ビューから削除する
//			}
		}
	}
}
