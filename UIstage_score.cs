using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIstage_score : MonoBehaviour {
	public GameObject gameController;	//GameController取得
	public Text scoreText;				//Textコンポーネント取得用

	void Update () {
		//gcって仮の変数にGameControllerのコンポーネントを入れる
		GameController gc = gameController.GetComponent<GameController>();
		//score表示
		scoreText.text = "Score : " + gc.total_Score.ToString("000000" + "p");
	}
}
