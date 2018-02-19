using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bullet : MonoBehaviour {

	private int _checkOnce;

	public Text ScoreText;
	public Text FeverText;
	public static int Score = 0;
	public static int Fever = 0;
	// Use this for initialization
	void Awake()
	{
		_checkOnce = 0;
		ScoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
		FeverText = GameObject.FindGameObjectWithTag("FeverText").GetComponent<Text>();
	}
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate()
	{
		if(this.transform.position.y >= 9)
		{
			Destroy(gameObject);
		}
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Box"))
		{
			if(_checkOnce==0)
			{
				//Debug.Log(BoardManager.color[BoardManager.FirstColor]);
				//Debug.Log(other.GetComponent<SpriteRenderer>().color);
				if(other.transform.position.x==1){
					if(BoardManager.color[BoardManager.FirstColor] == other.GetComponent<SpriteRenderer>().color)
					{
						Destroy(other.gameObject);
						Destroy(gameObject);
						Score++;
						Fever++;
						ScoreText.text = "x " + Score;
						if(Fever<20){
							FeverText.text = "x " + Fever;
						}
					}else if(other.GetComponent<SpriteRenderer>().color == Color.white)
					{
						Destroy(other.gameObject);
						Destroy(gameObject);
						Score++;
						BoardManager.FeverScore++;
						ScoreText.text = "x " + Score;
						FeverText.text = "FEVER!!";
					}
					else{
						SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
						Score = 0;
						Fever = 0;
					}
				}
				if(other.transform.position.x==4){
					if(BoardManager.color[BoardManager.SecondColor] == other.GetComponent<SpriteRenderer>().color)
					{
						Destroy(other.gameObject);
						Destroy(gameObject);
						Score++;
						Fever++;
						ScoreText.text = "x " + Score;
						if(Fever<20){
							FeverText.text = "x " + Fever;
						}						
					}
					else if(other.GetComponent<SpriteRenderer>().color == Color.white)
					{
						Destroy(other.gameObject);
						Destroy(gameObject);
						Score++;
						BoardManager.FeverScore++;
						ScoreText.text = "x " + Score;
						FeverText.text = "FEVER!!";
					}
					else
					{
						SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
						Score = 0;
						Fever = 0;
					}
				}
				
			}
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if(_checkOnce==1)
		{
			_checkOnce =0;
		}
	}
}
