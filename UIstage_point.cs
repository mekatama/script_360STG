using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIstage_point : MonoBehaviour {
	public GameObject gameController;	//GameController取得
	public Text pointText;				//Textコンポーネント取得用

	void Update () {
		//gcって仮の変数にGameControllerのコンポーネントを入れる
		GameController gc = gameController.GetComponent<GameController>();
		//score表示
		pointText.text = "Coin : " + gc.total_ItemNum.ToString("000000" + "p");
	}
}
