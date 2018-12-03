using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shild : MonoBehaviour {
	public GameObject gameController;	//GameController取得
	public int localShildLevel;			//shildLevel判別用の値
	public float shildScale;			//shild size

	void Start () {
		this.transform.localScale = new Vector3(0, 0, 0);	//初期は非表示
	}
	
	void Update () {
		//gcって仮の変数にGameControllerのコンポーネントを入れる
		GameController gc = gameController.GetComponent<GameController>();
		//shildLevelでsize分岐
		if(gc.shildLevel == localShildLevel){
			this.transform.localScale = new Vector3(shildScale, shildScale, shildScale);
		}
		//enemy接触時
		if(gc.shildLevel == (localShildLevel -1)){
			this.transform.localScale = new Vector3(0, 0, 0);	//非表示
		}
	}
}
