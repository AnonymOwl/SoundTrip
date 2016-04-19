using UnityEngine;
using System.Collections;
using System.IO;

[System.Serializable]
public class ItemValues {
	public KeyCode		code;
	public GameObject	prefab;
}

public class MoveManager : MonoBehaviour {

	public int				speed;
	public string			musicPath;
	public ItemValues[]		values;

	private AudioSource		music;
	private bool			musicOn = false;
	private StreamReader	sr;

	void Start () {

		music = GameObject.Find("AudioSource").GetComponent<AudioSource>();
		if (File.Exists("Assets/SongFiles/" + musicPath))
			sr = new StreamReader(File.OpenRead("Assets/SongFiles/" + musicPath));
		else {
			print(musicPath + ": no such song file.");
			return;
		}
		PlacePoints();
	}

	void Update () {
		if (transform.position.z <= 0 && !musicOn) {
			music.Play();
			musicOn = true;
		}
		if (!musicOn)
			transform.Translate(0, 0, Time.deltaTime * -speed);
		else {
			transform.position = new Vector3(0, 0, -music.time * speed);
		}
	}

	private void PlacePoints() {
		string 		line;
		string 		elem;
		float 		pos;
		GameObject	thing;

		while ((line = sr.ReadLine()) != null) {
			pos = float.Parse(line.Substring(0, line.IndexOf(":")));
			elem = line.Substring(line.IndexOf(":") + 1, 1);
			foreach (ItemValues item in values) {
				if (item.code.ToString() == elem) {
					thing = (GameObject)Instantiate(item.prefab);
					thing.transform.SetParent(this.transform);
					thing.transform.localPosition = new Vector3(0, 0, pos * speed);
				}
			}
		}
	}
}
