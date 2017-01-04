using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器の機能:遠距離攻撃を追加する。
/// </summary>
public class RengedWeaponModule : IWeaponModule {

	public float shotSpeed {
		get; set;
	}
	public UnitShot _shotPre {
		get; set;
	}
	public NWayShotSystem.ShotSet set {	//nway弾
		get; set;
	}
	public int penetration {			//貫通回数
		get; set;
	}

	/// <summary>
	/// IWeaponModuleのメソッド
	/// </summary>
	/// <param name="owner">攻撃するキャラクタ</param>
	public void Attack(UnitBase owner) {
		Debug.Log("RangedAttack");

		//マウス取得
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 shotVec = (mousePosition - owner.transform.position).normalized;

		//撃つ
		NWayShotSystem.ShotResult result = NWayShotSystem.Shot(set, owner.transform.position, shotVec);

		List<GameObject> list = result.GetShotList();
		for(int i = 0;i < list.Count;i++) {
			UnitShot shot = list[i].GetComponent<UnitShot>();
			//所有者を設定
			shot.owner = owner;
			//スピードを設定
			shot.shotSpeed = shotSpeed;
		}
	}
}
