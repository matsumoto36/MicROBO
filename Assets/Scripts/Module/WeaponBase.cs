using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 様々な武器の基底クラス
/// </summary>
public abstract class WeaponBase: ModuleBase {

	//共通ステータス
	protected float damageMagnification;	//ダメージ倍率

	//武器の効果リスト
	protected List<IWeaponModule> weaponModuleList;

	public virtual void Awake() {
		weaponModuleList = new List<IWeaponModule>();
	}

	// Use this for initialization
	public override void Start () {
		base.Start();

		//タイプの設定
		moduleType = ModuleType.Weapon;
		//装備効果 = 武器の攻撃
		action = Attack;
	}

	/// <summary>
	/// 武器で攻撃したときの効果
	/// </summary>
	public void Attack() {
		
		foreach(IWeaponModule weaponModule in weaponModuleList) {
			weaponModule.Attack(owner);
		}
	}

}
