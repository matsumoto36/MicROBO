using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵
/// </summary>
public class UnitEnemy : UnitBase {

	static int enemyCount = 0;		//敵のカウンタ

	public UnitBase targetUnit {	//攻撃するキャラクタ
		get; private set;
	}
	public UnitAIType AIType {		//敵のAI
		get; private set;
	}
	public Vector3 attackAngle {	//敵の攻撃する方向
		get; set;
	}
	public Vector3 moveVec {		//敵の移動する方向
		get; set;
	}

	UnitAI unitAI;					//敵AI
	int expGain;					//落とす経験値
	float dropRate;					//装備のドロップ率

	//デバッグ用
	public WeaponBase testmodule;

	// Use this for initialization
	public override void Start() {

		enemyCount++;

		//初期設定
		unitName = SetEnemyName();
		AIType = UnitAIType.Ranger;
		unitAI = new UnitAI();
		unitAI.targetUnit = GameManager.UnitPlayer;

		unitAI.fireRate = 2;
		unitAI.keepDistance = 8;

		unitType = UnitType.Enemy;
		_hp = 100;
		_power = 4;
		_defence = 10;
		_speed = 1;
		memory = 0;
		_luck = 1;
		equipWeapon = testmodule;

		expGain = 5000;
		dropRate = 1.0f;


		base.Start();
	}

	/// <summary>
	/// 敵の攻撃
	/// </summary>
	public override void Attack() {
		//モジュールのアクションを発動
		if(equipWeapon) equipWeapon.Action();
	}

	/// <summary>
	/// 敵の移動
	/// </summary>
	public override void Move() {

		//AIに任せる
		unitAI.AIUpdate(this);

	}

	/// <summary>
	/// 敵が死んだときの処理
	/// </summary>
	/// <param name="unit">とどめを刺した相手:プレイヤー</param>
	public override void Death(UnitBase unit) {

		//経験値の付与
		unit.GainEXP(expGain);
		//ドロップ
		if(UnityEngine.Random.Range(0, 1.0f) < dropRate) {
			Debug.Log("Drop");
			DropModule(equipWeapon);
		}
		base.Death(unit);
	}

	/// <summary>
	/// 敵の名前を決める
	/// </summary>
	/// <returns>敵の名前</returns>
	string SetEnemyName() {
		//敵の名前 = Enemy + Type + ステージ番号 + 製造番号
		return string.Format("Enemy-{0}-{1}-{2}", "[Type]", "[StageNum]", enemyCount);
	}
}
