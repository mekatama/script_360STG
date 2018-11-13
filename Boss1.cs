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
					Instantiate (particle, transform.position, transform.rotation);
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
