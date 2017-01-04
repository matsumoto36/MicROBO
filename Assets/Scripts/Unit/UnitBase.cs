using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType {
	Player,
	Enemy,
}

// キャラクタのデータの親
public abstract class UnitBase : MonoBehaviour {

	//キャラクタの情報(基本)
	protected string unitName;					//名前
	protected UnitType unitType;				//自分のタイプ
	int level;									//レベル
	uint nextLevelEXP;							//レベルアップに必要な経験値
	uint experience;							//現在の経験値
	protected int memory;						//記憶力

	protected int _hp;							//基礎体力
	protected int _power;						//基礎攻撃力
	protected float _critDamage;				//基礎クリティカルダメージ倍率
	protected int _defence;						//基礎防御力
	protected float _speed;						//基礎移動速度
	protected int _luck;						//基礎運

	//キャラクタの情報(装備後)
	public int hp { get; set; }					//装備後の体力
	public int power { get; set; }				//装備後の攻撃力
	public float critDamage { get; set; }		//装備後のクリティカルダメージ倍率
	public int defence { get; set; }			//装備後の防御力
	public float speed { get; set; }			//装備後の移動速度
	public int luck { get; set; }				//装備後の運

	protected WeaponBase equipWeapon;			//装備品:武器

	//制御情報
	public bool isFreeze;						//操作できないか(true : できない)

	public virtual void Start() {

		Debug.Log("UnitInitStart");

		//初期設定
		level = 1;
		isFreeze = false;

		//初期装備のパラメータ計算
		//CalcStatus();
		EquipWeapon(equipWeapon);

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

		hp -= damage;

		//死亡チェック
		if(hp <= 0) {
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
		}
	}

	/// <summary>
	/// 装備時のステータスを計算する
	/// </summary>
	void CalcStatus() {
		
		//基礎パラメータ反映
		hp = _hp;
		power = _power;
		critDamage = _critDamage;
		defence = _defence;
		speed = _speed;
		luck = _luck;

		//装備後パラメータ反映
		if(equipWeapon) equipWeapon.Attach(this);


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
			UnitBase otherUnit = other.GetComponent<UnitShot>().owner;

			//自分と違うタイプならダメージ発生
			if(otherUnit.unitType != unitType) {
				Damage(otherUnit);
			}
		}
	}

	/// <summary>
	/// 装備中の武器を落とす
	/// </summary>
	public void DropModule() {

		//装備を落とす動作
		equipWeapon.Drop();
		//装備をはずす
		equipWeapon = null;

		//ステータス計算
		CalcStatus();
	}
}