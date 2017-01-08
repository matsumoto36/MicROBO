using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤー
/// </summary>
public class UnitPlayer : UnitBase {

	public WeaponBase module;

	void Awake() {
		//自分を登録
		GameManager.UnitPlayer = this;
	}

	// Use this for initialization
	public override void Start() {

		//初期設定
		unitName = "Player";
		unitType = UnitType.Player;
		_hp = 100;
		_power = 10;
		_defence = 5;
		_speed = 3;
		memory = 0;
		_luck = 1;
		equipWeapon = module;

		base.Start();
	}

	public override void Update() {

		if(Input.GetMouseButton(0)) {
			//攻撃
			Attack();
		}

		if(Input.GetKeyDown(KeyCode.Q)) {
			//装備を捨てる(Debug)
			DropModule(equipWeapon);
		}

		base.Update();
	}

	/// <summary>
	/// プレイヤーの移動
	/// </summary>
	public override void Move() {

		Vector3 moveVec = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		transform.position += moveVec * speed * Time.deltaTime;
	}

	/// <summary>
	/// プレイヤーの攻撃
	/// </summary>
	public override void Attack() {
		//モジュールのアクションを発動
		if(equipWeapon) equipWeapon.Action();
	}
}