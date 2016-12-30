using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlayer : UnitBase {

	public ModuleBase module;

	// Use this for initialization
	public override void Start() {

		//初期設定
		unitName = "";
		unitType = UnitType.Player;
		_hp = 100;
		_power = 10;
		_defence = 10;
		_speed = 3;
		memory = 0;
		_luck = 1;
		equipModule = module;

		base.Start();
	}

	public override void Update() {

		if(Input.GetMouseButton(0)) {
			//攻撃
			Attack();
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
		equipModule.Action();
	}
}