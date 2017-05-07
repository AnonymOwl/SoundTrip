using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class DBData {

	private IDbConnection dbconn;

	public DBData () {
		dbconn = DBConnect("/Resources/database.db");
	}
	
	private IDbConnection DBConnect (string path) {
		string conn = "URI=file:" + Application.dataPath + path;
		IDbConnection dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open();
		return (dbconn);
	}

	public List<Item>	GetItems(Item.ItemType type = Item.ItemType.Default) {
		string query = "SELECT ID, Name, IconPath, Type, Number, ResourcePath FROM Items";
		List<Item> items = new List<Item>();

		if (type != Item.ItemType.Default)
			query = query + " WHERE Type IS '" + type.ToString() + "'";

		IDbCommand dbcmd = dbconn.CreateCommand();
		dbcmd.CommandText = query;
		IDataReader reader = dbcmd.ExecuteReader();

		while (reader.Read())
		{
			items.Add(new Item(
				reader.GetInt32(0),
				reader.GetString(1),
				reader.GetString(2),
				Item.StringToItemType(reader.GetString(3)),
				reader.GetInt32(4),
				reader.GetString(5))
			);
		}

		reader.Close();
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;		
		return (items);
	}

	public void	SetItems(List<Item> itms) {
		string query;
		//TODO: make it in one query
		IDbCommand dbcmd = dbconn.CreateCommand();
		foreach (Item itm in itms) {
			query = "UPDATE Items SET Number = " + itm.Number + " WHERE Name = '" + itm.ItemName + "'";
			dbcmd.CommandText = query;
			dbcmd.ExecuteScalar();
		}
	}

	public void CloseConnection() {
		dbconn.Close();
		dbconn = null;

	}
}
