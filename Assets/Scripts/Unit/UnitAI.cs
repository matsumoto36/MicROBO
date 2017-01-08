using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitAIType {	//敵AIのタイプ
	Attacker,				//突っ込んでいく
	Ranger,					//距離をとって攻撃
}

/// <summary>
/// AIをまとめて置いておくクラス
/// </summary>
public class UnitAI {

	//共通
	public UnitBase targetUnit {	//攻撃対象のキャラクタ
		get; set;
	}

	public float fireRate {			//攻撃間隔
		get; set;
	}

	float _waitTime = 0;

	//Ranger
	public float keepDistance {		//保つ距離
		get; set;
	}

	/// <summary>
	/// AIの行動(振り分け前)
	/// </summary>
	public void AIUpdate(UnitEnemy unit) {

		if(targetUnit) {
			switch(unit.AIType) {
				case UnitAIType.Attacker:
					AIAttacker(unit);
					break;
				case UnitAIType.Ranger:
					AIRanger(unit);
					break;

			}
		}
		else {
			Debug.Log("Notarget");
		}

	}

	/// <summary>
	/// AI:アタッカーの行動
	/// </summary>
	void AIAttacker(UnitEnemy unit) {

		_waitTime += Time.deltaTime;

		//突撃
		unit.moveVec = unit.attackAngle = (targetUnit.transform.position - unit.transform.position).normalized;
		unit.transform.position += unit.moveVec * unit.speed * Time.deltaTime;

		//攻撃
		if(unit.equipWeapon && unit.equipWeapon.waitTime * fireRate < _waitTime) {
			_waitTime = 0;
			Debug.Log("AIAttack");
			unit.Attack();
		}

		
	}

	/// <summary>
	/// AI:レンジャーの行動
	/// </summary>
	void AIRanger(UnitEnemy unit) {

		_waitTime += Time.deltaTime;

		Vector2 diff = targetUnit.transform.position - unit.transform.position;
		float moveSpd = unit.speed;

		//攻撃の向きを計算
		unit.attackAngle = diff.normalized;
		//距離を計算
		float dist = diff.magnitude;
		if(dist < keepDistance) {
			//近い場合は遠ざかる
			unit.moveVec = unit.attackAngle * -1 * moveSpd / 2;

			//攻撃
			if(unit.equipWeapon && unit.equipWeapon.waitTime * fireRate * 2 < _waitTime) {
				_waitTime = 0;
				Debug.Log("AIAttack");
				unit.Attack();
			}
		}
		else {
			//遠い場合は近づく
			unit.moveVec = unit.attackAngle * moveSpd;

			//攻撃
			if(unit.equipWeapon && unit.equipWeapon.waitTime * fireRate < _waitTime) {
				_waitTime = 0;
				Debug.Log("AIAttack");
				unit.Attack();
			}
		}

		//移動
		unit.transform.position += unit.moveVec * Time.deltaTime;
	}
}
