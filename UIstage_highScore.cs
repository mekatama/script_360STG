using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIstage_highScore : MonoBehaviour {
	private int high_Score;				//temp
	public Text scoreText;				//Textコンポーネント取得用

	void Start(){
		//HighScoreがなかったら０を入れて初期化
		high_Score = PlayerPrefs.GetInt("HighScore", 0); 
	}

	void Update () {
		//highscore表示
		scoreText.text = "HighScore : " + high_Score.ToString("000000" + "p");
	}
}
