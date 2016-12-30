using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;

/*
--------------------------
*                        *
* <Version>              *
* SaveDataSystem v1.4    *
*            作者 松元 涼 *
*                        *
--------------------------
*/

/**
	[使い方]
	基本的にPlayerPrefsと同じです。
	1. SaveData クラスを生成して、SetDataで書き込みます。
	2. GetDataを使うと内容を持ち出すことができます。
	3. ファイルとして保存する場合はSaveGameFileを実行してください。
	4. ロードする場合はLoadGameFileを実行してください。
	※以下の文字はキーとして使わないでください(データが壊れます)
	| , \ /
*/

//
// セーブデータファイルクラス
// SaveDataクラスの内容をファイルに保存する。
//
static class GameSaveLoad {

	//セーブするときのフォルダ名
	public static string folderName = "Saves";

	/// <summary>
	/// 	セーブデータの内容をファイルに出力するメソッド
	///		string fileName : ファイル名
	///		int num         : ファイルナンバー
	///		SaveData data   : 保存するデータ
	/// </summary>
	public static void SaveGameFile(string fileName, int num, SaveData data) {

		string path = Application.dataPath + "/" + folderName;  //保存先
		string name = fileName + num + ".sav";                  //ファイル名

		DebugMessage("[Save path] " + path + "/" + name, false);

		FileInfo info = new FileInfo(path + "/" + name);

		//ファイル書き込み設定:新規
		using(StreamWriter sw = info.CreateText()) {
			foreach(KeyValuePair<string, object> pair in data.GetDataAll()) {

				//データは "key,値", でセットにして保存する。
				string strData = pair.Key + "," + pair.Value;

				sw.WriteLine(strData);
				DebugMessage("[object] " + strData, false);

			}
			sw.Close();
		}
	}

	/// <summary>
	/// 	セーブデータをロードするメソッド
	///		string fileName : ファイル名
	///		int num         : ファイルナンバー
	///		ret SaveData    : ロードするセーブデータ
	/// </summary>
	public static SaveData LoadGameFile(string fileName, int num) {

		SaveData data = new SaveData();                         //返すセーブデータ

		string path = Application.dataPath + "/" + folderName;  //保存先
		string name = fileName + num + ".sav";                  //ファイル名

		DebugMessage("[path] " + path + "/" + name, false);

		FileInfo info = new FileInfo(path + "/" + name);

		//ファイル書き込み設定:読み込み
		using(StreamReader sr = new StreamReader(info.OpenRead(), Encoding.UTF8)) {

			string buff = "";

			while(!sr.EndOfStream) {

				string[] str = DataSplit(sr.ReadLine(), ',');
				data.SetData(str[0], str[1]);
				DebugMessage("[object] " + buff, false);
			}

		}

		return data;
	}



	/// <summary>
	///		文字列を分割するメソッド
	///		string rawData : 元データ
	///		char splitC    : 分割用文字
	///		ret string[]   : 分割後のデータ
	/// </summary>
	static string[] DataSplit(string rawData, char splitC) {

		return rawData.Split(splitC);
	}

	/// <summary>
	/// 	デバッグ用のメッセージをまとめたメソッド
	///		ログの管理をする
	///		string mes : ファイル名
	///		bool isErr : エラーかどうか
	/// </summary>
	static void DebugMessage(string mes, bool isErr) {

		//ログを出力するかどうか Default:true
#if true

		//エラー時
		if(isErr) {
			Debug.Log(mes);

			//エラーログでゲームを止めるか Default:true
#if true

			Debug.Break();

#endif
			return;
		}

		Debug.Log(mes);

#endif
	}
}


/// <summary>
///		セーブデータ本体のクラス。
///		データのセット・ゲットを行う
/// </summary>
public class SaveData {

	//データ保存にはDictionary<>を使用
	Dictionary<string, object> dataList = new Dictionary<string, object>();

	/* -------------------- セット系メソッド -------------------- */

	/// <summary>
	///		データをセットする
	///		string key : 取得用キー
	///		int data   : データ
	/// </summary>
	public void SetData(string key, object data) {

		dataList.Add(key, data);
	}



	/// <summary>
	/// 	全てのデータをセットする。
	///		Dictionary<string, object> data : セットするデータ
	/// </summary>
	public void SetDataAll(Dictionary<string, object> data) {

		dataList = data;
	}

	/* -------------------- ゲット系メソッド -------------------- */

	/// <summary>
	///		string key  : 取得用キー
	///		ret object  : 取得できるデータ
	/// </summary>
	public object GetData(string key) {

		object d = null;

		//例外キャッチ
		try {
			d = dataList[key];
		}
		catch(Exception e) {
			DebugMessage("[Err] <key = " + key + "> " + e.Message, true);
		}
		return d;
	}

	/// <summary>
	/// 	データをすべてゲットする。
	/// 	ret Dictionary<string, object> : 取得できるすべてのデータ
	/// </summary>
	public Dictionary<string, object> GetDataAll() {

		return dataList;
	}


	/* -------------------- システム系メソッド -------------------- */

	/// <summary>
	///		データの数を取得する
	///		ret int : データの数
	/// </summary>
	public int GetDataCount() {
		return dataList.Count;
	}

	/// <summary>
	/// 	デバッグ用のメッセージをまとめたメソッド
	///		ログの管理をする
	///		string mes : ファイル名
	///		bool isErr : エラーかどうか
	/// </summary>
	static void DebugMessage(string mes, bool isErr) {

		//ログを出力するかどうか Default:true
#if true

		//エラー時
		if(isErr) {
			Debug.Log(mes);

			//エラーログでゲームを止めるか Default:true
#if true

			Debug.Break();

#endif
			return;
		}

		Debug.Log(mes);

#endif
	}
}