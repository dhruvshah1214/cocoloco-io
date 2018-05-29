using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocolocoWeaponManager : MonoBehaviour {
	private GameObject localPlayer = null;
	private vp_PlayerInventory m_Inventory = null;

	bool done = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (done)
			return;
		
		localPlayer = MPRootUIConnector.GetLocalPlayer ();

		if (localPlayer != null) {
			m_Inventory = localPlayer.GetComponent<vp_PlayerInventory> ();
			int weaponNumber = CocolocoMainMenuGUI.m_WeaponNumber;
			string weaponName = "CL" + CocolocoMainMenuGUI.m_Weapons [weaponNumber];
			done = addWeaponToInventory (weaponName);
			Debug.Log ("Weapon set!");
		}
		
	}

	bool addWeaponToInventory(string weaponName) {
		for (int v = 0; v < m_Inventory.m_ItemCapInstances.Count; v++) {

			if (m_Inventory.m_ItemCapInstances [v].Type.name == weaponName)
			if (localPlayer.GetComponent<vp_FPPlayerEventHandler>().AddItem.Try(new object[] { m_Inventory.m_ItemCapInstances [v].Type })) {
				localPlayer.GetComponent<vp_FPPlayerEventHandler> ().SetWeapon.TryStart (v + 1);

				return true;
			} else {
				return false;
			}
		}
		Debug.LogError ("Couldn't find requested weapon to add to inventory");
		return false;
	}
}
