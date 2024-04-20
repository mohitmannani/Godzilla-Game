﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MKGameManager : MonoBehaviour {

	[HideInInspector] 
	static public MKGameManager instance;

	[Tooltip("The number of starting lives for the player.")]
	public int startingLives;
	[Tooltip("The name of main menu scene.")]
	public string mainMenu;
	[Tooltip("The name of the game levels, in order.")]
	public string[] levels; 

	[HideInInspector] 
	public int currentScene;
	[HideInInspector] 
	public int playerLives;

	void Awake()
	{
		if ( instance == null ) {
			instance = this;
			instance.InitData();
			DontDestroyOnLoad (this);
		} else {
			Destroy ( gameObject );
		}
	}

	
	void InitData() {
		DontDestroyOnLoad(gameObject);

		ResetGame ();
	}

	public void ResetGame() {
		currentScene = 0;
		playerLives = startingLives;
	}

	public void StartGame() {
        if (currentScene < levels.Length)
            SceneManager.LoadScene(levels[currentScene]);
	}
	
	public void NextScene() {
		currentScene++;
		if (currentScene < levels.Length)
            SceneManager.LoadScene(levels[currentScene]);
        else
            SceneManager.LoadScene(mainMenu);
    }

    public void MainScene() {
        SceneManager.LoadScene(mainMenu);
    }

}
