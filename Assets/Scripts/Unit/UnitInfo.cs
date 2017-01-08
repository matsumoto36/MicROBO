using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// カーソルを合わせると情報が出てくる
/// </summary>
public class UnitInfo : MonoBehaviour {


	Color[] UIColor;            //UIのパネルの色
	Color[] HPColor;			//HPの色

	[SerializeField]
	GameObject infoUI;			//UI本体

	UnitInfoSet UISet;			//情報格納用クラス
	UnitBase currentTargetUnit;	//表示対象のキャラクタ

	// Use this for initialization
	void Start () {

		//色をセット
		UIColor = new Color[2];
		UIColor[(int)UnitType.Player] = new Color(0.5f, 1.0f, 0.5f);
		UIColor[(int)UnitType.Enemy] = new Color(1.0f, 0.5f, 0.5f);

		HPColor = new Color[2];
		HPColor[(int)UnitType.Player] = new Color(0, 0.8f, 0);
		HPColor[(int)UnitType.Enemy] = new Color(0.8f, 0, 0);

		//オブジェクト取得
		UISet = new UnitInfoSet();
		UISet.backImage = infoUI.GetComponent<Image>();
		UISet.texUnitName = infoUI.transform.FindChild("UnitName").GetComponent<Text>();
		UISet.unitImage = infoUI.transform.FindChild("UnitImage").GetComponent<Image>();

		Transform t = infoUI.transform.FindChild("LifeBar");
		UISet.texUnitMaxHp = t.FindChild("UnitMaxHP").GetComponent<Text>();
		UISet.texUnitNowHp = t.FindChild("UnitNowHP").GetComponent<Text>();
		UISet.unitLifeBar = t.FindChild("BarForword").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {

		//マウスポインタに重なっているキャラクタを取得
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.forward);
		if(hit && hit.collider.tag == "Unit") {
			UnitBase u = hit.collider.gameObject.GetComponent<UnitBase>();
			if(u != currentTargetUnit) {

				currentTargetUnit = u;
			}
			//ステータスの表示
			ShowStatus();
		}
		else {
			infoUI.SetActive(false);
		}
	}

	/// <summary>
	/// ステータスを表示する
	/// </summary>
	void ShowStatus() {
		
		//表示
		infoUI.SetActive(true);

		//UIのパネルの色を設定
		UISet.backImage.color = UIColor[(int)currentTargetUnit.unitType];

		//HPのバーの色を設定
		UISet.unitLifeBar.color = HPColor[(int)currentTargetUnit.unitType];

		//数値・画像データ反映
		UISet.texUnitName.text = currentTargetUnit.unitName;
		UISet.texUnitMaxHp.text = currentTargetUnit.maxHp.ToString();
		UISet.texUnitNowHp.text = currentTargetUnit.nowHp.ToString();

		UISet.unitImage.sprite = currentTargetUnit.gameObject.GetComponentInChildren<SpriteRenderer>().sprite;

		//割合の情報を反映
		Vector3 scale = new Vector3((float)currentTargetUnit.nowHp / currentTargetUnit.maxHp , 1, 1);
		UISet.unitLifeBar.rectTransform.localScale = scale;
	}
}

/// <summary>
/// キャラクタの表示用UIのオブジェクトを格納する
/// </summary>
class UnitInfoSet {

	public Image backImage {	//UIのパネル
		get; set;
	}

	public Text texUnitName {	//キャラクタの名前
		get; set;
	}
	public Text texUnitMaxHp {	//キャラクタの最大体力
		get; set;
	}
	public Text texUnitNowHp {	//キャラクタの今の体力
		get; set;
	}

	public Image unitImage {	//キャラクタの画像
		get; set;
	}
	public Image unitLifeBar {	//キャラクタのライフバー
		get; set;
	}
}
