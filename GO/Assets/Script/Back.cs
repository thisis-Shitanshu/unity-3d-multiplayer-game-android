using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Net;
using System.Net.NetworkInformation;
using UnityEngine.Networking;

public class Back : MonoBehaviour {

	public void BackButtonToMenu()
	{
		Server s = FindObjectOfType<Server>();
		if(s != null){
			Destroy(s.gameObject);
		}

		Client c = FindObjectOfType<Client>();
		if(c != null){
			Destroy(c.gameObject);
		}

		SceneManager.LoadScene("Menu");
	}
}
