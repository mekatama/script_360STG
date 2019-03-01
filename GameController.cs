using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public int total_Score;		//score
	public int high_Score = 0;	//high score
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
	public bool isBossGo20;		//boss出現flag 20体毎
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
	public Text bossSpawnText;			//Textコンポーネント取得用
	float time_bossSpawn = 0f;			//UIのSTARTを表示する時間用の変数
//	public bool isHighScore;			//highscore更新制御用
	public int isNewRecord;				//highscore更新制御用intで代用
	
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
		bossSpawnText.enabled = false;		//UI非表示
		time_bossSpawn = 0f;				//UIを表示する時間用の変数の初期化
		isGameOver = false;	//初期化
		isBossOnce = false;	//初期化
		//HighScoreがなかったら０を入れて初期化
		high_Score = PlayerPrefs.GetInt("HighScore", 0); 
		//NewRecord flag用 0 = false
		PlayerPrefs.SetInt("NewRecord", 0);
		Debug.Log("isNewRecord" + isNewRecord);
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
				//BossSpawnのUI表示制御
				if(bossSpawnText.enabled == true){
					time_bossSpawn += Time.deltaTime;
					if(time_bossSpawn > PowerUpUITime){
						bossSpawnText.enabled = false;		//UI非表示
						time_bossSpawn = 0f;				//初期化
					}
				}
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
		if(!isGameOver){
			if((spawnEnemyNum % 20) == 0){
				isBossGo20 = true;
			}else{
				isBossGo20 = false;
			}

			//spawn数でbossの出現と種類を制御。しきい値は後で制御
//bossの出現パターンの設定
			if(spawnEnemyNum == 40){
				//ココに一回だけ処理
				if(!isBossOnce){
					isBossGo = true;				//bosss出現。撃破でoff
					bossType = 0;					//type select
					bossSpawnText.enabled = true;	//UI表示判定用
					isBossOnce = true;
				}
			}else if(spawnEnemyNum == 70){
				//ココに一回だけ処理
				if(!isBossOnce){
					isBossGo = true;				//bosss出現。撃破でoff
					bossType = 1;					//type select
					bossSpawnText.enabled = true;	//UI表示判定用
					isBossOnce = true;
				}
			}else if(spawnEnemyNum == 90){
				//ココに一回だけ処理
				if(!isBossOnce){
					isBossGo = true;				//bosss出現。撃破でoff
					bossType = 2;					//type select
					bossSpawnText.enabled = true;	//UI表示判定用
					isBossOnce = true;
				}
			}else if(spawnEnemyNum >= 90 && isBossGo20 == true){
				//ココに一回だけ処理
				if(!isBossOnce){
					isBossGo = true;				//bosss出現。撃破でoff
					bossType = Random.Range(0,3);	//type select
					bossSpawnText.enabled = true;	//UI表示判定用
					isBossOnce = true;
				}
			}else{
				isBossOnce = false;
//enemy出現パターンの設定
				if(spawnEnemyNum < 10){
					editEnemySpawn = 2.5f;	//enemyスポーン間隔
					enemyType = 0;			//enemyの種類を増やしたら変更する
				}else if(spawnEnemyNum >= 10 && spawnEnemyNum < 20){
					editEnemySpawn = 2.0f;	//enemyスポーン間隔
				}else if(spawnEnemyNum >= 20 && spawnEnemyNum < 30){
					editEnemySpawn = 1.5f;	//enemyスポーン間隔
				}else if(spawnEnemyNum >= 30 && spawnEnemyNum < 40){
					editEnemySpawn = 1.2f;	//enemyスポーン間隔
				}else if(spawnEnemyNum >= 40 && spawnEnemyNum < 50){
					editEnemySpawn = 2.0f;	//enemyスポーン間隔
					enemyType = Random.Range(0,2);	//enemyの種類 0 or 1
				}else if(spawnEnemyNum >= 50 && spawnEnemyNum < 60){
					editEnemySpawn = 1.5f;	//enemyスポーン間隔
					enemyType = Random.Range(0,2);	//enemyの種類 0 or 1
				}else if(spawnEnemyNum >= 60 && spawnEnemyNum < 70){
					editEnemySpawn = 1.2f;	//enemyスポーン間隔
					enemyType = Random.Range(0,2);	//enemyの種類 0 or 1
				}else if(spawnEnemyNum >= 70 && spawnEnemyNum < 80){
					editEnemySpawn = 2.0f;	//enemyスポーン間隔
					enemyType = Random.Range(0,3);	//enemyの種類 0 or 1 or 2
				}else if(spawnEnemyNum >= 80 && spawnEnemyNum < 90){
					editEnemySpawn = 1.5f;	//enemyスポーン間隔
					enemyType = Random.Range(0,3);	//enemyの種類 0 or 1 or 2
				}else if(spawnEnemyNum >= 90){
					editEnemySpawn = 1.2f;	//enemyスポーン間隔
					enemyType = Random.Range(0,3);	//enemyの種類 0 or 1 or 2
				}
			}
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
		//HighScore判定
		if(total_Score > high_Score){
			high_Score = total_Score;
			isNewRecord = 1;								//newrecord flag on
			PlayerPrefs.SetInt("HighScore", high_Score);	//save
			PlayerPrefs.SetInt("NewRecord", isNewRecord);	//save
			Debug.Log("HighScore=" + high_Score);
//			isHighScore = true;								//highscore更新flag on
		}
		SceneManager.LoadScene("gameover");	//シーンのロード
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
