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
			if (File.Exists(Application.dataPath + "/Resources/SongFiles/" + filename))
				File.Delete(Application.dataPath + "/Resources/SongFiles/" + filename);
			sw = File.CreateText(Application.dataPath + "/Resources/SongFiles/" + filename);
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
