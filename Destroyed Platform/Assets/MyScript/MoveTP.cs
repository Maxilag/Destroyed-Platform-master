using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class MoveTP : MonoBehaviour {
	public Transform objectReference;

	private enum Etat {Look, AnalyseCommande, fadeOut, endFadeOut, teleportation, fadeIn, endFadeIn};
	Etat etat;
	private Vector3 centreCamera;
	private Vector3 nouvellePosition;
	private double chrono;
	private double chronoFadeOut;
	private double chronoFadeIn;
	private string tagTouchee;
	private OVRScreenFadeCenter fadeout;
	private OVRScreenFadeIn fadein;

	private Vector3 anciennePositionCube;
	public Camera cam;
	public OVRCameraRig cameraOVR;
	public GameObject cube;

	private float dist;
	/// <summary>
	/// Starts the fade in
	/// </summary>


	void Start() {
		centreCamera = new Vector3 (Screen.width / 2.0f, Screen.height / 2.0f, cameraOVR.transform.forward.z);
		etat = Etat.Look;
	}

	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		dist = Vector3.Distance (anciennePositionCube, cube.transform.position);

		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
			nouvellePosition = hit.point;
			tagTouchee = hit.transform.tag;
			cube.transform.position = nouvellePosition;
		}

		if (etat == Etat.Look) {
			if (dist < 2.0f) {
				chrono += Time.deltaTime;
				Debug.Log (chrono);
			} else {
				chrono = 0;
				anciennePositionCube = nouvellePosition;
				Debug.Log (chrono);
			}

			if (chrono > 2) {
				chrono = 0;
				etat = Etat.AnalyseCommande;
			}
		} else if (etat == Etat.AnalyseCommande) {
			if (tagTouchee == "terrain") {
				etat = Etat.fadeOut;
			}
			if (tagTouchee == "obstacle") {
				etat = Etat.Look;
			}
		}else if (etat == Etat.fadeOut) {
			cam.GetComponent<OVRScreenFadeCenter> ().enabled = true;
			chronoFadeOut += Time.deltaTime;
			if (chronoFadeOut > cam.GetComponent<OVRScreenFadeCenter> ().fadeTime) {
				cam.GetComponent<OVRScreenFadeCenter> ().enabled = false;
				etat = Etat.teleportation;
				chronoFadeOut = 0;
			}
		} else if (etat == Etat.teleportation) {
			this.transform.position = new Vector3 (nouvellePosition.x, nouvellePosition.y + 0.6f, nouvellePosition.z);
			cameraOVR.transform.position = new Vector3 (nouvellePosition.x, nouvellePosition.y + 1.0f, nouvellePosition.z);
			etat = Etat.fadeIn;
		} else if (etat == Etat.fadeIn) {
			cam.GetComponent<OVRScreenFadeIn> ().enabled = true;
			chronoFadeIn += Time.deltaTime;
			if (chronoFadeIn > cam.GetComponent<OVRScreenFadeIn> ().fadeTime) {
				cam.GetComponent<OVRScreenFadeIn> ().enabled = false;
				chronoFadeIn = 0;
				etat = Etat.Look;
			}
		}
	}


}