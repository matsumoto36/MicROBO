using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitShot : MonoBehaviour {

	public UnitBase owner { get; set; }
	public float shotSpeed { get; set; }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		//移動
		transform.position += transform.right * shotSpeed * Time.deltaTime;
	}
}
