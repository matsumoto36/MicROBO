using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラクタから出る近接武器本体
/// </summary>
public class UnitMelee : MonoBehaviour, IUnitAttack {

	//プロパティ
	public UnitBase owner { get; set; }			//攻撃したキャラクタ
	public float meleeDuration { get; set; }	//攻撃時間
	public float meleeDir { get; set; }			//攻撃する角度

	//回転角度関係
	Quaternion startRot;						//最初の角度
	Quaternion endRot;							//終了角度

	//時間関係
	float _time = 0;							//経過時間

	void Start() {
		//角度を保存
		startRot = transform.rotation;
		endRot = startRot * Quaternion.AngleAxis(meleeDir, Vector3.forward);
	}

	// Update is called once per frame
	void Update () {

		_time += Time.deltaTime;

		//指定時間まで回転し続ける
		transform.rotation = Quaternion.Lerp(startRot, endRot, _time / meleeDuration);
		if(_time > meleeDuration) {
			Destroy(gameObject);
		}
	}
}
