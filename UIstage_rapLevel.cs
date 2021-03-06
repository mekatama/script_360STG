﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIstage_rapLevel : MonoBehaviour {
	public GameObject gameController;	//GameController取得
	public Text rapLevelText;			//Textコンポーネント取得用
	public Image batuImage;				//

	void Start () {
		gameController = GameObject.FindWithTag ("GameController");	//GameControllerオブジェクトを探す
	}

	void Update () {
		//gcって仮の変数にGameControllerのコンポーネントを入れる
		GameController gc = gameController.GetComponent<GameController>();
		//powerLevel表示
		rapLevelText.text = gc.rappedLevel.ToString("+"+ "0");

		//アイコン使用不可演出
		if(gc.isRappedButton == false){
			batuImage.enabled = true;	//表示
		}else{
			batuImage.enabled = false;	//非表示
		}
	}
}
