using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Box : MonoBehaviour {

	Rigidbody2D rb;
	SpriteRenderer spriteRenderer;
	public Text ScoreText;
	public Text FeverText;
	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		initBox();
	}
	// Use this for initialization
	void Start () {
		ScoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
		FeverText = GameObject.FindGameObjectWithTag("FeverText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate()
	{
		if(this.transform.position.y <=0)
		{
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Shoot"))
		{
			if(this.transform.position.x==1){
				if(BoardManager.color[BoardManager.FirstColor] == this.GetComponent<SpriteRenderer>().color){
					SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
					Bullet.Score = 0;
					Bullet.Fever = 0;
				}else{
					Bullet.Score++;
					Bullet.Fever++;
					ScoreText.text = "x " + Bullet.Score;
					if(Bullet.Fever<20)
					{
						FeverText.text = "x "+ Bullet.Fever;
					}else
					{
						FeverText.text = "FEVER!!";
						BoardManager.FeverScore++;
					}
				}
			}
			if(other.transform.position.x==4){
				if(BoardManager.color[BoardManager.SecondColor] == this.GetComponent<SpriteRenderer>().color){
					SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
					Bullet.Score= 0;
					Bullet.Fever = 0;
				}else{
					Bullet.Score++;
					Bullet.Fever++;
					ScoreText.text = "x " + Bullet.Score;
					if(Bullet.Fever<20)
					{
						FeverText.text = "x " + Bullet.Fever;
					}else
					{
						FeverText.text = "FEVER!!";
						BoardManager.FeverScore++;
					}
				}
			}
		}
	}
	void initBox()
	{
		rb.velocity = new Vector2(0, -3);
		//Debug.Log(BoardManager.FirstColor);
		//spriteRenderer.material.SetColor("_Color", BoardManager.color[BoardManager.FirstColor]);
	}
}
