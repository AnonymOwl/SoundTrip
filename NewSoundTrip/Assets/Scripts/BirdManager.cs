using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour {

	private List<Material>		colorList;
	private List<GameObject>	effectList;
	private List<Mesh> 			shapeList;
	public Material			defaultMat;
	public Mesh				defaultShape;

	void Awake () {
		//TODO: better
		colorList = new List<Material>();
		effectList = new List<GameObject>();
		shapeList = new List<Mesh>();

		foreach (Object o in Resources.LoadAll("Items/Colors")) {
			if (o.name.Contains("Default"))
				defaultMat = (Material)o;
			colorList.Add((Material)o);
		}
	
		foreach (Object o in Resources.LoadAll("Items/Effects")) {
			effectList.Add((GameObject)o);
		}

		//TODO: right default
		foreach (Object o in Resources.LoadAll("Items/Feathers")) {
			GameObject m = (GameObject)o;
			if (o.name.Contains("Default"))
				defaultShape = m.GetComponent<MeshFilter>().sharedMesh;
			shapeList.Add(m.GetComponent<MeshFilter>().sharedMesh);
		}
	}

	void Start() {
		GetAllFeathers();			
	}
	
	void Update () {
		
	}

	public void SetEditMode(bool editMode) {
		foreach (FeatherManager f in GetFeatherManagers())
			f.editMode = editMode;
	}

	//TODO: do that better and fuck unity
	public Transform FindFeathers(Transform o, string n) {
		if (o.childCount == 0)
			return null;
		for (int i = 0; i < o.childCount; i++) {
			if (o.GetChild(i).name == n)
				return o.GetChild(i);
			else {
				Transform c = FindFeathers(o.GetChild(i), n);
				if (c != null)
					return (c);
			}
		}
		return null;
	}

	public FeatherManager[]	GetFeatherManagers() {
		return GetComponentsInChildren<FeatherManager>();
	}

	public void GetAllFeathers() {

		GetFeathers("FeatherRL", 16);
		GetFeathers("FeatherLL", 16);
		GetFeathers("FeatherRS", 11);
		GetFeathers("FeatherLS", 11);
		GetFeathers("FeatherT", 7);
	}

	private void GetFeathers(string featherName, int featherNb) {
		int i;
		Transform f;
		for (i = 1; i <= featherNb; i++) {
			if (PlayerPrefs.HasKey(featherName + i.ToString() + "C")) {
				f = FindFeathers(transform, featherName + i.ToString());
				foreach (Material m in colorList) {
					if (m.name == PlayerPrefs.GetString(featherName + i.ToString() + "C"))
						f.GetChild(0).GetComponent<FeatherManager>().SetColor(m);
				}

				if (f.childCount > 1) {
					GameObject.Destroy(f.GetChild(1).gameObject);
				}
				if (PlayerPrefs.HasKey(featherName + i.ToString() + "E")) {
					f = FindFeathers(transform, featherName + i.ToString());
					foreach (GameObject m in effectList) {
						if (m.name == PlayerPrefs.GetString(featherName + i.ToString() + "E"))
							f.GetChild(0).GetComponent<FeatherManager>().SetEffect(m);
					}
				} else {
					f = FindFeathers(transform, featherName + i.ToString());
					f.GetChild(0).GetComponent<FeatherManager>().SetEffect(null, true);
				}

				if (PlayerPrefs.HasKey(featherName + i.ToString() + "S")) {
					f = FindFeathers(transform, featherName + i.ToString());
					foreach (Mesh m in shapeList) {
						if (m.name == PlayerPrefs.GetString(featherName + i.ToString() + "S"))
							f.GetChild(0).GetComponent<FeatherManager>().SetShape(m);
					}
				}
			}
		}
	}
}
