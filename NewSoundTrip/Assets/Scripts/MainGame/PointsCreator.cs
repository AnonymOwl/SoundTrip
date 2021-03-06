﻿using UnityEngine;
using System.Collections.Generic;

public class PointsCreator : MonoBehaviour {

	public int				distance = 1;
	public int				number = 8;
	public GameObject		parent;
	public GameObject		pointPrefab;
	public List<Vector2>	positions;
	private float			cosAngle;
	private float			sinAngle;

	void Start () {
		int	angle = 90;
		float cosAngle;
		float sinAngle;
		GameObject point;


		for (int i = 0; i < number; i++) {
			cosAngle = Mathf.Cos(Mathf.Deg2Rad * angle);
			sinAngle = Mathf.Sin(Mathf.Deg2Rad * angle);
			point = (GameObject)Instantiate(pointPrefab);
			point.name = "Point" + i;
			point.transform.position = new Vector3(cosAngle * distance, sinAngle * distance, 0);
			positions.Add(new Vector2(cosAngle * distance, sinAngle * distance));
			point.transform.SetParent(parent.transform);
			angle -= 360 / number;
		}
		GameObject.Find("Tunnel").transform.localScale = new Vector3(distance * 2.5f, distance * 2.5f, 1);
		GameObject.Find("Moving").GetComponent<MoveManager>().PlacePoints();
	}
}
