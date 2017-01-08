using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器の機能:近接攻撃を追加する
/// </summary>
public class MeleeWeaponModule : MonoBehaviour, IWeaponModule {

	public UnitMelee meleePre { get; set; }		//攻撃本体
	public float meleeDuration { get; set; }	//攻撃の速度
	public float meleeDir { get; set; }			//攻撃する角度

	/// <summary>
	/// IWeaponModuleのメソッド
	/// </summary>
	/// <param name="owner">攻撃するキャラクタ</param>
	public void Attack(UnitBase owner) {
		Debug.Log("MeleeAttack");

		Vector3 meleeVec = Vector3.zero;
		//キャラクタによって向きの取得方法を変える
		switch(owner.unitType) {
			case UnitType.Player:
				//マウス取得
				Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				meleeVec = (mousePosition - owner.transform.position).normalized;
				break;
			case UnitType.Enemy:
				//キャストして持ってくる
				UnitEnemy e = (UnitEnemy)owner;
				meleeVec = e.attackAngle.normalized;
				break;

		}

		//回転を始める角度を計算
		float rotZ = Mathf.Atan2(meleeVec.y, meleeVec.x) * Mathf.Rad2Deg;
		Debug.Log("rot:" + rotZ);
		Quaternion startRot = 
			Quaternion.Euler(new Vector3(0, 0, rotZ - meleeDir / 2));

		//生成
		UnitMelee m = Instantiate(meleePre, owner.transform.position, startRot);
		m.transform.parent = owner.transform;
		m.owner = owner;
		m.meleeDuration = meleeDuration;
		m.meleeDir = meleeDir;
	}
}
