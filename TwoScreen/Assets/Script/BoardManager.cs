using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

	//Custom Class can't see in inspector.
	//Serializable help us to see variable in inspector.
	[Serializable]
	public class Count{
		public int minimum;
		public int maximum;

		public Count(int min, int max){
			minimum = min;
			maximum = max;
		}
	}

	public int columns = 10;
	public int rows= 6;
	//public Count foodCount = new Count(30,40);
	//public Count enemyCount = new Count(1,1);
	public GameObject[] floorTiles;
	public GameObject shootMachine1;
	public GameObject shootMachine2;
	public GameObject Box;
	public AudioSource BGM;

	private Transform boardHolder;
	private List<Vector3> gridPositions = new List<Vector3>();

	private int _startx1;
	private int _startx2;
	private int _starty;
	private float _timer = 0;
	private float _fevertimer = 0;
	private float _feverDelay = 0;

	public static List<Color> color = new List<Color>();
	public static int FirstColor;
	public static int SecondColor;
	public static int RandomColorL;
	public static int RandomColorR;
	public static int FeverScore = 0;

	public static BoardManager instance = null;
	void Awake()
	{
		_startx1 = 1;
		_startx2 = 4;
		_starty = 9;
	}

	void Update()
	{	
		/* 
		if(BGM.time ==2 || BGM.time==4){
			Debug.Log(BGM.time);
			StartCoroutine(GenerateBox(1f));	
		}
		*/
		//Debug.Log(_timer);
		if(Bullet.Fever<19)
		{
			_timer++;
			if(_timer>45)
			{
			//StartCoroutine(GenerateBox(2f));
				GenerateBox1();
				_timer = 0;
			}
		}else
		{
			_fevertimer++;
			if(_fevertimer>30)
			{
				GenerateBox_white();
				_fevertimer = 0;
				StartCoroutine(FeverDelay(2f));
			}
			if(FeverScore >20){
				Bullet.Fever = 0;
				FeverScore = 0;

			}
		}

	}

	void initialiseList()
	{
		gridPositions.Clear();
		for(int x=0; x<rows ;x++)
		{
			for(int y=0; y<columns; y++)
			{
				gridPositions.Add(new Vector3(x,y, 0f));
			}
		}

	}
	void changeColor(int a, int b)
	{
		if(Bullet.Fever<20)
		{
			shootMachine1.GetComponent<SpriteRenderer>().material.SetColor("_Color", color[a]);
			shootMachine2.GetComponent<SpriteRenderer>().material.SetColor("_Color", color[b]);
		}else
		{
			FirstColor = 4;
			SecondColor = 4;
			shootMachine1.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.white);
			shootMachine2.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.white);			
		}
	}
	void BoardSetup(int a, int b)
	{
		boardHolder = new GameObject("Board").transform;
		
		for(int x=0; x<rows; x++)
		{
			for(int y=0; y<columns; y++)
			{
				GameObject toInstantiate;
				toInstantiate = null;
				if((x==0 && y==columns-1) || (x==3 && y==columns-1))
				{
					toInstantiate = floorTiles[0];
				}
				else if((x==1 && y==columns-1) || (x==4 && y==columns-1))
				{
					toInstantiate = floorTiles[1];
				}
				else if((x==5 && y==columns-1) || ( x==2 && y== columns-1))
				{
					toInstantiate = floorTiles[2];
				}
				else if(x==5 && y==0)
				{
					toInstantiate = floorTiles[4];
				}
				else if((x== 1 && y==0)||(x==4&&y==0)){
					toInstantiate = floorTiles[5];
				}
				else if((x==rows-1 && y==0) || (x==2 && y==0)) 
				{
					toInstantiate = floorTiles[4];
				}
				else if((x==0 && y==0) || (x==3 && y==0))
				{
					toInstantiate = floorTiles[6];
				}
				else if(x == 0 || x==3)
				{
					toInstantiate = floorTiles[7];
				}
				else if(x==2 || x== 5)
				{
					toInstantiate = floorTiles[3];
				}
				//Clean Img... null로 설정하니까 오류가 나서 일단 투명 이미지로 넣어둠
				else
				{
					toInstantiate = floorTiles[8];
				}
				GameObject instance = Instantiate(toInstantiate, new Vector3(x,y,0f), Quaternion.identity) as GameObject;
				if(x==0 || x==1 || x==2)
				{
					instance.GetComponent<SpriteRenderer>().material.SetColor("_Color", color[a]);
				}
				if(x==3 || x==4 || x==5)
				{
					instance.GetComponent<SpriteRenderer>().material.SetColor("_Color", color[b]);
				}
				instance.transform.SetParent(boardHolder);
			}
		}
	}  

	Vector3 RandomPostition()
	{
		int randomindex = Random.Range(0, gridPositions.Count);
		Vector3 randomPosition = gridPositions[randomindex];
		gridPositions.RemoveAt(randomindex);
		return randomPosition;
	}

	void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
	{
		int objectCount = Random.Range(minimum, maximum+1);
		for( int i=0; i<objectCount ; i++)
		{
			Vector3 randomPostion = RandomPostition();
			GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
			Instantiate (tileChoice, randomPostion, Quaternion.identity);			
		}
	}
	public void SetupScene(int lvl)
	{
		FirstColor = Random.Range(0,2);
		SecondColor = Random.Range(2,4);
		color.Add(Color.red);
		color.Add(Color.green);
		color.Add(Color.blue);
		color.Add(Color.yellow);
		color.Add(Color.white);
		BoardSetup(FirstColor, SecondColor);
		changeColor(FirstColor, SecondColor);
		initialiseList();
		//LayoutObjectAtRandom(foodTiles, foodCount.minimum, foodCount.maximum);

	}
	public void GenerateBox1()
	{
		RandomColorL = Random.Range(0,4);
		RandomColorR = Random.Range(0,4);

		GameObject instance1 = Instantiate(Box, new Vector2(_startx1, _starty), Quaternion.identity) as GameObject;
		GameObject instance2 = Instantiate(Box, new Vector2(_startx2, _starty), Quaternion.identity) as GameObject;
		instance1.GetComponent<SpriteRenderer>().color = color[RandomColorL];
		//instance1.GetComponent<SpriteRenderer>().material.SetColor("_Color", color[RandomColorL]);
		//instance2.GetComponent<SpriteRenderer>().material.SetColor("_Color", color[RandomColorR]);		
		instance2.GetComponent<SpriteRenderer>().color = color[RandomColorR];
	}
	public void GenerateBox_white()
	{
		RandomColorL = 4;
		RandomColorR = 4;
		GameObject instance1 = Instantiate(Box, new Vector2(_startx1, _starty), Quaternion.identity) as GameObject;
		GameObject instance2 = Instantiate(Box, new Vector2(_startx2, _starty), Quaternion.identity) as GameObject;
		instance1.GetComponent<SpriteRenderer>().color = color[RandomColorL];
		//instance1.GetComponent<SpriteRenderer>().material.SetColor("_Color", color[RandomColorL]);
		//instance2.GetComponent<SpriteRenderer>().material.SetColor("_Color", color[RandomColorR]);		
		instance2.GetComponent<SpriteRenderer>().color = color[RandomColorR];
	}

	IEnumerator FeverDelay(float time)
	{
		yield return new WaitForSeconds(time);
	}
}
