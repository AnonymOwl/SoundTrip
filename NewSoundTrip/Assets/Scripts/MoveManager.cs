using UnityEngine;
using System.Collections;

public class MoveManager : MonoBehaviour {

	public int			speed;

	private AudioSource	music;
	private bool		musicOn = false;

	void Start () {
		music = GameObject.Find("AudioSource").GetComponent<AudioSource>();
	}
	
	void Update () {
		if (transform.position.z <= 0 && !musicOn) {
			music.Play();
			musicOn = true;
		}
		if (!musicOn)
			transform.Translate(0, 0, -speed * Time.deltaTime);
		else {
			transform.position = new Vector3(0, 0, -music.time * speed);
		}
	}
}
