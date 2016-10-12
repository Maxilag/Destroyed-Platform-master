using UnityEngine;
using System.Collections;

public class FogOfWarPlayer : MonoBehaviour {
	public Transform FogofWarPlane;
	public int Number;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 screenPos = Camera.main.WorldToScreenPoint (transform.position);
		Ray rayToPlayerPos = Camera.main.ScreenPointToRay(screenPos);
		RaycastHit hit;
		if (Physics.Raycast (rayToPlayerPos, out hit, 1000)) {
			FogofWarPlane.GetComponent<Renderer> ().material.SetVector ("_Player" + Number.ToString () + "_Pos", hit.point);
		}
	
	}
}
