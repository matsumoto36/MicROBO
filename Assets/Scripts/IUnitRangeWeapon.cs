using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitRangeWeapon {

	float shotSpeed {		//弾のスピード
		get; set;
	}

	int penetration {		//弾の貫通回数
		get; set;
	}
}
