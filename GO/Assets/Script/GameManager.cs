using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Net;
using System.Net.NetworkInformation;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour {
    // port and IP adrress changed.. checkvictory brought to start function.

        //Modifying ip address.

    public static GameManager Instance { set; get; }

    public GameObject mainMenu;
    public GameObject serverMenu;
    public GameObject connectMenu;

	public CanvasGroup alertCanvas;
	public float lastAlert;
	private bool alertActive;

    public GameObject serverPrefab;
    public GameObject clientPrefab;

    public InputField nameInput;

	private void Start()
	{	
		alertCanvas.alpha =0;
        Instance = this;
        serverMenu.SetActive(false);
        connectMenu.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }

/*	private void Update(){

		UpdateAlert ();
	}
*/
	public void Alert(string text){
		alertCanvas.GetComponentInChildren<Text>().text = text;
		alertCanvas.alpha =1;
		lastAlert = Time.time;
		alertActive = true;
	}

/*	public void UpdateAlert(){
		if(alertActive){
			if(Time.time - lastAlert > 1.5f){

				alertCanvas.alpha = 1 - ( (Time.time - lastAlert) - 1.5f);

				if(Time.time - lastAlert > 2.5f){
					alertActive = false;
				}                                                   
			}
		}
	}

*/
    public void ConnectButton()
    {
        mainMenu.SetActive(false);
        connectMenu.SetActive(true);
    }

    public static IPAddress GetIPAddress(string hostName) // New - 1
        {
			System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
            var replay = ping.Send(hostName);

            if (replay.Status == IPStatus.Success)
            {
                return replay.Address;
            }
            return null;
         }


    public void HostButton()
    {   
		string hostIP = GetIPAddress (Dns.GetHostName ()).ToString(); // New 1

        try 
	    {	        
		    Server s = Instantiate(serverPrefab).GetComponent<Server>();
            s.Init();

            Client c = Instantiate(clientPrefab).GetComponent<Client>();
            c.clientName = nameInput.text;
            c.isHost = true;

            if(c.clientName == "")
                c.clientName = "Host";



			c.ConnectToServer(hostIP, 9876);   //New 1

            Alert(hostIP);                     //New 1


	    }
	    catch (Exception e)
	    {

		    Debug.Log(e.Message);
	    }

        mainMenu.SetActive(false);

        serverMenu.SetActive(true);

        
    }

    public void ConnectToServerButton()                 //New 1
    {
        string hostAddress = GameObject.Find("HostInput").GetComponent<InputField>().text;
        
        if(hostAddress == "")
			hostAddress = "192.168.0.104";
        else
	    {
            hostAddress = GameObject.Find("HostInput").GetComponent<InputField>().text;
	    }


        try 
	    {	        
		    Client c = Instantiate(clientPrefab).GetComponent<Client>();
            c.clientName = nameInput.text;
            if(c.clientName == "")
                c.clientName = "Client";


            c.ConnectToServer(hostAddress, 9876);
            connectMenu.SetActive(false);
	    }
	    catch (Exception e)
	    {

		    Debug.Log(e.Message);
	    }

    }
    public void BackButton()
    {
        mainMenu.SetActive(true);
        serverMenu.SetActive(false);
        connectMenu.SetActive(false);

        Server s = FindObjectOfType<Server>();
        if(s != null){
            Destroy(s.gameObject);
        }

        Client c = FindObjectOfType<Client>();
        if(c != null){
            Destroy(c.gameObject);
        }
    }
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

    public void HotseatButton(){
        SceneManager.LoadScene("Game");
    }

    public void StartGame(){
        SceneManager.LoadScene("Game");        
    }
		
}
