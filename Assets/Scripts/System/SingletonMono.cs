using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シングルトンの基底クラス。
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour {

	private static T instance;
	public static T Instance {
		get {
			if(instance == null) {
				//取得を試みる
				instance = (T)FindObjectOfType(typeof(T));
				if(instance == null) {
					//取得できないのでエラー
					Debug.LogError(typeof(T) + "is nothing");
				}
			}
			//instanceまたはnull
			return instance;
		}
	}



	protected void Awake() {
		//複数instanceがないかチェック
		CheckInstance();
	}


	/// <summary>
	/// instanceが一つかチェックする
	/// </summary>
	/// <returns>一つ : true</returns>
	protected bool CheckInstance() {

		if(this == Instance) { return true; }
		Destroy(this);
		return false;
	}
}