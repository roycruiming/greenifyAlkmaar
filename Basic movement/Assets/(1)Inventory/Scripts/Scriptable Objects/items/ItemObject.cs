using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{

	//item storable in inventory
	Storable,
	//Cant be stored but can be picked up for mana/hp/xp/etc
	Consumable

}




public abstract class ItemObject : ScriptableObject
{
	GameObject prefab;
	public ItemType type;
	public new string name;
	[TextArea(15, 20)]
	public string description;
	public int permittedAmount;
	public Sprite icon;

	public GameObject getprefab() {
		return prefab; 
	}

	public void setprefab(GameObject _prefab) {
		this.prefab = _prefab; 
	
	}
}



