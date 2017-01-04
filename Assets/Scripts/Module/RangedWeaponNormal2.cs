using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// テスト用武器その2
/// </summary>
public class RangedWeaponNormal2 : WeaponBase {

	[SerializeField]
	UnitShot shotPre;               //出す弾


	public override void Awake() {
		base.Awake();

		Debug.Log("WeaponSet");

		//初期設定
		moduleName = "NormalWeapon2";
		damageMagnification = 4.0f;
		isAuto = true;
		waitTime = 0.3f;

		//遠距離攻撃用モジュール設定
		RengedWeaponModule rangeModule = new RengedWeaponModule();
		rangeModule._shotPre = shotPre;
		rangeModule.set = new NWayShotSystem.ShotSet(
			rangeModule._shotPre.gameObject, 3, 20, true);
		rangeModule.shotSpeed = 6;
		//遠距離攻撃用モジュール追加
		weaponModuleList.Add(rangeModule);

		//装備時の動作
		attach = (UnitBase unit) => {
			unit.power = (int)(unit.power * damageMagnification);
		};
	}


	public override void Start() {
		base.Start();
	}
}
