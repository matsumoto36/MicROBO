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
	protected int level;						//レベル
	protected int experience;					//自分の総経験値
	protected int memory;						//記憶力

	protected int _hp;							//基礎体力
	protected int _power;						//基礎攻撃力
	protected float _critDamage;				//基礎クリティカルダメージ倍率
	protected int _defense;						//基礎防御力
	protected float _speed;						//基礎移動速度
	protected int _luck;						//基礎運

	//キャラクタの情報(装備後)
	public int hp { get; set; }					//装備後の体力
	public int power { get; set; }				//装備後の攻撃力
	public float critDamage { get; set; }		//装備後のクリティカルダメージ倍率
	public int defense { get; set; }			//装備後の防御力
	public float speed { get; set; }			//装備後の移動速度
	public int luck { get; set; }				//装備後の運

	protected ModuleBase equipModule;				//装備品

	//制御情報
	public bool isFreeze;                       //操作できないか(true : できない)

	public virtual void Start() {

		Debug.Log("UnitInitStart");

		isFreeze = false;

		if(equipModule) {
			//パラメータ計算
			EquipModule(equipModule);
		}

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

		//Damage balance (小数点切り捨て)
		//Damage = power - 0.9 * defence +- rand(power / 16)
		int damage = (int)(pow - 0.9f * defense + Random.Range(-pow / 16, pow / 16));
		hp -= damage;

		//死亡チェック
		if(hp <= 0) {
			Death();
		}
	}

	/// <summary>
	/// キャラクタが死亡したときの処理
	/// </summary>
	public virtual void Death() {

		Destroy(gameObject);
	}

	/// <summary>
	/// キャラクタに装備させるときの処理
	/// </summary>
	/// <param name="module">装備するモジュール</param>
	public void EquipModule(ModuleBase module) {

		Debug.Log("Equip");

		//装備
		equipModule = module;

		//基礎パラメータ反映
		hp = _hp;
		power = _power;
		critDamage = _critDamage;
		defense = _defense;
		speed = _speed;
		luck = _luck;

		//最終パラメータ反映
		equipModule.Attach(this);

	}

	public void OnTriggerEnter2D(Collider2D other) {

		Debug.Log("Hit : " + other.tag);
		if(other.tag == "UnitAttack" ) {

			//所有者を取得
			UnitBase otherUnit = other.GetComponent<UnitShot>().owner;

			//自分と違うタイプならダメージ発生
			if(otherUnit.unitType != unitType) {
				Damage(otherUnit);
			}
		}
	}
}