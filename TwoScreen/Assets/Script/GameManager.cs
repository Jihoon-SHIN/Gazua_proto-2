using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public float turnDelay = .05f;
	//public static : If we declare in this method,
	//We can use the GameManager in every script
	public static GameManager instance = null;
	public BoardManager boardScript;
	public int playerFoodPoints = 100;
	[HideInInspector] public bool playersTurn = true;

	//private variable 
	private int lvl =3;
	//private List<Enemy> enemies;
	private bool enemiesMoving;

	// Use this for initialization
	void Awake () 
	{
		//It is GameManager Script, so "this" is GameManager
		if(instance == null)
			instance = this;
		else if(instance != this)
			Destroy(gameObject);

		//Scene이 바뀌면 Scene에 있는 hierarchy에 있는 것들이 다 지워지는데, DontDestroyOnLoad를 사용하면 다 지워지지 않음
		//Scene이 바뀔 때 Score같은 것을 유지할 때 용이한 것 같음
		DontDestroyOnLoad(gameObject);

		boardScript = GetComponent<BoardManager>();
		initGame();
	}
	
	void initGame()
	{
		boardScript.SetupScene(lvl);
	}
	public void GameOver()
	{
		enabled = false;
	}
	// Update is called once per frame
	void Update () 
	{
		if(playersTurn || enemiesMoving)
			return;
		//StartCoroutine(MoveEnemies());	
	}

	/* 
	public void AddEnemyToList(Enemy script)
	{
		enemies.Add(script);
	}

	IEnumerator MoveEnemies()
	{
		enemiesMoving = true;
		yield return new WaitForSeconds(turnDelay);
		if(enemies.Count == 0)
		{
			yield return new WaitForSeconds(turnDelay);
		}

		for(int i=0; i<enemies.Count; i++)
		{
			enemies[i].MoveEnemy();
			yield return new WaitForSeconds(enemies[i].moveTime);
		}

		playersTurn = true;
		enemiesMoving = false;
	}
	*/
}

