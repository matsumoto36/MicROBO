using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// フィールドに落ちている状態のモジュールクラス
/// </summary>
public class ItemModule : MonoBehaviour {

	const float PICKUPWAIT = 1.0f; //アイテムが取得できるようになるまでの時間
	float _wait = 0;				//時間計算用

	ModuleBase contentModule;		//中身の装備品
	SpriteRenderer render;			//見た目
	
	void Update() {
		_wait += Time.deltaTime;
	}

	/// <summary>
	/// アイテムに必要な情報をセット
	/// </summary>
	/// <param name="moduleName">モジュールの名前</param>
	/// <param name="icon">アイテムとして表示される画像</param>
	public void SetModule(ModuleBase module, Sprite icon) {

		//装備品を紐づけ
		contentModule = module;
		Debug.Log(contentModule.name);
		//見た目を取得
		render = GetComponent<SpriteRenderer>();
		render.sprite = icon;
	}

	void OnTriggerEnter2D(Collider2D other) {

		//取得可能でなければ終了
		if(_wait < PICKUPWAIT) return;

		if(other.tag == "Unit") {
			//アイテムから装備を取り出す
			PickItem(other.GetComponent<UnitBase>());
			Destroy(gameObject);
		}
	}

	/// <summary>
	/// 装備品を取り出して装備させる
	/// </summary>
	/// <returns>装備品本体のオブジェクト</returns>
	void PickItem(UnitBase unit) {

		//指定したタイプ別に装備する
		switch(contentModule.moduleType) {
			case ModuleType.Armor:
				break;
			case ModuleType.Weapon:
				unit.EquipWeapon((WeaponBase)contentModule);
				break;
		}
	}
}