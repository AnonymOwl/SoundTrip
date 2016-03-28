using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Camera	leftCamera;
	public Camera	rightCamera;
	private bool	vertical = false;

	void Start () {
	
	}
	
	void Update () {
		if (Application.platform == RuntimePlatform.Android) {
			if (vertical == false && Screen.orientation == ScreenOrientation.Portrait) {
				leftCamera.rect = new Rect(0f, 0.5f, 1f, 0.5f);
				rightCamera.rect = new Rect(0f, 0f, 1f, 0.5f);
				vertical = true;
			}
			else if (vertical == true && Screen.orientation == ScreenOrientation.Landscape) {
				leftCamera.rect = new Rect(0f, 0f, 0.5f, 1f);
				rightCamera.rect = new Rect(0.5f, 0f, 0.5f, 1f);
				vertical = false;
			}
		}
	}
}
