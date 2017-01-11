using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ModuleAction();
public delegate void ModuleAttach(UnitBase unit);

public enum ModuleType {
	Weapon,
	Armor,
}

/// <summary>
/// 様々な装備品の基底クラス
/// </summary>
public abstract class ModuleBase : MonoBehaviour {

	protected ModuleAction action;		//ユニットが使用したときの処理
	protected ModuleAttach attach;		//ユニットに装備したときの処理

	//システム
	[SerializeField]
	protected Sprite moduleIcon;		//装備の見た目
	[SerializeField]
	protected Sprite itemIcon;			//アイテムのアイコン

	public string moduleName {			//装備品の名前
		get; protected set;
	}
	public UnitBase owner {				//装備の所有者
		get; private set;
	}
	public ModuleType moduleType {		//装備のタイプ
		get; protected set;
	}
	float _waitTime;					//待ち時間計算用

	//パラメータ
	uint nextLevelEXP;					//レベルアップに必要な経験値
	uint experience;					//現在の経験値
	int level;							//装備のレベル
	protected float _param_growth;		//成長度によるパラメータ補正値(割合)
	protected float growthDifficult;	//成長のしやすさ
	protected float growthScale;		//成長量の補正値

	public bool isAuto {				//連続実行可能か
		get; protected set;
	}

	public float waitTime {				//待ち時間
		get; protected set;
	}

	public virtual void Start() {
		_waitTime = 0;
		nextLevelEXP = GameBalance.INITNEXTLEVELEXP;
	}

	public virtual void Update() {

		_waitTime += Time.deltaTime;
	}
	
	/// <summary>
	/// 装備品の行動
	/// </summary>
	public void Action() {

		if(waitTime < _waitTime) {
			if(action != null) action();
			_waitTime = 0;
		}
	}

	/// <summary>
	/// 対象ユニットに自分を装備させる
	/// </summary>
	/// <param name="unit">対象のユニット</param>
	public void Attach(UnitBase unit) {
		//所有者を指定
		owner = unit;
		//装備時のメソッドを実行
		if(attach == null) {
			Start();
		}

		attach(unit);
	}

	/// <summary>
	/// 経験値を取得する
	/// </summary>
	/// <param name="exp">取得経験値</param>
	public void GainEXP(int exp) {
		
		//経験値取得
		experience += (uint)exp;
		//連続でレベルアップする可能性があるのでチェック
		while(experience >= nextLevelEXP) {
			//レベルアップ処理
			experience -= nextLevelEXP;
			level++;
			nextLevelEXP = GameBalance.GetNextLevelEXPFromEXP(nextLevelEXP, 1);

			Debug.Log("Module LevelUp! Level:" + level + "NextEXP:" + nextLevelEXP);
		}
		
	}

	/// <summary>
	/// 装備状態からアイテム状態へ
	/// </summary>
	public void Drop() {

		//アイテム化
		GameObject itemPre = Resources.Load<GameObject>("Item/ItemModule");
		GameObject item = Instantiate(itemPre, transform.position, Quaternion.identity);
		item.name = string.Format("[Item] {0}", moduleName);
		item.GetComponent<ItemModule>().SetModule(this , itemIcon);
		transform.parent = item.transform;
	}

}