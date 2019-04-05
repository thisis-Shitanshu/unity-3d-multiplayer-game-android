using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Net;
using System.Net.NetworkInformation;
using UnityEngine.Networking;

public class GmGame : MonoBehaviour {


	public CanvasGroup ruleCanvas;

	public GameObject mainMenu;
	public GameObject serverMenu;
	public GameObject connectMenu;

	private bool ruleFlag = false;

	private void Start(){
		ruleCanvas.alpha = 0;
	}
		
	private void Update(){
	//	ruleFlag = true;
	}

	public void RuleButton(){

		if (ruleFlag == true) {
			ruleFlag = false;
			ruleCanvas.alpha = 1;
			mainMenu.SetActive (false);
			serverMenu.SetActive (false);
			connectMenu.SetActive (false);

		} 
		else {
			ruleFlag = true;
			mainMenu.SetActive (true);
			ruleCanvas.alpha = 0;
		}
	}
}
