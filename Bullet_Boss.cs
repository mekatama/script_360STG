using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Boss : MonoBehaviour {
	GameObject gameController;			//検索したオブジェクト入れる用
	public GameObject particle;			//爆発Particle

	void Start () {
		gameController = GameObject.FindWithTag ("GameController");	//GameControllerオブジェクトを探す
	}
	
	// Update is called once per frame
	void Update () {
		//gcって仮の変数にGameControllerのコンポーネントを入れる
		GameController gc = gameController.GetComponent<GameController>();
		//boss弾特殊処理
		if(gc.isBossGo == false){
			Destroy(gameObject);	//このGameObjectを［Hierrchy］ビューから削除する
			//爆発effect
			Instantiate (particle, transform.position, transform.rotation);
		}
	}
}
