using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {

	void Update(){
		//backキー
		if (Input.GetKeyUp(KeyCode.Escape)){
			Application.Quit();	//アプリ終了
		}
	}

	//セレクト画面用の制御関数
	public void ButtonClicked_Start(){
		SceneManager.LoadScene("stage1");	//シーンのロード
	}

	//遊び方ボタン用の制御関数
	public void ButtonClicked_HowToPlay(){
		SceneManager.LoadScene("HowToPlay");	//シーンのロード
	}

	//アプリ終了
	public void ButtonClicked_Exit(){
		Application.Quit();
		Debug.Log("exit");	
	}

	//Debug用ハイスコアリセットボタン
	public void ButtonClicked_Reset(){
		PlayerPrefs.DeleteAll();
		Debug.Log("全データ削除しますた");	
	}
}
