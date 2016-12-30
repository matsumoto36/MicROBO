using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// テスト用武器
/// </summary>
public class RengedWeaponNormal : RengedWeaponBase {

	[SerializeField]
	UnitShot shotPre;

	public override void Start() {
		base.Start();

		//初期設定
		moduleName = "DebugModule";
		_shotPre = shotPre;
		damageMagnification = 2.0f;
		isAuto = false;
		waitTime = 0.5f;

		set = new NWayShotSystem.ShotSet(
_shotPre.gameObject, 1, 90, true);

		shotSpeed = 5;

		//装備時の動作
		attach = (UnitBase unit) => {
			unit.power = (int)(unit.power * damageMagnification);
		};
	}

}
