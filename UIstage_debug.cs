using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIstage_debug : MonoBehaviour {
	public GameObject gameController;	//GameController取得
	public Text powerText;				//Textコンポーネント取得用
	public Text levelText;				//Textコンポーネント取得用

	void Update () {
		//gcって仮の変数にGameControllerのコンポーネントを入れる
		GameController gc = gameController.GetComponent<GameController>();
		//debug表示
		powerText.text = gc.attackPower.ToString("debug attackPower : "+ "00");
		levelText.text = gc.shotLevel.ToString("debug shotLevel : " + "00");
	}
}
