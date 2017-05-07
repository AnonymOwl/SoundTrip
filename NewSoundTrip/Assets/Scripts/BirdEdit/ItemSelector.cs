using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelector : MonoBehaviour {


	public Item	item;

	public Material defaultColor;
	public Mesh		defaultMeshShort;
	public Mesh		defaultMeshLong;

	private InventoryManager manager;
	private bool isSelected = false;
	private bool selectHappened = false;
	private GameObject outline;
	private Text	NbDisplay;

	void Awake() {
		NbDisplay = transform.Find("Nb").GetComponent<Text>();
	}

	void Start () {
		manager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
		NbDisplay.text = item.Number.ToString();
		outline = transform.Find("Outline").gameObject;
		outline.SetActive(false);
		defaultColor = (Material)Resources.Load("items/Colors/DefaultFeather");
		defaultMeshLong = (Mesh)Resources.Load("Items/Feathers/DefaultFeatherLong");
		defaultMeshShort = (Mesh)Resources.Load("Items/Feathers/DefaultFeatherShort");
	}
	
	void Update () {
		if (isSelected && !selectHappened) {
			outline.SetActive(true);
			if (manager.activeItem != null && manager.activeItem.gameObject != gameObject)
				manager.activeItem.ToggleSelect();
			manager.activeItem = this;
			selectHappened = true;
		}
		if (!isSelected && selectHappened) {
			if (manager.activeItem.gameObject == gameObject)
				manager.activeItem = null;
			outline.SetActive(false);
			selectHappened = false;
		}
	}

	public void ToggleSelect() {
		if (isSelected)
			isSelected = false;
		else
			isSelected = true;
	}

	public void UpdateNb(int modifier) {
		item.Number += modifier;
		NbDisplay.text = item.Number.ToString();
	}

	public void SetTotal(int n) {
		item.Number = n;
		NbDisplay.text = item.Number.ToString();
	}

	public void SetPosition() {
		RectTransform t = GetComponent<RectTransform>();
		t.offsetMin = new Vector2(0, 0);
		t.offsetMax = new Vector2(0, 0);
		t.anchoredPosition = new Vector2(0, 0);
	}
}
