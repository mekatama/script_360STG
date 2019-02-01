using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public int total_Score;		//score
	public int attackPower;		//攻撃力
	public int total_ItemNum;	//item数
	public int spawnEnemyNum;	//出現enemy数
	public int killEnemyNum;	//撃墜enemy数
	public int killBossNum;		//撃墜boss数
	public bool isMuteki;		//無敵flag
	public bool isGameOver;		//無敵flag
	public int enemyType;		//enemyの種類数
	public int shotLevel;		//shotのレベル
	public bool isShildBom;		//shildBom発動flag
	public int rappedLevel;		//player弾の連射間隔レベル
	public float editEnemySpawn;//spawn時間変更制御用数値
	public bool isBossGo;		//boss出現flag
	public int bossType;		//bossの種類数
	public int shildLevel;		//shildのlevel
	private bool isBossOnce;	//一回だけ処理
	public bool isWayButton;	//powerupボタンway制御用
	public int[] shotLevelItemNum;	//shotLevelのpowerupに必要な数
	public bool isAtkButton;		//powerupボタンatk制御用
	public int[] atkLevelItemNum;	//atkLevelのpowerupに必要な数
	public bool isRappedButton;		//powerupボタンrapped制御用
	public int[] rappedLevelItemNum;//rappedLevelのpowerupに必要な数
	public bool isShildButton;		//powerupボタンshild制御用
	public int[] shildLevelItemNum;	//shildLevelのpowerupに必要な数
	public AudioClip audioClipPowerup;	//Powerup SE

	public Text wayLevelUpText;			//Textコンポーネント取得用
	public Text attackLevelUpText;		//Textコンポーネント取得用
	public Text rappedLevelUpText;		//Textコンポーネント取得用
	public Text shildLevelUpText;		//Textコンポーネント取得用
	float PowerUpUITime = 3.0f;			//UIのPowerUpUIを表示する時間
	float time_PowerUp1 = 0f;			//UIのSTARTを表示する時間用の変数
	float time_PowerUp2 = 0f;			//UIのSTARTを表示する時間用の変数
	float time_PowerUp3 = 0f;			//UIのSTARTを表示する時間用の変数
	float time_PowerUp4 = 0f;			//UIのSTARTを表示する時間用の変数
	
	//ゲームステート
	enum State{
		GameStart,	//
		Play,		//
		Clear,		//
		Result,		//
		GameOver,	//
	}
	State state;

	void Start () {
		wayLevelUpText.enabled = false;		//UI非表示
		attackLevelUpText.enabled = false;	//UI非表示
		rappedLevelUpText.enabled = false;	//UI非表示
		shildLevelUpText.enabled = false;	//UI非表示
		time_PowerUp1 = 0f;					//UIを表示する時間用の変数の初期化
		time_PowerUp2 = 0f;					//UIを表示する時間用の変数の初期化
		time_PowerUp3 = 0f;					//UIを表示する時間用の変数の初期化
		time_PowerUp4 = 0f;					//UIを表示する時間用の変数の初期化
		isGameOver = false;	//初期化
		isBossOnce = false;	//初期化
		GameStart();		//初期ステート		
	}

	void LateUpdate () {
		//ステートの制御
		switch(state){
			case State.GameStart:
//				Debug.Log("game start");
				Play();		//ステート移動		
				break;
			//
			case State.Play:
				//PowerUpのUI表示制御
				if(wayLevelUpText.enabled == true){
					time_PowerUp1 += Time.deltaTime;
					if(time_PowerUp1 > PowerUpUITime){
						wayLevelUpText.enabled = false;		//UI非表示
						time_PowerUp1 = 0f;					//初期化
					}
				}
				if(attackLevelUpText.enabled == true){
					time_PowerUp2 += Time.deltaTime;
					if(time_PowerUp2 > PowerUpUITime){
						attackLevelUpText.enabled = false;		//UI非表示
						time_PowerUp2 = 0f;					//初期化
					}
				}
				if(rappedLevelUpText.enabled == true){
					time_PowerUp3 += Time.deltaTime;
					if(time_PowerUp3 > PowerUpUITime){
						rappedLevelUpText.enabled = false;		//UI非表示
						time_PowerUp3 = 0f;					//初期化
					}
				}
				if(shildLevelUpText.enabled == true){
					time_PowerUp4 += Time.deltaTime;
					if(time_PowerUp4 > PowerUpUITime){
						shildLevelUpText.enabled = false;		//UI非表示
						time_PowerUp4 = 0f;					//初期化
					}
				}

//				Debug.Log("play");
				//GameOver判定
				if(isGameOver){
					GameOver();
				}
				break;
			//
			case State.Clear:
				Debug.Log("clear");
				break;
			//
			case State.Result:
				Debug.Log("result");
				break;
			//
			case State.GameOver:
				Debug.Log("game over");
				break;
		}
	}

	void Update () {
		//spawn数でbossの出現と種類を制御。しきい値は後で制御
		if(spawnEnemyNum == 6 || spawnEnemyNum == 12){
			//ココに一回だけ処理
			if(!isBossOnce){
				isBossGo = true;	//bosss出現。撃破でoff
				bossType = Random.Range(0,1);
				isBossOnce = true;
			}
		}else{
			isBossOnce = false;
			enemyType = Random.Range(0,3);	//enemyの種類を増やしたら変更する
		}

		//powerupアイコン制御用
		switch(shotLevel){
			case 1:
				if(total_ItemNum < shotLevelItemNum[0])		isWayButton = false;	//使用不可
				if(total_ItemNum >= shotLevelItemNum[0])	isWayButton = true;		//使用可	
				break;
			case 2:
				if(total_ItemNum < shotLevelItemNum[1])		isWayButton = false;	//使用不可
				if(total_ItemNum >= shotLevelItemNum[1])	isWayButton = true;		//使用可	
				break;
			case 3:
				isWayButton = false;	//Max
				break;
		}

		switch(attackPower){
			case 1:
				if(total_ItemNum < atkLevelItemNum[0])	isAtkButton = false;	//使用不可
				if(total_ItemNum >= atkLevelItemNum[0])	isAtkButton = true;		//使用可	
				break;
			case 2:
				if(total_ItemNum < atkLevelItemNum[1])	isAtkButton = false;	//使用不可
				if(total_ItemNum >= atkLevelItemNum[1])	isAtkButton = true;		//使用可	
				break;
			case 3:
				isAtkButton = false;	//Max
				break;
		}

		switch(rappedLevel){
			case 1:
				if(total_ItemNum < rappedLevelItemNum[0])	isRappedButton = false;	//使用不可
				if(total_ItemNum >= rappedLevelItemNum[0])	isRappedButton = true;	//使用可	
				break;
			case 2:
				if(total_ItemNum < rappedLevelItemNum[1])	isRappedButton = false;	//使用不可
				if(total_ItemNum >= rappedLevelItemNum[1])	isRappedButton = true;	//使用可	
				break;
			case 3:
				isRappedButton = false;	//Max
				break;
		}

		switch(shildLevel){
			case 1:
				if(total_ItemNum < shildLevelItemNum[0])	isShildButton = false;	//使用不可
				if(total_ItemNum >= shildLevelItemNum[0])	isShildButton = true;		//使用可	
				break;
			case 2:
				if(total_ItemNum < shildLevelItemNum[1])	isShildButton = false;	//使用不可
				if(total_ItemNum >= shildLevelItemNum[1])	isShildButton = true;		//使用可	
				break;
			case 3:
				isShildButton = false;	//Max
				break;
		}
	}

	void GameStart(){
		state = State.GameStart;
	}
	void Play(){
		state = State.Play;
	}
	void Clear(){
		state = State.Clear;
	}
	void Result(){
		state = State.Result;
	}
	void GameOver(){
		state = State.GameOver;
	}

	//item用のbutton1制御関数
	public void ButtonOn_Item1(){
		if(isWayButton == true){
			wayLevelUpText.enabled = true;	//UI表示
			shotLevel ++;
			Debug.Log("shotLevel : " + shotLevel);
			//SEをその場で鳴らす
			AudioSource.PlayClipAtPoint( audioClipPowerup, transform.position);	//SE再生(Destroy対策用)
		}
	}

	//item用のbutton2制御関数
	public void ButtonOn_Item2(){
		if(isAtkButton == true){
			attackLevelUpText.enabled = true;	//UI表示
			attackPower ++;
			Debug.Log("attackPower : " + attackPower);
			//SEをその場で鳴らす
			AudioSource.PlayClipAtPoint( audioClipPowerup, transform.position);	//SE再生(Destroy対策用)
		}
	}
	//item用のbutton3制御関数
	public void ButtonOn_Item3(){
		if(isRappedButton == true){
			rappedLevelUpText.enabled = true;	//UI表示
			rappedLevel ++;
			Debug.Log("rappedLevel : " + rappedLevel);
			//SEをその場で鳴らす
			AudioSource.PlayClipAtPoint( audioClipPowerup, transform.position);	//SE再生(Destroy対策用)
		}
	}
	//item用のbutton4制御関数
	public void ButtonOn_Item4(){
		if(isShildButton == true){
			shildLevelUpText.enabled = true;	//UI表示
			shildLevel ++;
			Debug.Log("shildLevel : " + shildLevel);
			//SEをその場で鳴らす
			AudioSource.PlayClipAtPoint( audioClipPowerup, transform.position);	//SE再生(Destroy対策用)
		}
	}
}
