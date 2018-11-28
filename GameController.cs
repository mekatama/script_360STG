using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	public int rappedLevel;	//player弾の連射間隔レベル
	public float editEnemySpawn;//spawn時間変更制御用数値
	public bool isBossGo;		//boss出現flag
	public int bossType;		//bossの種類数
	public bool isShild;		//shildのflag
	private bool isBossOnce;	//一回だけ処理
	
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
		isGameOver = false;	//初期化
		isBossOnce = false;	//初期化
		isShild = false;	//初期化
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
			enemyType = Random.Range(0,2);
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
		switch(shotLevel){
			case 1:
				if(total_ItemNum >= 5){
					shotLevel ++;
					total_ItemNum = total_ItemNum - 5;
					Debug.Log("shotLevel : " + shotLevel);
				}
				break;
		}
	}
	//item用のbutton2制御関数
	public void ButtonOn_Item2(){
		switch(attackPower){
			case 1:
				if(total_ItemNum >= 5){
					attackPower ++;
					total_ItemNum = total_ItemNum - 5;
					Debug.Log("attackPower : " + attackPower);
				}
				break;
		}
	}
	//item用のbutton3制御関数
	public void ButtonOn_Item3(){
		switch(rappedLevel){
			case 1:
				if(total_ItemNum >= 5){
					rappedLevel ++;
					total_ItemNum = total_ItemNum - 5;
					Debug.Log("rappedLevel : " + rappedLevel);
				}
				break;
		}
	}
	//item用のbutton4制御関数
	public void ButtonOn_Item4(){
		switch(attackPower){
			case 1:
				if(total_ItemNum >= 5){
					attackPower ++;
					total_ItemNum = total_ItemNum - 5;
					Debug.Log("attackPower : " + attackPower);
				}
				break;
		}
	}
}
