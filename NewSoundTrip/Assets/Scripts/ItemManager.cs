using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
	
	}

	void OnCollisionEnter(Collision other) {
		GameObject.Destroy(gameObject);
	}
}
