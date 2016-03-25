using UnityEngine;
using System.Collections;
using System.IO;

//TODO: put that in the thing that actually places the elements, you only need key codes here
[System.Serializable]
public class ItemValues {
	public KeyCode		code;
	public GameObject	prefab;
}

public class ItemTimer : MonoBehaviour {

	public bool				isOn;
	public AudioSource		source;
	public ItemValues[]		values;
	public string			filename;

	private string			input;
	private StreamWriter	sw;

	void Start () {
		if (isOn) {
			if (File.Exists("Assets/SongFiles/" + filename))
				File.Delete("Assets/SongFiles/" + filename);
			sw = File.CreateText("Assets/SongFiles/" + filename);
		}
	}
	
	void Update () {
		if (isOn) {
			input = Input.inputString;
			if (input != "") {
				foreach (ItemValues v in values) {
					if (input.ToLower()[0] == v.code.ToString().ToLower()[0]) {
						sw.WriteLine(source.time + ":" + v.code.ToString());
					}
				}
			}
		}
	}

	void OnApplicationQuit() {
		if (isOn)
			sw.Close();
	}
		
}
