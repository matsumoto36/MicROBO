using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RengedWeaponBase : WeaponBase {

	protected float shotSpeed;

	protected UnitShot _shotPre;
	protected NWayShotSystem.ShotSet set;

	// Use this for initialization
	public override void Start() {
		base.Start();

	}

	public override void Attack() {

		//マウス取得
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 shotVec = (mousePosition - transform.position).normalized;

		//撃つ
		NWayShotSystem.ShotResult result = NWayShotSystem.Shot(set, transform.position, shotVec);

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
