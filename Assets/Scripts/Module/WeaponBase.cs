using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 様々な武器の基底クラス
/// </summary>
public abstract class WeaponBase: ModuleBase {

	protected float damageMagnification;	//ダメージ倍率

	// Use this for initialization
	public override void Start () {
		base.Start();
		//武器の行動は攻撃すること
		action = Attack;
	}

	public abstract void Attack();
}
