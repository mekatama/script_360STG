 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour {
	GameObject gameController;			//検索したオブジェクト入れる用
	public int bossHp;					//EnemyのHP
	public int boss_score;				//enemyの点数
	public float bossMoveSpeed = 0.0f;	//1秒間にenemyが進む距離
	public GameObject item1;			//Enemyから出現させるアイテム
	GameObject targetPos;
	private Vector3 vecAddPos;
	private bool isHitStop;
	public float hitStoptime = 0.2f;	//HitStop間隔
	private float timeElapsed = 0.0f;	//HitStopカウント用
	private bool isDeth;				//死亡flag
	public GameObject particle;			//爆発Particle
	private float x_pos;				//point出現位置random
	private float z_pos;				//point出現位置random
	private float tmpPos;				//random値
	public int spawnCoin;				//coin出現数

	public GameObject bulletObject = null;	//boss弾プレハブ
	public float timeOut = 0.4f;			//boss弾の連射間隔
	private float timeElapsedAttack = 0.0f;	//boss弾の連射間隔カウント用
	public int bossAttackType;				//boss攻撃パターン
	private float radius;					//boss攻撃半径
	private float angle; 					//boss攻撃角度

	void Start () {
		gameController = GameObject.FindWithTag ("GameController");	//GameControllerオブジェクトを探す
		targetPos = GameObject.FindWithTag ("Target");				//
		isHitStop = false;
		isDeth = false;
	}
	
	void Update () {
		Vector3 target = targetPos.transform.position; // キャラクタ位置
		transform.LookAt(target);
		//1秒間の移動量
		vecAddPos = (Vector3.forward * bossMoveSpeed);
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
		BossShot();
	}

	//Boss攻撃
	void BossShot(){
		//gcって仮の変数にGameControllerのコンポーネントを入れる
		GameController gc = gameController.GetComponent<GameController>();
		//boss弾の自動連射
		timeElapsedAttack += Time.deltaTime;
        if(timeElapsedAttack >= timeOut) {
			switch(bossAttackType){
				//弾を生成する位置をbossAttackTypeで変化させる
				case 0:
					//固定方向type
					radius =8.0f;
					angle = 90.0f; 
					break;
				case 1:
					//全方位ランダムtype
					radius =9.0f;
					angle = Random.Range(0.0f,359.0f); //ランダムで角度を決める
					break;
				case 2:
					//順番方位type
					radius =9.0f;
					angle += 10.0f;
					if(angle >= 360.0f){
						angle = 0.0f;
					}
					break;
				case 3:
					break;
			}
			//円周上の座標
			float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
			float z = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
			//弾を生成する位置を指定する
			Vector3 vecBulletPos	= new Vector3 (x, 0.6f, z);
			//弾を生成する
			Instantiate( bulletObject, vecBulletPos, transform.rotation);
			timeElapsedAttack = 0.0f;
//			Debug.Log("boss attack");
			gc.isShildBom = false;		//ShildBom flag初期化
		}
	}

	//他のオブジェクトとの当たり判定
	void OnTriggerEnter( Collider other) {
		//gcって仮の変数にGameControllerのコンポーネントを入れる
		GameController gc = gameController.GetComponent<GameController>();
		if(other.tag == "Bullet1" || other.tag == "Bullet2" || other.tag == "Bullet3"){
			if(bossHp > 0){
				bossHp = bossHp - gc.attackPower;	//攻撃力をHPから引く
				isHitStop = true;					//on
			}
			//死亡判定
			if(bossHp == 0){
				if(isDeth == false){
					//スコア加算
					gc.isBossGo = false;
					Destroy(gameObject);	//このGameObjectを［Hierrchy］ビューから削除する
					gc.total_Score += boss_score;
					gc.killBossNum += 1;	//enemy撃破数
					//爆発effect
					for(int i = 5; i > 0; --i){
						tmpPos = 2.0f;
						x_pos = Random.Range(-tmpPos,tmpPos); //ランダムで出現位置を決める
						z_pos = Random.Range(-tmpPos,tmpPos); //ランダムで出現位置を決める
						Instantiate (	particle, 
										new Vector3(transform.position.x + x_pos, transform.position.y, transform.position.z + z_pos),
										transform.rotation);
					}
					//アイテムを落とす
					for(int i = spawnCoin; i > 0; --i){
						tmpPos = 1.0f;
						x_pos = Random.Range(-tmpPos,tmpPos); //ランダムで出現位置を決める
						z_pos = Random.Range(-tmpPos,tmpPos); //ランダムで出現位置を決める
						Instantiate (	item1, 
										new Vector3(transform.position.x + x_pos, transform.position.y, transform.position.z + z_pos),
										transform.rotation);
					}
					isDeth = true;
				}
			}
		}
	}
}
