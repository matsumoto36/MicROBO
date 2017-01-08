using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// テスト用武器その2
/// </summary>
public class RangedWeaponNormal2 : WeaponBase {

	[SerializeField]
	UnitShot shotPre;				//出す弾

	[SerializeField]
	UnitMelee meleePre;				//出す武器

	public override void Awake() {
		base.Awake();

		Debug.Log("WeaponSet");

		//初期設定
		moduleName = "NormalWeapon2";
		damageMagnification = 4.0f;
		isAuto = true;
		waitTime = 0.3f;

		weaponModuleList = new List<IWeaponModule>();

		////遠距離攻撃用モジュール設定
		//RengedWeaponModule rangeModule = new RengedWeaponModule();
		//rangeModule.shotPre = shotPre;
		//rangeModule.set = new NWayShotSystem.ShotSet(
		//	rangeModule.shotPre.gameObject, 3, 20, true);
		//rangeModule.shotSpeed = 6;
		////遠距離攻撃用モジュール追加
		//weaponModuleList.Add(rangeModule);

		//近接攻撃用モジュール設定
		MeleeWeaponModule meleeModule = new MeleeWeaponModule();
		meleeModule.meleePre = meleePre;
		meleeModule.meleeDuration = 0.5f;
		meleeModule.meleeDir = 179.0f;
		//近接攻撃用モジュール追加
		weaponModuleList.Add(meleeModule);

		//装備時の動作
		attach = (UnitBase unit) => {
			unit.power = (int)(unit.power * damageMagnification);
		};
	}


	public override void Start() {
		base.Start();
	}
}
