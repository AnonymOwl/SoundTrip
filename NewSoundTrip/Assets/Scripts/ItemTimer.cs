using UnityEngine;
using System.Collections;
using System.IO;

public class ItemTimer : MonoBehaviour {

	public bool				isOn;
	public AudioSource		source;
	public ItemValues[]		values;
	public string			filename;

	private string			input;
	private StreamWriter	sw;

	void Start () {
		if (isOn) {
			print("item timer start");
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
					sw.WriteLine(source.time + ":" + v.code.ToString());
				}
			}
		}
	}

	void OnApplicationQuit() {
		if (isOn) {
			sw.Close();
			print("item timer end");
		}
	}
		
}
