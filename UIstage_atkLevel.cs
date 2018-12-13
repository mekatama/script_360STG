using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIstage_atkLevel : MonoBehaviour {
	public GameObject gameController;	//GameController取得
	public Text atkLevelText;			//Textコンポーネント取得用
	public Image batuImage;				//

	void Start () {
		gameController = GameObject.FindWithTag ("GameController");	//GameControllerオブジェクトを探す
	}

	void Update () {
		//gcって仮の変数にGameControllerのコンポーネントを入れる
		GameController gc = gameController.GetComponent<GameController>();
		//powerLevel表示
		atkLevelText.text = gc.attackPower.ToString("+"+ "0");

		//アイコン使用不可演出
		if(gc.isAtkButton == false){
			batuImage.enabled = true;	//表示
		}else{
			batuImage.enabled = false;	//非表示
		}
	}
}
