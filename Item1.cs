using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1 : MonoBehaviour {
	GameObject gameController;			//検索したオブジェクト入れる用
	public float itemMoveSpeed = 5.0f;	//1秒間にenemyが進む距離
	private Vector3 vecAddPos;
	GameObject targetPos;
	private bool isStop;
	public float stopTime = 0.2f;		//stop間隔
	private float timeElapsed = 0.0f;	//Stopカウント用
	public AudioClip audioClipGet;		//Item Get SE

	void Start () {
		gameController = GameObject.FindWithTag ("GameController");	//GameControllerオブジェクトを探す
		targetPos = GameObject.FindWithTag ("Target");	//
		isStop = true;
	}
	
	void Update () {
		Vector3 target = targetPos.transform.position; // キャラクタ位置
		transform.LookAt(target);
		//1秒間の移動量
		vecAddPos = (Vector3.forward * itemMoveSpeed);
		//移動
		if(isStop == false){
			//移動量、回転量には Time.deltaTime をかけて実行環境(フレーム数の差)による違いが出ないようにします
			transform.position += ((transform.rotation * vecAddPos) * Time.deltaTime);
		}else{
			//HitStop中はtransform.position処理に行かない
			timeElapsed += Time.deltaTime;
			if(timeElapsed >= stopTime) {
				timeElapsed = 0.0f;
				isStop = false;
			}
		}
	}

	//他のオブジェクトとの当たり判定
	void OnTriggerEnter( Collider other) {
		//gcって仮の変数にGameControllerのコンポーネントを入れる
		GameController gc = gameController.GetComponent<GameController>();
		if(other.tag == "Player"){
			//SEをその場で鳴らす
			AudioSource.PlayClipAtPoint( audioClipGet, transform.position);	//SE再生(Destroy対策用)
			gc.total_ItemNum += 1;	//個数加算
			Destroy( gameObject);	//このGameObjectを［Hierrchy］ビューから削除する
		}
	}
}
