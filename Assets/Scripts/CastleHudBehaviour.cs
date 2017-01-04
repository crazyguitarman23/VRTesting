using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleHudBehaviour : MonoBehaviour 
{
	TextMesh textMesh;
	CastleBehaviour castle;

	// Use this for initialization
	void Start () 
	{
		textMesh = GetComponent<TextMesh> ();
		castle = GameObject.FindWithTag ("Castle").GetComponent<CastleBehaviour> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		textMesh.transform.LookAt (Camera.main.transform.position);

		if (castle)
		{
			textMesh.text = "HP: " + castle.GetHitPoints ();
		}
		else
		{
			textMesh.text = "THANKS OBAMA";
		}
	}
}
