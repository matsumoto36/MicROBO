using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラクタから出る発射物本体
/// </summary>
public class UnitShot : MonoBehaviour, IUnitAttack {

	public UnitBase owner { get; set; }		//攻撃したキャラクタ
	public float shotSpeed { get; set; }	//弾の速さ
	public int penetration { get; set; }	//貫通回数
	
	// Update is called once per frame
	void Update () {

		//移動
		transform.position += transform.right * shotSpeed * Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Unit") {
			UnitBase unit = other.GetComponent<UnitBase>();
			//敵にヒットしたか
			if(unit.unitType != owner.unitType) {
				//貫通回数を減らして判定
				if(--penetration < 0) {
					Destroy(gameObject);
				}
			}
		}
	}
}
