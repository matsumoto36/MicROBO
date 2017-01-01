using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// テスト用武器その2
/// </summary>
public class RangedWeaponNormal2 : RengedWeaponBase {

	[SerializeField]
	UnitShot shotPre;               //出す弾

	public override void Start() {
		base.Start();

		//初期設定
		moduleName = "NormalWeapon2";
		_shotPre = shotPre;
		damageMagnification = 4.0f;
		isAuto = false;
		waitTime = 0.5f;

		set = new NWayShotSystem.ShotSet(
_shotPre.gameObject, 3, 20, true);

		shotSpeed = 5;

		//装備時の動作
		attach = (UnitBase unit) => {
			unit.power = (int)(unit.power * damageMagnification);
		};
	}
}
