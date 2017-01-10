using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボタンを押したときの挙動
/// </summary>
public class ButtonManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// デバッグ用設定切り替え
	/// </summary>
	public void ToggleIsUISimple() {
		GameSettings.isUISimple = !GameSettings.isUISimple;
	}
}
