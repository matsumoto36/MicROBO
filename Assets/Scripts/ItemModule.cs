using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// フィールドに落ちている状態のモジュールクラス
/// </summary>
public class ItemModule : MonoBehaviour {

	ModuleBase contentModule;	//中身の装備品
	SpriteRenderer render;		//見た目

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
		
		if(other.tag == "Unit") {
			//アイテムから装備を取り出す
			other.GetComponent<UnitBase>().EquipModule(PickItem());
			Destroy(gameObject);
		}
	}

	/// <summary>
	/// アイテムから装備品に変換
	/// </summary>
	/// <returns>装備品本体のオブジェクト</returns>
	ModuleBase PickItem() {
		return contentModule;
	}
}