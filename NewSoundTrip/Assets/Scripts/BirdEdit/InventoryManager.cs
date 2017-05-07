using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

	public ItemSelector activeItem = null;
	public List<ItemSelector> itemSelectorList;
	public GameObject	itemPrefab;
	public BirdManager bird;

	private bool		colorOn = true;
	private DBData 		db;

	//TODO: Close db connection somewhere
	void Awake() {
		db = new DBData();
		InitInventory();
		bird = GameObject.Find("PlayerBird").GetComponent<BirdManager>();
	}

	void Start () {
		bird.SetEditMode(true);
		bird.GetAllFeathers();
	}
	
	void Update () {
		//cheat!
		if (Input.GetKeyDown(KeyCode.R))
			itemSelectorList.Find(i => i.item.ItemName == "RedDye").UpdateNb(1);
		if (Input.GetKeyDown(KeyCode.G))
			itemSelectorList.Find(i => i.item.ItemName == "GreenDye").UpdateNb(1);
		if (Input.GetKeyDown(KeyCode.B))
			itemSelectorList.Find(i => i.item.ItemName == "BlueDye").UpdateNb(1);
	}

	void InitInventory() {
		List<Item> items = new List<Item>();
		items = db.GetItems();
		itemSelectorList = new List<ItemSelector>();
		Transform itemParent = GameObject.Find("ItemsLeft").transform;
		int x = 1;

		GameObject g;
		foreach (Item itm in items) {
			g = (GameObject)Instantiate(itemPrefab, itemParent.Find("Item" + x.ToString()));
			g.GetComponent<ItemSelector>().item = itm;
			g.name = itm.ItemName;
			g.GetComponent<Image>().sprite = Resources.Load<Sprite>(itm.IconPath);
			g.GetComponent<ItemSelector>().SetPosition();
			g.GetComponent<ItemSelector>().UpdateNb(0);
			x++;
			itemSelectorList.Add(g.GetComponent<ItemSelector>());
		}

	}

	public void ColorPick(FeatherManager obj) {
		if (Input.GetMouseButtonDown(0)) {
			if (activeItem != null && activeItem.item.GetName() == obj.GetName(activeItem.item.Type))
				colorOn = false;
			else
				colorOn = true;
		}

		if (Input.GetMouseButton(0) && !colorOn && obj.GetName(activeItem.item.Type) == activeItem.item.GetName()) {
			obj.SetModifier(activeItem.item.Type, true);
			activeItem.UpdateNb(1);
		}
		if (Input.GetMouseButton(0) && activeItem != null && colorOn) {
			if (activeItem != null && activeItem.item.Number > 0 && activeItem.item.GetName() != obj.GetName(activeItem.item.Type)) {
				foreach (ItemSelector selector in itemSelectorList) {
					if (selector.item.GetName() == obj.GetName(activeItem.item.Type))
						selector.UpdateNb(1);
				}
				obj.SetModifier(activeItem.item.Type);
				activeItem.UpdateNb(-1);
			}
		}
	}

	//TODO:better query string (too much data)
	public void getInventory() {

		DBData db = new DBData();
		List<Item> items = new List<Item>();
		items = db.GetItems();
		db.CloseConnection();

		foreach (Item itm in items) {
			ItemSelector itms = itemSelectorList.Find(i => (i.item.ItemName == itm.ItemName));
			if (itms != null) {
				itms.SetTotal(itm.Number);
			}
		}

		bird.GetAllFeathers();
	}

	public void setInventory() {
		List<Item> itms = new List<Item>();
		//TODO: isn't there a better way?
		foreach (ItemSelector its in itemSelectorList) {
			itms.Add(its.item);
		}
		db.SetItems(itms);

		foreach (FeatherManager feather in bird.GetFeatherManagers()) {
			if (feather.GetName(Item.ItemType.Color) != "")
				PlayerPrefs.SetString(feather.transform.parent.gameObject.name + "C", feather.color.name);
			if (feather.GetName(Item.ItemType.Effect) != "")
				PlayerPrefs.SetString(feather.transform.parent.gameObject.name + "E", feather.effect.name);
			else
				PlayerPrefs.DeleteKey(feather.transform.parent.gameObject.name + "E");
			if (feather.GetName(Item.ItemType.Shape) != "")
				PlayerPrefs.SetString(feather.transform.parent.gameObject.name + "S", feather.shape.name);
		}
	}
}
