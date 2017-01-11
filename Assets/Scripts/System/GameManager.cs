using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static UnitPlayer UnitPlayer {	//プレイヤーキャラクタ
		get; set;
	}
	public static List<UnitBase> unitList {     //フィールド上のキャラクタリスト
		private get; set;
	}
	public static List<CanPauseObject> pauseObjectList {	//ポーズ可能なオブジェクト
		get; private set;
	}

	void Awake() {
		unitList = new List<UnitBase>();
		pauseObjectList = new List<CanPauseObject>();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	/// <summary>
	/// ポーズが可能なオブジェクトの動きを止める
	/// </summary>
	/// <param name="enabled">true:止める</param>
	public static void Pause(bool enabled) {
		foreach(CanPauseObject obj in pauseObjectList) {
			obj.Pause(enabled);
		}
	}
}