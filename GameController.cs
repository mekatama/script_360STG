using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public int total_Score;		//score
	public int attackPower;		//攻撃力
	public int total_ItemNum;	//item数
	public int killEnemyNum;	//撃墜enemy数
	public bool isMuteki;		//無敵flag
	public bool isGameOver;		//無敵flag
	public int enemyType;		//enemyの種類数
	public int shotLevel;		//shotのレベル
	public float editEnemySpawn;//spawn時間変更制御用数値

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
		//撃破数でenemyの種類を制御。しきい値は後で制御
		if(killEnemyNum < 10){
			enemyType = 0;
		}else{
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
}
