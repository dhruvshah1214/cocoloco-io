using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPRootUIConnector : MonoBehaviour {

	public vp_UIManager RootUIManager;
	public vp_UFPSMobileDemo RootUIController;

	public GameObject AmmoLabel = null;			// a gameobject that has a TextMesh component for ammo label
	public GameObject HealthLabel = null;		// a gameobject that has a TextMesh component for Health label
	public GameObject HintsLabel = null;		// a gameobject that has a TextMesh component for Hints label

	// Use this for initialization
	void Start () {
		Debug.Log ("SETTING");

		GameObject gc = GameObject.FindGameObjectWithTag ("Player");
		RootUIManager.Player = gc.GetComponent<vp_FPPlayerEventHandler> ();


		RootUIController.SimpleHUD = gc.GetComponent<vp_SimpleHUDMobile> ();

		RootUIController.SimpleHUD.AmmoLabel = AmmoLabel;
		RootUIController.SimpleHUD.HealthLabel = HealthLabel;
		RootUIController.SimpleHUD.HintsLabel = HintsLabel;

		RootUIManager.ForceUIRefresh ();
		RootUIManager.enabled = true;

		RootUIController.SimpleHUD.SetLabels ();


		Debug.Log ("DONE");

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
