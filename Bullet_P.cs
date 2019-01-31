using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_P : MonoBehaviour {
	public float bulletMoveSpeed = 10.0f;	//1秒間に弾が進む距離
	public GameObject explosion;			//爆発プレハブを入れる用
	public GameObject explosionEnemy;		//爆発プレハブを入れる用
	public AudioClip audioClipGuard;		//Guard SE

	void Start () {
		
	}
	
	void Update () {
		//1秒間の移動量
		Vector3 vecAddPos = (Vector3.forward * bulletMoveSpeed);
		//移動量、回転量には Time.deltaTime をかけて実行環境(フレーム数の差)による違いが出ないようにします
		transform.position += ((transform.rotation * vecAddPos) * Time.deltaTime);
	}

	//他のオブジェクトとの当たり判定
	void OnTriggerEnter( Collider other) {
		if(other.tag == "Wall"){
			Destroy( gameObject);	//このGameObjectを［Hierrchy］ビューから削除する
		}
		if(other.tag == "Enemy"){
			Destroy( gameObject);	//このGameObjectを［Hierrchy］ビューから削除する
			Instantiate (explosion, transform.position, transform.rotation);
		}
		if(other.tag == "Shild_Enemy"){
			Destroy( gameObject);	//このGameObjectを［Hierrchy］ビューから削除する
			Instantiate (explosionEnemy, transform.position, transform.rotation);
			//SEをその場で鳴らす
			AudioSource.PlayClipAtPoint( audioClipGuard, transform.position);	//SE再生(Destroy対策用)
		}
	}
}
