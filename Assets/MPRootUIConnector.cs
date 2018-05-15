using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPRootUIConnector : MonoBehaviour {

	public vp_UIManager RootUIManager;
	public vp_UFPSMobileDemo RootUIController;

	public GameObject AmmoLabel = null;			// a gameobject that has a TextMesh component for ammo label
	public GameObject HealthLabel = null;		// a gameobject that has a TextMesh component for Health label
	public GameObject HintsLabel = null;		// a gameobject that has a TextMesh component for Hints label


	private GameObject localPlayer = null;
	private bool isRefreshed = false;

	// Use this for initialization
	void Start () {
		Debug.Log ("SETTING");



		localPlayer = GetLocalPlayer ();
		if (localPlayer == null) {
			Debug.Log ("START LOCAL PLAYER NOT FOUND!");
		} else {
			doRefresh ();
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (localPlayer == null) {
			Debug.Log ("UPDTAE LOCAL PLAYER NOT FOUND!");

			localPlayer = GetLocalPlayer ();
		} else {
			doRefresh ();
		}
	}

	private GameObject GetLocalPlayer()
	{
		GameObject[] objects = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject obj in objects)
		{
			if (obj.layer.Equals(LayerMask.NameToLayer("LocalPlayer"))) // localplayer
			{
				return obj;
			}
		}
		return null;
	}

	private void doRefresh() {
		if (isRefreshed.Equals(true)) {
			return;
		}
		RootUIManager.Player = localPlayer.GetComponent<vp_FPPlayerEventHandler> ();


		RootUIController.SimpleHUD = localPlayer.GetComponent<vp_SimpleHUDMobile> ();

		RootUIController.SimpleHUD.AmmoLabel = AmmoLabel;
		RootUIController.SimpleHUD.HealthLabel = HealthLabel;
		RootUIController.SimpleHUD.HintsLabel = HintsLabel;



		RootUIController.SimpleHUD.SetLabels ();

		RootUIManager.enabled = true;
		RootUIManager.ForceUIRefresh ();

		Debug.Log ("DONE");
		isRefreshed = true;
	}
}
