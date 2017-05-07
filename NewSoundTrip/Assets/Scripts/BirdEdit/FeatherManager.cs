using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherManager : MonoBehaviour {

	public Material		color = null;
	public Mesh 		shape = null;
	public GameObject 	effect = null;
	public bool editMode = false;

	private InventoryManager	manager;

	void Start () {
		if (GameObject.Find("InventoryManager") != null)
			manager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
	}
	
	void Update () {
		
	}

	public void SetModifier(Item.ItemType type, bool isDefault = false) {
		if (type == Item.ItemType.Color)
			SetColor(manager.activeItem.item.color, isDefault);
		else if (type == Item.ItemType.Effect)
			SetEffect(manager.activeItem.item.effect, isDefault);
		else if (type == Item.ItemType.Shape)
			SetShape(manager.activeItem.item.mesh, isDefault);
	}

	public void SetColor(Material c, bool isDefault = false) {
		if (!isDefault)
			color = c;
		else
			color = manager.bird.defaultMat;
		GetComponent<MeshRenderer>().material = color;
	}

	public void SetEffect(GameObject e, bool isDefault = false) {
		if (!isDefault) {
			effect = e;
			if (transform.parent.childCount > 1)
				GameObject.Destroy(transform.parent.GetChild(1).gameObject);
			GameObject eff = GameObject.Instantiate(e, transform.parent);
			eff.transform.localPosition = Vector3.zero;
		} else {
			effect = null;
			if (transform.parent.childCount > 1)
				GameObject.Destroy(transform.parent.GetChild(1).gameObject);
		}
	}

	public void SetShape(Mesh m, bool isDefault = false) {
		if (!isDefault)
			shape = m;
		else
			shape = manager.bird.defaultShape;
		GetComponent<MeshFilter>().mesh = m;
	}

	public string GetName(Item.ItemType t) {
		if (t == Item.ItemType.Color && color != null && color.name != manager.bird.defaultMat.name)
			return color.name;
		else if (t == Item.ItemType.Effect && effect != null)
			return effect.name;
		else if (t == Item.ItemType.Shape && shape != null)
			return shape.name;
		else
			return "";
	}

	void OnMouseOver() {
		if (editMode) {
			manager.ColorPick(this);
		}
	}
}
