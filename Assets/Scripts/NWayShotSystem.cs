using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
*/

public static class NWayShotSystem {

	//
	// 撃つために必要な一塊のデータ
	//
	public class ShotSet {

		//BaseData--------------------
		public GameObject shotPre { get; private set; }

		//ShotData--------------------
		public int genCount { get; private set; }
		public float dirInterval { get; private set; }

		//SystemData------------------
		public bool isToTarget { get; private set; }



	//
	// コンストラクタ
	//
	public ShotSet(GameObject pre, int n, float dir, bool isTargetShot) {
			shotPre = pre;

			genCount = n;
			dirInterval = dir;

			isToTarget = isTargetShot;
		}

		public void SetShot(GameObject pre, int n, float dir) {

			shotPre = pre;

			genCount = n;
			dirInterval = dir;
		}
	}

	//
	// 撃った弾のデータ
	//
	public class ShotResult {

		//弾のリスト
		List<GameObject> shotObj;

		public ShotResult() {
			shotObj = new List<GameObject>();
		}

		public List<GameObject> GetShotList() {
			return shotObj;
		}

		public void AddList(GameObject s) {
			shotObj.Add(s);
		}
	}

	//
	// 指定の位置で撃つ
	//
	public static ShotResult Shot(ShotSet data, Vector3 position, Vector3 vec, float degOffset) {

		//オフセット用加算
		float shotDeg = degOffset;

		//リターン用ShotResult
		ShotResult result = new ShotResult();

		// ターゲットに向かって撃つか
		if(data.isToTarget) {
			shotDeg += Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
		}

		//端の角度
		float startDeg = shotDeg + (data.genCount - 1) / 2.0f * data.dirInterval;

		for(int i = 0;i < data.genCount;i++) {

			GameObject s = MonoBehaviour.Instantiate(data.shotPre, position, Quaternion.Euler(0, 0, startDeg - data.dirInterval * i));
			result.AddList(s);
		}

		return result;

	}
	public static ShotResult Shot(ShotSet data, Vector3 position, Vector3 vec) {
		return Shot(data, position, vec, 0);
	}
}
