using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// キャラクタの表示用UIのオブジェクトを格納する
/// </summary>
public class UnitInfoContiner : MonoBehaviour {

	#region 共通の項目
	
	public RectTransform UIBody;	//UI本体
	public Vector3[] UIPos;			//UIの表示位置 [0]:優先度が高い [1]:その次
	public Vector2[] UIAnchorMin;	//UIのアンカー(Min) [0]:優先度が高い [1]:その次
	public Vector2[] UIAnchorMax;	//UIのアンカー(Max) [0]:優先度が高い [1]:その次
	public Image[] UIPanels;		//UIのパネル

	public Text texUnitName;		//キャラクタの名前
	public Text texUnitMaxHp;		//キャラクタの最大体力
	public Text texUnitNowHp;		//キャラクタの今の体力
	public Image unitImage;			//キャラクタの画像
	public Image unitLifeBar;		//キャラクタのライフバー

	#endregion

	#region 詳細表示のみ

	public Text texUnitPower;		//キャラクタの今の攻撃力
	public Text texUnitDefence;		//キャラクタの今の防御力
	public Text texUnitSpeed;		//キャラクタの今の移動速度
	public Text texUnitLuck;		//キャラクタの今の運
	public Text texUnitLevel;		//キャラクタの今のレベル
	public Image unitExpBar;		//キャラクタの経験値バー
	public Text unitEquipWeapon;	//キャラクタの装備中の武器
	public Text unitEquipArmor;		//キャラクタの装備中の装備
	public Text unitEquipArmor2;	//キャラクタの装備中の装備その２

	#endregion

	// Use this for initialization
	void Start () {
		
	}
}
