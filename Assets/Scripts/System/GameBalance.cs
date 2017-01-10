using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム全体にかかわる各種計算式をまとめるクラス
/// </summary>
static class GameBalance {

	public const int INITNEXTLEVELEXP = 10;	//レベル1から2になるまでの必要経験値

	/// <summary>
	/// ダメージ量計算
	/// </summary>
	/// <param name="attackPower">相手の攻撃力</param>
	/// <param name="defence">自分の防御力</param>
	/// <returns>自分へのダメージ</returns>
	public static int GetDamage(int attackPower, int defence) {
 
		//Damage(小数点切り捨て) = power - 0.9 * defence +- rand(power / 16)
		return (int)(attackPower - 0.9f * defence + Random.Range(-defence / 16, attackPower / 16));
	}

	/// <summary>
	/// レベルアップに必要な経験値を取得する
	/// </summary>
	/// <param name="level">現在のレベル</param>
	/// <param name="growthDiffcult">成長のしやすさ</param>
	/// <returns>必要な経験値</returns>
	public static uint GetNextLevelEXPFromLevel(int level, float growthDiffcult) {


		if(level == 1) {
			return INITNEXTLEVELEXP;
		}
		else {
			//必要な経験値 = 前のレベルの必要経験値 * (1.3 * 成長のしやすさ)
			uint nextEXP = INITNEXTLEVELEXP;
			for(int i = 1;i < level;i++) {
				nextEXP = (uint)(nextEXP * (1.3f * growthDiffcult));
			}

			return nextEXP;
		}
	}

	/// <summary>
	/// レベルアップに必要な経験値を取得する
	/// </summary>
	/// <param name="level">現在のレベル</param>
	/// <returns>必要な経験値</returns>
	public static uint GetNextLevelEXPFromLevel(int level) {
		return GetNextLevelEXPFromLevel(level, 1.0f);
	}


	public static uint GetNextLevelEXPFromEXP(uint prevExp, float growthDiffcult) {

		//必要な経験値 = 前のレベルの必要経験値 * (1.3 * 成長のしやすさ)
		return (uint)(prevExp * (1.3f * growthDiffcult)) ;
	}


}
