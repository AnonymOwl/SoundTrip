using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public enum 			Side {Left, Right};

	public Side				playerSide;

	private Camera			leftCamera;
	private Camera			rightCamera;
	private GameObject		rightPlayer;
	private PointsCreator	center;

	private Vector3			mousePosition;
	private Vector3			offset;

	void Start () {
		leftCamera = GameObject.Find("LeftCamera").GetComponent<Camera>();
		rightCamera = GameObject.Find("RightCamera").GetComponent<Camera>();
		rightPlayer = GameObject.Find("RightPlayer");
		center = GameObject.Find("Center").GetComponent<PointsCreator>();
	}
	
	void Update () {
		if (playerSide == Side.Right) {
			if (Application.platform == RuntimePlatform.Android)
				mousePosition = Input.GetTouch(0).position;
			else
				mousePosition = Input.mousePosition;
			mousePosition = rightCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));
			transform.position = mousePosition;
		}
		if (playerSide == Side.Left) {
			transform.position = new Vector3(-rightPlayer.transform.position.x, rightPlayer.transform.position.y, 0);
			if (Vector3.Distance(transform.position, Vector3.zero) > center.distance) {
				offset = transform.position;
				offset.Normalize();
				transform.position = offset * center.distance;
			}
		}
	}
}
