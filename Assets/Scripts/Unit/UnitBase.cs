using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType {
	Player,
	Enemy,
}

/// <summary>
/// キャラクタのデータの基底クラス
/// </summary>
public abstract class UnitBase : MonoBehaviour {

	//キャラクタの情報(基本)
	public string unitName {					//名前
		get; protected set;
	}
	public UnitType unitType {					//自分のタイプ
		get; protected set;
	}
	public int level {							//レベル
		get; private set;
	}
	public uint nextLevelEXP {					//レベルアップに必要な経験値
		get; private set;
	}
	public uint experience {					//現在の経験値
		get; private set;
	}

	protected int _hp;							//基礎体力
	protected int _power;						//基礎攻撃力
	protected float _critDamage;				//基礎クリティカルダメージ倍率
	protected int _defence;						//基礎防御力
	protected float _speed;						//基礎移動速度
	protected int _luck;						//基礎運

	//キャラクタの情報(装備後)
	public int maxHp { get; set; }				//装備後の最大体力
	public int nowHp { get; set; }				//装備後の現在の体力
	public int power { get; set; }				//装備後の攻撃力
	public int defence { get; set; }			//装備後の防御力
	public float speed { get; set; }			//装備後の移動速度
	public int luck { get; set; }				//装備後の運

	public WeaponBase equipWeapon {				//装備品:武器
		get; protected set;
	}
	public ModuleBase[] equipModule {			//装備品:その他
		get; protected set;
	}


	//制御情報
	public bool isFreeze;						//操作できないか(true : できない)

	/// <summary>
	/// キャラクタの能力値をレベル1の状態にする
	/// </summary>
	public void SetInitStatus() {
		level = 1;										//レベル
		nextLevelEXP = GameBalance.INITNEXTLEVELEXP;	//レベルアップに必要な経験値
		experience = 0;									//現在の経験値

		_hp = 10;										//基礎体力
		_power = 2;										//基礎攻撃力
		_defence = 2;									//基礎防御力
		_speed = 5;										//基礎移動速度
		_luck = 1;										//基礎運
}

	public virtual void Start() {

		Debug.Log("UnitInitStart");

		//初期設定
		level = 1;
		isFreeze = false;
		equipModule = new ModuleBase[2];

		//初期装備のパラメータ計算
		//CalcStatus();
		EquipWeapon(equipWeapon);
		//全回復
		nowHp = maxHp;

		//必要経験値を取得
		nextLevelEXP = GameBalance.INITNEXTLEVELEXP;

		Debug.Log("UnitInitEnd");
	}

	public virtual void Update() {
		//常に動こうとする
		if(!isFreeze) {
			Move();
		}
	}

	/// <summary>
	/// キャラクタの移動処理
	/// </summary>
	public abstract void Move();

	/// <summary>
	///	キャラクタの攻撃
	/// </summary>
	public abstract void Attack();

	/// <summary>
	/// キャラクタにダメージを与える
	/// </summary>
	/// <param name="unit">攻撃したUnit</param>
	public virtual void Damage(UnitBase unit) {

		int pow = unit.power;

		//ダメージ量取得
		int damage = GameBalance.GetDamage(pow, defence);

		Debug.Log(unit.unitType + " > " + unitType + " Attack!  Damage : " + damage);

		nowHp -= damage;

		//死亡チェック
		if(nowHp <= 0) {
			Death(unit);
		}
	}

	/// <summary>
	/// キャラクタが死亡したときの処理
	/// </summary>
	/// <param name="unit">とどめを刺したキャラクタ</param>
	public virtual void Death(UnitBase unit) {

		Destroy(gameObject);
	}	

	/// <summary>
	/// キャラクタに武器を装備させるときの処理
	/// </summary>
	/// <param name="module">装備するモジュール</param>
	public void EquipWeapon(WeaponBase weapon) {

		//違う装備をすでに装備していた場合は落とす
		if(equipWeapon && equipWeapon != weapon) DropModule(equipWeapon);

		//装備
		equipWeapon = weapon;

		if(equipWeapon) {
			Debug.Log("EquipStart");
			//親子関係設定
			weapon.transform.parent = transform;
			weapon.name = string.Format("[Weapon] {0}", weapon.moduleName);
			weapon.transform.localPosition = Vector3.zero;
			//最終パラメータ反映
			CalcStatus();
		}
		else {
			Debug.Log("NoModule!");
			//最終パラメータ反映
			CalcStatus();
		}
	}

	/// <summary>
	/// 装備時のステータスを計算する
	/// </summary>
	void CalcStatus() {
		
		//基礎パラメータ反映
		maxHp = _hp;
		power = _power;
		defence = _defence;
		speed = _speed;
		luck = _luck;

		//装備後パラメータ反映
		if(equipWeapon) equipWeapon.Attach(this);

		//体力調整
		nowHp = Mathf.Min(nowHp, maxHp);
	}

	/// <summary>
	/// 経験値を取得する
	/// </summary>
	/// <param name="exp">取得経験値</param>
	public void GainEXP(int exp) {

		long t = System.DateTime.Now.ToBinary();

		experience += (uint)exp;
		//連続でレベルアップする可能性があるのでチェック
		while(experience >= nextLevelEXP) {

			//レベルアップ処理
			experience -= nextLevelEXP;
			level++;
			nextLevelEXP = GameBalance.GetNextLevelEXPFromEXP(nextLevelEXP, 1);
			Debug.Log(unitType + " LevelUp! Level:" + level + "NextEXP:" + nextLevelEXP);
		}

		//装備にも経験値を取得させる
		if(equipWeapon) {
			equipWeapon.GainEXP(exp);
		}

		Debug.Log("time:" + (System.DateTime.Now.ToBinary() - t));
	}

	public void OnTriggerEnter2D(Collider2D other) {

		if(other.tag == "UnitAttack" ) {

			//所有者を取得
			UnitBase otherUnit = other.GetComponent<IUnitAttack>().owner;

			//自分と違うタイプならダメージ発生
			if(otherUnit.unitType != unitType) {
				Damage(otherUnit);
			}
		}
	}

	/// <summary>
	/// 装備中の装備を落とす
	/// </summary>
	public void DropModule(ModuleBase module) {

		//装備を落とす動作
		if(module) module.Drop();
		//装備をはずす
		Debug.Log("Drop");
		switch(module.moduleType) {
			case ModuleType.Weapon:
				equipWeapon = null;
				break;
		}
		

		//ステータス計算
		CalcStatus();
	}
}