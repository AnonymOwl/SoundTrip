using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PlayerManager : MonoBehaviour {

	public enum 			Side {Left, Right};

	public Side				playerSide;

	private Camera			leftCamera;
	private Camera			rightCamera;
	private GameObject		rightPlayer;
	private PointsCreator	center;

	private Vector3			mousePosition;
	private Vector3			offset;
	private Vector2			screenCenter;


	void Start () {
		leftCamera = GameObject.Find("LeftCamera").GetComponent<Camera>();
		rightCamera = GameObject.Find("RightCamera").GetComponent<Camera>();
		rightPlayer = GameObject.Find("RightPlayer");
		center = GameObject.Find("Center").GetComponent<PointsCreator>();
		Cursor.lockState = CursorLockMode.Locked;
		mousePosition = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), -20);
		screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
	}
	
	void Update () {
		if (playerSide == Side.Right) {
			if (Application.platform == RuntimePlatform.Android)
				mousePosition = Input.GetTouch(0).position;
			else {
				mousePosition += new Vector3(-Input.GetAxis("Mouse X") / 4, Input.GetAxis("Mouse Y") / 4, 0);
			}
			//for actual mouse movement
			//mousePosition = rightCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));
			transform.position = mousePosition;
			if (Vector3.Distance(transform.position, new Vector3(0, 0, transform.position.z)) > center.distance) {
				offset = transform.position;
				offset = new Vector3(offset.x, offset.y, 0);
				offset.Normalize();
				transform.position = new Vector3(offset.x * center.distance, offset.y * center.distance, rightCamera.transform.position.z -10);
				mousePosition = transform.position;
			}
		}
		if (playerSide == Side.Left) {
			/* full movement
			transform.position = new Vector3(-rightPlayer.transform.position.x, rightPlayer.transform.position.y, 0);
			//uncomment this for free movement inside circle
			//if (Vector3.Distance(transform.position, Vector3.zero) > center.distance) {
			offset = transform.position;
			offset.Normalize();
			transform.position = new Vector3(offset.x * center.distance, offset.y * center.distance, 0);
			*/
			if (GetCurrentZone(gameObject, 1) != GetCurrentZone(rightPlayer, -1) && !DOTween.IsTweening(transform)) {
				transform.DOMove(center.positions[GetCurrentZone(rightPlayer, -1)], 0.1f, false);
			}
		}
	}

	//TODO: clean that
	int	GetCurrentZone(GameObject thing, int invert) {
		int zone;

		Vector2 refAngle = new Vector2(0, 1);
		Vector2 currentAngle = new Vector2(invert * thing.transform.position.x - center.transform.position.x, thing.transform.position.y - center.transform.position.y);
		float finalAngle = MyAngle(refAngle, currentAngle);
		float halfZone = 360f / (2f * (float)center.number);

		if (finalAngle < MyAngle(refAngle, center.positions[0]) + halfZone)
			zone = 0;
		else if (finalAngle < MyAngle(refAngle, center.positions[1]) + halfZone)
			zone = 1;
		else if (finalAngle < MyAngle(refAngle, center.positions[2]) + halfZone)
			zone = 2;
		else if (finalAngle < MyAngle(refAngle, center.positions[3]) + halfZone)
			zone = 3;
		else if (finalAngle < MyAngle(refAngle, center.positions[4]) + halfZone)
			zone = 4;
		else if (finalAngle < MyAngle(refAngle, center.positions[5]) + halfZone)
			zone = 5;
		else if (finalAngle < MyAngle(refAngle, center.positions[6]) + halfZone)
			zone = 6;
		else if (finalAngle < MyAngle(refAngle, center.positions[7]) + halfZone)
			zone = 7;
		else
			zone = 0;

		return zone;
	}

	//workaround to have a 360 degress angle because I'm lazy
	float MyAngle(Vector2 a1, Vector2 a2) {
		float angle = Vector2.Angle(a1, a2);

		if (a2.x < a1.x && angle != 0)
			angle = Mathf.Abs(angle - 360);

		return angle;
	}
}
