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
	UnitInfoContiner[] UISet;	//情報格納用クラス

	UnitBase currentTargetUnit; //表示対象のキャラクタ

	// Use this for initialization
	void Start () {

		//色をセット
		UIColor = new Color[2];
		UIColor[(int)UnitType.Player] = new Color(0.5f, 1.0f, 0.5f);
		UIColor[(int)UnitType.Enemy] = new Color(1.0f, 0.5f, 0.5f);

		HPColor = new Color[2];
		HPColor[(int)UnitType.Player] = new Color(0, 0.8f, 0);
		HPColor[(int)UnitType.Enemy] = new Color(0.8f, 0, 0);
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
			ShowInfo(GameSettings.isUISimple);
		}
		else {
			//触れてない場合は非表示
			UISet[0].UIBody.gameObject.SetActive(false);
			UISet[1].UIBody.gameObject.SetActive(false);
		}
	}

	/// <summary>
	/// ステータスを表示する
	/// </summary>
	void ShowInfo(bool isSimple) {

		int infoNum = isSimple ? 1 : 0;

		//表示
		UISet[infoNum].UIBody.gameObject.SetActive(true);
		Debug.Log("mouse : " + Input.mousePosition);
		Debug.Log("uiset : " + UISet[infoNum].UIBody.sizeDelta);
		//UIの位置を設定
		if(Input.mousePosition.x > Screen.width - UISet[infoNum].UIBody.sizeDelta.x &&
			Input.mousePosition.y < UISet[infoNum].UIBody.sizeDelta.y) {
			//マウスとUIが被るので別の位置に
			SetUIPosition(isSimple, 1);
		}
		else {
			//普通の位置
			SetUIPosition(isSimple, 0);
		}

		//UIのパネルの色を設定
		foreach(Image i in UISet[infoNum].UIPanels) {
			i.color = UIColor[(int)currentTargetUnit.unitType];
		}

		//HPのバーの色を設定
		UISet[infoNum].unitLifeBar.color = HPColor[(int)currentTargetUnit.unitType];

		//数値・画像データ反映
		UISet[infoNum].texUnitName.text = currentTargetUnit.unitName;
		UISet[infoNum].texUnitMaxHp.text = currentTargetUnit.maxHp.ToString();
		UISet[infoNum].texUnitNowHp.text = currentTargetUnit.nowHp.ToString();

		UISet[infoNum].unitImage.sprite = currentTargetUnit.gameObject.GetComponentInChildren<SpriteRenderer>().sprite;

		//割合の情報を反映
		Vector3 scale = new Vector3((float)currentTargetUnit.nowHp / currentTargetUnit.maxHp , 1, 1);
		UISet[infoNum].unitLifeBar.rectTransform.localScale = scale;

		if(!isSimple) {//詳細表示の項目を設定

			//ステータス類
			UISet[infoNum].texUnitPower.text = currentTargetUnit.power.ToString();
			UISet[infoNum].texUnitDefence.text = currentTargetUnit.defence.ToString();
			UISet[infoNum].texUnitSpeed.text = currentTargetUnit.speed.ToString();
			UISet[infoNum].texUnitLuck.text = currentTargetUnit.luck.ToString();

			//経験値類
			UISet[infoNum].texUnitLevel.text = currentTargetUnit.level.ToString();
			Vector3 scaleExp = new Vector3((float)currentTargetUnit.experience / currentTargetUnit.nextLevelEXP, 1, 1);
			UISet[infoNum].unitExpBar.rectTransform.localScale = scaleExp;

			//装備類
			UISet[infoNum].unitEquipWeapon.text = 
				currentTargetUnit.equipWeapon ? currentTargetUnit.equipWeapon.moduleName : "";
			UISet[infoNum].unitEquipArmor.text =
				currentTargetUnit.equipModule[0] ? currentTargetUnit.equipModule[0].moduleName : "";
			UISet[infoNum].unitEquipArmor2.text =
				currentTargetUnit.equipModule[1] ? currentTargetUnit.equipModule[1].moduleName : "";
		}
	}

	/// <summary>
	/// UIを指定の位置に移動させる
	/// </summary>
	/// <param name="positionNum">UIの表示位置番号</param>
	void SetUIPosition(bool isSimple, int positionNum) {

		int infoNum = isSimple ? 1 : 0;
		//TODO
		UISet[infoNum].UIBody.anchoredPosition = UISet[infoNum].UIPos[positionNum];
		UISet[infoNum].UIBody.anchorMin = UISet[infoNum].UIAnchorMin[positionNum];
		UISet[infoNum].UIBody.anchorMax = UISet[infoNum].UIAnchorMax[positionNum];
	}
}
