using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMachine : MonoBehaviour {
	public KeyCode key;
	public GameObject bullet;

	public Vector2 pos;
	public List<Color> color;
	public int FirstColor;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(key))
		{
			GameObject instance;
			pos = new Vector2(transform.position.x, transform.position.y + 1);
			instance = Instantiate(bullet, pos, Quaternion.identity) as GameObject;
			instance.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 8f);
			//StartCoroutine(deleteBullet(instance));
		}
	}

	IEnumerator deleteBullet(GameObject instance)
	{
		yield return new WaitForSeconds(0.2f);
		//Destroy(instance.gameObject);
	}
}
