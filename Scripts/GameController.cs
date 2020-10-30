using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject DeathScreen;
    public GameObject StartMenu;
    public GameObject PauseMenu;
    public bool CanPlay;

    public int Coins;
    public Text CoinsTxt;

    void Start()
    {
    	Coins = 1;
    	CanPlay = false;
    	DeathScreen.SetActive(false);
    	PauseMenu.SetActive(false);
    	StartMenu.SetActive(true);
    }

    private void Update()
    {
    	CoinsTxt.text = ((int)Coins).ToString();
    }

    public void AddReward(){
    	Coins++;
    }

    public void StartGame(){
    	DeathScreen.SetActive(false);
    	StartMenu.SetActive(false);
    	CanPlay = true;
    }

    public void ShowResult(){
    	DeathScreen.SetActive(true);
    }

    public bool ReturnPlay(){
    	return CanPlay;
    }

    public void CancelPlay(){
    	CanPlay = false;
    }

    public void Play(){
    	CanPlay = true;
    }
}
