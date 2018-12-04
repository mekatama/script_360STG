using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	GameObject gameController;			//検索したオブジェクト入れる用
	private float rayRange = 1000f;			//レイを飛ばす距離
	private Vector3 targetPosition;			//移動する位置

	void Start () {
		gameController = GameObject.FindWithTag ("GameController");	//GameControllerオブジェクトを探す
		targetPosition = transform.position;	//playerの位置
	}
	
	void Update () {
		//マウスクリックの位置に向く
		//マウスのタッチとタッチしっぱなしを判定
		if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			//地面にFieldレイヤーを設定してタッチ位置を取得
			if(Physics.Raycast(ray, out hit, rayRange, LayerMask.GetMask ("Field"))) {
				targetPosition = hit.point;
				targetPosition.y = 0.5f;			//y座標固定
				transform.LookAt(targetPosition);	//向く
			}
		}
	}

	//他のオブジェクトとの当たり判定
	void OnTriggerEnter( Collider other) {
		//gcって仮の変数にGameControllerのコンポーネントを入れる
		GameController gc = gameController.GetComponent<GameController>();
		if(other.tag == "Enemy"){
			if(!gc.isMuteki){
				gc.isGameOver = true;
				Destroy(gameObject);	//このGameObjectを［Hierrchy］ビューから削除する
			}
//test
				if(gc.shildLevel >= 2){
					gc.shildLevel --;		//shild削る
					gc.isShildBom = true;	//shildBom発動
					Debug.Log("shildLevel : " + gc.shildLevel);
				}
		}
	}
 }
