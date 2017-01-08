using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// テスト用武器
/// </summary>
public class RengedWeaponNormal : WeaponBase {

	[SerializeField]
	UnitShot shotPre;               //出す弾

	public override void Awake() {
		base.Awake();

		Debug.Log("WeaponSet");

		//初期設定
		moduleName = "NormalWeapon";
		damageMagnification = 2.0f;
		isAuto = false;
		waitTime = 0.5f;

		//遠距離攻撃用モジュール設定
		RengedWeaponModule rangeModule = new RengedWeaponModule();
		rangeModule.shotPre = shotPre;
		rangeModule.set = new NWayShotSystem.ShotSet(
			rangeModule.shotPre.gameObject, 1, 90, true);
		rangeModule.shotSpeed = 5;
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
