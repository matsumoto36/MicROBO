using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ModuleAction();
public delegate void ModuleAttach(UnitBase unit);

/// <summary>
/// 様々な装備品の基底クラス
/// </summary>
public abstract class ModuleBase : MonoBehaviour {

	protected ModuleAction action;		//ユニットが使用したときの処理
	protected ModuleAttach attach;		//ユニットに装備したときの処理

	//システム
	Sprite icon;						//表示用アイコン
	public UnitBase owner {					//装備の所有者
		get; private set;
	}
	float _waitTime;					//待ち時間計算用

	//パラメータ
	protected string moduleName;		//装備品の名前
	uint nextLevelEXP;					//レベルアップに必要な経験値
	uint experience;					//現在の経験値
	int level;							//装備のレベル
	protected float _param_growth;		//成長度によるパラメータ補正値(割合)
	protected float growthDifficult;	//成長のしやすさ
	protected float growthScale;		//成長量の補正値

	public bool isAuto {				//連続実行可能か
		get; protected set;
	}

	protected float waitTime;			//待ち時間

	public virtual void Start() {
		_waitTime = 0;
		nextLevelEXP = GameBalance.INITNEXTLEVELEXP;
	}

	public virtual void Update() {
		_waitTime += Time.deltaTime;
		transform.position = owner.transform.position;
	}

	/// <summary>
	/// 装備品の行動
	/// </summary>
	public void Action() {

		if(waitTime < _waitTime) {
			action();
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
		attach(unit);
	}

	/// <summary>
	/// 経験値を取得する
	/// </summary>
	/// <param name="exp">取得経験値</param>
	public void GainEXP(int exp) {

		experience += (uint)exp;
		//連続でレベルアップする可能性があるのでチェック
		while(experience >= nextLevelEXP) {
			//レベルアップ処理
			experience -= nextLevelEXP;
			level++;
			nextLevelEXP = GameBalance.GetNextLevelEXPFromEXP(nextLevelEXP, 1);
			//nextLevelEXP = GameBalance.GetNextLevelEXPFromLevel(level, 1);

			Debug.Log("Module LevelUp! Level:" + level + "NextEXP:" + nextLevelEXP);
		}

	}

}
