using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponModule : IWeaponModule{

	/// <summary>
	/// IWeaponModuleのメソッド
	/// </summary>
	/// <param name="owner">攻撃するキャラクタ</param>
	public void Attack(UnitBase owner) {
		Debug.Log("MeleeAttack");
	}
}
