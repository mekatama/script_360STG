using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIstage_shildLevel : MonoBehaviour {
	public GameObject gameController;	//GameController取得
	public Text shildLevelText;			//Textコンポーネント取得用
	public Image batuImage;				//

	void Start () {
		gameController = GameObject.FindWithTag ("GameController");	//GameControllerオブジェクトを探す
	}

	void Update () {
		//gcって仮の変数にGameControllerのコンポーネントを入れる
		GameController gc = gameController.GetComponent<GameController>();
		//powerLevel表示
		if(gc.shildLevel == 4){
			//level maxの時
			shildLevelText.text = "MAX";
		}else{
			shildLevelText.text = gc.shildLevel.ToString("+"+ "0");
		}

		//アイコン使用不可演出
		if(gc.isShildButton == false){
			batuImage.enabled = true;	//表示
		}else{
			batuImage.enabled = false;	//非表示
		}
	}
}
