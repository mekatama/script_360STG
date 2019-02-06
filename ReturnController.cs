using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnController : MonoBehaviour {

	//戻るボタン用の制御関数
	public void ButtonClicked_Title(){
		SceneManager.LoadScene("title");	//シーンのロード
	}
}
