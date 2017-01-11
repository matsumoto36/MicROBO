using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ポーズ可能
/// </summary>
public class CanPauseObject : MonoBehaviour {

	Behaviour[] pauseBehavior;
	readonly string[] ignoreBehavior = {
		"CircleCollider2D",
	};


	void Start() {

		pauseBehavior = null;
		//マネージャーに登録
		GameManager.pauseObjectList.Add(this);
	}

	/// <summary>
	/// ポーズ実行
	/// </summary>
	/// <param name="enable">true:ポーズ有効</param>
	public void Pause(bool enable) {
		//複数回ポーズをするのを防ぐ
		if(pauseBehavior == null ^ enable) return;

		if(enable) {
			pauseBehavior = Array.FindAll(GetComponentsInChildren<Behaviour>(), (obj) => { return obj.enabled && !IgnoreBehaviorCheck(obj); });
		}
		//ポーズ可能なコンポーネントを有効/無効にする
		foreach(Behaviour behavior in pauseBehavior) {
			behavior.enabled = !enable;
		}
		if(!enable) {
			pauseBehavior = null;
		}

	}


	/// <summary>
	/// 対象外のコンポーネントかどうかチェック
	/// </summary>
	/// <param name="behavior">チェックするコンポーネント</param>
	/// <returns>対象外かどうか true:対象外</returns>
	bool IgnoreBehaviorCheck(Behaviour behavior) {
		foreach(var ignoreStr in ignoreBehavior) {
			if(ignoreStr == behavior.GetType().Name) return true;
		}
		return false;
	}

	void OnDestroy() {
		//登録解除
		GameManager.pauseObjectList.Remove(this);
	}
}
