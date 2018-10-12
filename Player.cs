using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	GameObject gameController;			//検索したオブジェクト入れる用
	private float rayRange = 1000f;			//レイを飛ばす距離
	private Vector3 targetPosition;			//移動する位置

	public GameObject bulletObject = null;			//弾プレハブ
	public Transform bulletStartPosition = null;	//弾の発射位置を取得する
	public float timeOut = 0.4f;					//弾の連射間隔
	private float timeElapsed = 0.0f;				//弾の連射間隔カウント用

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

	//他のオブジェクトとの当たり判定
	void OnTriggerEnter( Collider other) {
		//gcって仮の変数にGameControllerのコンポーネントを入れる
		GameController gc = gameController.GetComponent<GameController>();
		if(other.tag == "Enemy"){
			if(!gc.isMuteki){
				gc.isGameOver = true;
				Destroy(gameObject);	//このGameObjectを［Hierrchy］ビューから削除する
			}
		}
	}
 }
