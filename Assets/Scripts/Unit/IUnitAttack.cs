
/// <summary>
/// 攻撃に所有者がいることを確定させる
/// </summary>
interface IUnitAttack {

	UnitBase owner { get; set; }
}
