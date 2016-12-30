
public class UnitEnemy : UnitBase {

	// Use this for initialization
	public override void Start() {

		//初期設定
		unitName = "";
		unitType = UnitType.Enemy;
		_hp = 100;
		_power = 0;
		_defense = 0;
		_speed = 1;
		memory = 0;
		_luck = 1;

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
}
