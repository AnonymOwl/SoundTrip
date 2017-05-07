using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item {
	public enum ItemType {Default, Color, Effect, Shape};

	public int 		Id;
	public string	ItemName;
	public string	IconPath;
	public string	ResourcePath;
	public ItemType	Type;
	public int		Number;

	public Material	color = null;
	public Mesh		mesh = null;
	public GameObject effect = null;

	//TODO: set correct default values
	public Item(int id = 0, string name = "", string icon = "", ItemType t = ItemType.Default, int nb = 0, string resPath = "") {
		Id = id;
		ItemName = name;
		IconPath = icon;
		ResourcePath = resPath;
		Type = t;
		Number = nb;
	
		if (Type == ItemType.Color)
			color = (Material)Resources.Load(ResourcePath);
		else if (Type == ItemType.Shape)
			mesh = (Mesh)Resources.Load(ResourcePath);
		else if (Type == ItemType.Effect)
			effect = (GameObject)Resources.Load(ResourcePath);
	}

	public static ItemType StringToItemType(string s) {
		if (s.ToLower() == "color")
			return ItemType.Color;
		else if (s.ToLower() == "effect")
			return ItemType.Effect;
		else if (s.ToLower() == "shape")
			return ItemType.Shape;
		else
			return ItemType.Default;
	}

	public string GetName() {
		if (color != null)
			return color.name;
		else if (effect != null)
			return effect.name;
		else if (mesh != null)
			return mesh.name;
		else
			return "";
	}
}
