
/// <summary>
/// 敵
/// </summary>
public class UnitEnemy : UnitBase {

	int expGain;        //落とす経験値

	public WeaponBase testmodule;

	// Use this for initialization
	public override void Start() {

		//初期設定
		unitName = "";
		unitType = UnitType.Enemy;
		_hp = 10;
		_power = 4;
		_defence = 10;
		_speed = 1;
		memory = 0;
		_luck = 1;
		equipWeapon = testmodule;

		expGain = 5000;


		base.Start();
	}

	/// <summary>
	/// 敵の攻撃
	/// </summary>
	public override void Attack() {
		
	}

	/// <summary>
	/// 敵の移動
	/// </summary>
	public override void Move() {
		
	}

	/// <summary>
	/// 敵が死んだときの処理
	/// </summary>
	/// <param name="unit">とどめを刺した相手:プレイヤー</param>
	public override void Death(UnitBase unit) {


		//経験値の付与
		unit.GainEXP(expGain);
		base.Death(unit);
	}
}
