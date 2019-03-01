using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {
	public Text newRecordText;	//Textコンポーネント取得用
	private int isNewRecord;	//NewRecord flag用

	void Start () {
		//NewRecord flag用 0 = false
		isNewRecord = PlayerPrefs.GetInt("NewRecord");
		if(isNewRecord == 0){
			newRecordText.enabled = false;		//UI非表示
		}
	}

	void Update () {
	}
}
