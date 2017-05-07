using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

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
	//private StreamReader	sr;
	private StringReader	sr;
	private List<Vector2>	circlePositions;

	void Start () {

		music = GameObject.Find("AudioSource").GetComponent<AudioSource>();
		/* Editor version
		if (File.Exists(Application.dataPath + "/Resources/SongFiles/" + musicPath)) {
			sr = new StreamReader(File.OpenRead(Application.dataPath + "/Resources/SongFiles/" + musicPath));
		}
		else {
			print(Application.dataPath + "/Resources/SongFiles/" + musicPath + ": no such song file.");
			return;
		}
		*/
		TextAsset txt = (TextAsset)Resources.Load("SongFiles/" + musicPath.Substring(0, musicPath.Length - 4), typeof(TextAsset));
		sr = new StringReader(txt.text);
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

	public void PlacePoints() {
		string 		line;
		string 		elem;
		float 		pos;
		int			totalPos;
		int 		currentPos;
		GameObject	thing;

		circlePositions = GameObject.Find("Center").GetComponent<PointsCreator>().positions;
		totalPos = circlePositions.Count - 1;
		currentPos = Random.Range(0, totalPos + 1);
		while ((line = sr.ReadLine()) != null) {
			pos = float.Parse(line.Substring(0, line.IndexOf(":")));
			elem = line.Substring(line.IndexOf(":") + 1, 1);
			foreach (ItemValues item in values) {
				if (item.code.ToString() == elem) {
					thing = (GameObject)Instantiate(item.prefab);
					thing.transform.SetParent(this.transform);
					thing.transform.localPosition = new Vector3(circlePositions[currentPos].x, circlePositions[currentPos].y, pos * speed);
					currentPos = ChangePosition(currentPos, totalPos);
				}
			}
		}
	}

	private int ChangePosition(int current, int total) {
		int multiplier = Random.Range(0, 2);

		if (multiplier == 0)
			multiplier = -1;
		else
			multiplier = 1;

		current += multiplier;
		if (current < 0)
			current = total;
		else if (current > total)
			current = 0;
		return current;
	}
}
