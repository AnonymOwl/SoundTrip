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

	private int				currentZone = -1;
	private int				keyNb = 0;


	void Start () {
		leftCamera = GameObject.Find("LeftCamera").GetComponent<Camera>();
		rightCamera = GameObject.Find("RightCamera").GetComponent<Camera>();
		rightPlayer = GameObject.Find("RightPlayer");
		center = GameObject.Find("Center").GetComponent<PointsCreator>();
		//Cursor.lockState = CursorLockMode.Locked;
		mousePosition = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), -20);
		screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
	}
	
	void Update () {
		if (playerSide == Side.Left) {
			LeftPlayerInput();
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

	void MoveToZone(int zone)
	{
		if (zone == -1)
		{
			currentZone = -1;
			transform.DOMove(new Vector3(0, 0, 0), 0.1f, false);
		}
		if (currentZone != zone)
		{
			currentZone = zone;
			transform.DOMove(center.positions[zone], 0.1f, false);
		}

	}

	//TODO: ask the internet some help to fix this ugly thing
	void LeftPlayerInput() {
		//bool []keys = new bool[] {false, false, false, false};
		//no binary literals >:(
		byte keys = 0;
		if (Input.GetKey(KeyCode.W))
			keys |= 8;	//1000
		if (Input.GetKey(KeyCode.A))
			keys |= 4;	//0100
		if (Input.GetKey(KeyCode.S))
			keys |= 2;	//0010
		if (Input.GetKey(KeyCode.D))
			keys |= 1;	//0001
		switch (keys)
		{
		//Single Arrow
		case 8:	//1000:W
			MoveToZone(0);
			break;
		case 4:	//0100:A
			MoveToZone(6);
			break;
		case 2:	//0010:S
			MoveToZone(4);
			break;
		case 1:	//0001:D
			MoveToZone(2);
			break;

		//Two Arrows
		case 12:	//1100:WA
			MoveToZone(7);
			break;
		case 9:	//1001:WD
			MoveToZone(1);
			break;
		case 6:	//0110:SA
			MoveToZone(5);
			break;
		case 3:	//0011:SD
			MoveToZone(3);
			break;

		case 0:
			MoveToZone(-1);
			break;
		}
	}
}
