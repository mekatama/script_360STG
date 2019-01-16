using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shot : MonoBehaviour {
	GameObject gameController;						//検索したオブジェクト入れる用
	public GameObject bulletObject = null;			//弾プレハブ
	public Transform bulletStartPosition = null;	//弾の発射位置を取得する
	private float timeElapsed = 0.0f;				//弾の連射間隔カウント用
	public float timeOut;							//連射間隔の時間
	public AudioClip audioClipShot;					//発射 SE

	void Start () {
		gameController = GameObject.FindWithTag ("GameController");	//GameControllerオブジェクトを探す
		this.transform.localScale = new Vector3(0, 0, 0);	//初期は非表示
	}

	void Update () {
		//gcって仮の変数にGameControllerのコンポーネントを入れる
		GameController gc = gameController.GetComponent<GameController>();
		if(gc.shotLevel == 1){
			//アタッチしたオブジェクト名で分岐
			if(this.gameObject.name == "ShotPoint1"){
				this.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);	//表示
				Shot();
			}
		}else if(gc.shotLevel == 2){
			//アタッチしたオブジェクト名で分岐
			if(this.gameObject.name == "ShotPoint2" || this.gameObject.name == "ShotPoint3"){
				this.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);	//表示
				Shot();
			}
			if(this.gameObject.name == "ShotPoint1"){
				this.transform.localScale = new Vector3(0, 0, 0);	//非表示
			}
		}else if(gc.shotLevel == 3){
			//アタッチしたオブジェクト名で分岐
			if(this.gameObject.name == "ShotPoint4" || this.gameObject.name == "ShotPoint5" || this.gameObject.name == "ShotPoint1"){
				this.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);	//表示
				Shot();
			}
			if(this.gameObject.name == "ShotPoint2" || this.gameObject.name == "ShotPoint3"){
				this.transform.localScale = new Vector3(0, 0, 0);	//非表示
			}
		}
	}

	void Shot(){
		//gcって仮の変数にGameControllerのコンポーネントを入れる
		GameController gc = gameController.GetComponent<GameController>();
		//連射速度の判定
		switch(gc.rappedLevel){
			case 1:
				timeOut = 0.4f;
				break;
			case 2:
				timeOut = 0.3f;
				break;
			case 3:
				timeOut = 0.2f;
				break;
		}
		//弾の自動連射
		timeElapsed += Time.deltaTime;
		if(timeElapsed >= timeOut) {
			//SEをその場で鳴らす
			AudioSource.PlayClipAtPoint( audioClipShot, transform.position);	//SE再生(Destroy対策用)
			//弾を生成する位置を指定する
			Vector3 vecBulletPos	= bulletStartPosition.position;
			//弾を生成する
			Instantiate( bulletObject, vecBulletPos, transform.rotation);
			timeElapsed = 0.0f;
		}
	}
}
