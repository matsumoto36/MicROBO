using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

delegate void ExecMenu();

public class PauseMenu : MonoBehaviour {

	enum MenuContents {
		UIInfoToggle
	}

	const float MENUSIZEW = 150;		//メニューの幅
	const float MENUSPACE = 20;			//メニューの上下にあけるスペース

	const float MENUHEIGHT = 40;        //一つの項目の高さ
	const float BUTTONSIZEH = 30;		//項目の高さ

	readonly string[] MenuName = {		//項目の名前
		"表示切替",
	};

	public bool isShowMenu {			//メニューが表示中か
		get; private set;
	}

	[SerializeField]
	RectTransform menuUI;				//表示用UI

	List<MenuContents> menuContents;	//メニューに表示される項目

	Button menuButtonPre;				//メニューボタンテンプレ

	// Use this for initialization
	void Start () {

		menuContents = new List<MenuContents>();
		menuUI.gameObject.SetActive(false);

		//リソース読み込み
		menuButtonPre = Resources.Load<Button>("System/MenuButton");

		//メニューに項目を追加
		menuContents.Add(MenuContents.UIInfoToggle);
		menuContents.Add(MenuContents.UIInfoToggle);
		menuContents.Add(MenuContents.UIInfoToggle);

		//メニュー表のサイズ調整
		menuUI.sizeDelta = new Vector2(MENUSIZEW, MENUSPACE * 2 + (menuContents.Count-1) * MENUHEIGHT + BUTTONSIZEH);

		//ボタン配置
		for(int i = 0;i < menuContents.Count;i++) {
			Button b = Instantiate(menuButtonPre, menuUI);
			//実行メソッド選択
			switch(menuContents[i]) {
				case MenuContents.UIInfoToggle:
					b.onClick.AddListener(ToggleInfo);
					break;
			}
			
			//位置・サイズ初期化
			b.transform.localPosition = Vector3.zero;
			b.transform.localScale = Vector3.one;

			RectTransform rt = b.GetComponent<RectTransform>();
			rt.anchoredPosition = new Vector2(0, -(MENUSPACE + MENUHEIGHT * i));
			rt.sizeDelta = new Vector2(MENUSIZEW - MENUSPACE * 2, BUTTONSIZEH);
			b.GetComponentInChildren<Text>().text = MenuName[(int)menuContents[i]];
		}
	}

	/// <summary>
	/// キャラクタ情報の詳細度を変更
	/// </summary>
	void ToggleInfo() {
		GameSettings.isUISimple = !GameSettings.isUISimple;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Escape)) {
			//メニュー表示切替
			isShowMenu = !isShowMenu;
			menuUI.gameObject.SetActive(isShowMenu);
			//ポーズ処理切り替え
			GameManager.Pause(isShowMenu);

		}
	}
}
