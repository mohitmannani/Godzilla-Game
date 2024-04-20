using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum SpawnMethod {
	SpawnFurthest,
	SpawnRecent,
	RandomSpawn
}

public class MKSceneManager : MonoBehaviour {
	public static MKSceneManager instance;

	[Tooltip("Reference to the player in the scene.")]
	public MKPlayer player;
	[Tooltip("This is the Y value that represents the point where the player dies if they fall past it.")]
	public float floor;
	[Tooltip("Indicates which spawn point to use.")]
	public SpawnMethod spawnMethod;
	[Tooltip("A list of points for the player to spawn at.")]
	public List<GameObject> spawnPoints;
	[Tooltip("The number of seconds to pause to show text when a player falls off.")]
	public float deathPause;
	[Tooltip("The text to display when a player falls off but has lives left.")]
	public string fallText;
	[Tooltip("The text to display when a player falls off and has no lives left.")]
	public string gameOverText;
	[Tooltip("The text to display when a player crosses the finish line.")]
	public string levelClearedText;

	[HideInInspector] 
	public bool inputLocked = true;
	[HideInInspector] 
	public bool mainMenu = false;

	private int currSpawnPoint;
	private System.Random random = new System.Random();
	private bool done;

    private Text livesLabel;
    private Text infoLabel;
    private Text otherButtonText;
    private GameObject otherButton;
    private GameObject menuButton;

    void Awake()
	{
		if (instance == null )
		{
			instance = this;
		}
	}


	// Use this for initialization
	void Start () {
		currSpawnPoint = 0;
		if (spawnPoints.Count > 0) {
			Respawn();
		} else {
			Debug.LogError("You have not setup any spawn points for this scene!");
		}

        livesLabel = GameObject.Find("smLivesLabel").GetComponent<Text>();
        infoLabel = GameObject.Find("smInfoLabel").GetComponent<Text>();
        otherButtonText = GameObject.Find("smButtonText").GetComponent<Text>();
        otherButton = GameObject.Find("smOtherButton");
        menuButton = GameObject.Find("smMainMenuButton");
        otherButton.SetActive(false);
        menuButton.SetActive(false);

    }

    // Update is called once per frame
    void Update () {
        livesLabel.text = "";
        if (MKGameManager.instance != null && !mainMenu)
            livesLabel.text = "Lives: " + MKGameManager.instance.playerLives;
    }

    public void OnButtonPress()
    {
        MKGameManager.instance.GetComponent<AudioSource>().Play();
        done = true;
    }

    public void OnMenuButtonPress()
    {
        MKGameManager.instance.GetComponent<AudioSource>().Play();
        if (MKGameManager.instance.playerLives == 0)
            MKGameManager.instance.ResetGame();
        MKGameManager.instance.MainScene();
    }

	void Respawn() {
		if (spawnMethod == SpawnMethod.RandomSpawn)
			currSpawnPoint = random.Next(0, spawnPoints.Count);

		ResetScene(spawnPoints[currSpawnPoint]);
	}

	public void ResetScene(GameObject target) {
		if (player == null)
			return;

		float offset = player.GetComponent<Collider>().bounds.extents.y - target.GetComponent<Collider>().bounds.extents.y + 0.1f;
		player.Reset(target, new Vector3(0, offset, 0));
		IPCheckpointManager.RestoreCheckpointState();
		StartCoroutine("StartScene");
	}

	public void HandleCheckpointPass(GameObject checkpoint) {
		int spawnIndex = spawnPoints.IndexOf(checkpoint);
		if ((spawnMethod == SpawnMethod.SpawnRecent) || (spawnMethod == SpawnMethod.SpawnFurthest && spawnIndex > currSpawnPoint) ) {
			currSpawnPoint = spawnIndex;
			IPCheckpointManager.SaveCheckpointState();
		}

	}

	public void HandlePlayerDeath() {
		StartCoroutine("EndScene");
	}

	public void HandleGameOver() {
		StartCoroutine("EndGame");
	}

	public void HandleLevelCleared() {
		StartCoroutine("EndLevel");
	}

	IEnumerator EndScene() {
		inputLocked = true;
        infoLabel.text = fallText;
		yield return new WaitForSeconds(deathPause);
        infoLabel.text = "";
        Respawn();
	}

	IEnumerator EndGame() {
		inputLocked = true;
        infoLabel.text = gameOverText;

        menuButton.SetActive(true);
		done = false;
		while (!done) {
			yield return null;
		}

        infoLabel.text = "";
    }

    IEnumerator EndLevel() {
		inputLocked = true;
		infoLabel.text = levelClearedText;
		player.ClearLevel();

        menuButton.SetActive(true);
        if (MKGameManager.instance.currentScene < MKGameManager.instance.levels.Length-1) {
            otherButtonText.text = "Next Level";
            otherButton.SetActive(true);
		} else {
			MKGameManager.instance.ResetGame();
		}

		done = false;
		while (!done) {
			yield return null;
		}

        infoLabel.text = "";
        MKGameManager.instance.NextScene();
	}

	IEnumerator StartScene() {
		AudioSource[] aSources = GetComponents<AudioSource>();

		yield return new WaitForSeconds(1);
		infoLabel.text = "3";
		aSources[0].Play();
		yield return new WaitForSeconds(1);
		infoLabel.text = "2";
		aSources[0].Play();
		yield return new WaitForSeconds(1);
		infoLabel.text = "1";
		aSources[0].Play();
		yield return new WaitForSeconds(1);
		infoLabel.text = "GO!";
		aSources[1].Play();
#if UNITY_STANDALONE || UNITY_WEBPLAYER

#else
		player.zeroAc = Input.acceleration;
#endif
		inputLocked = false;
		yield return new WaitForSeconds(1);
		infoLabel.text = "";
	}
}
