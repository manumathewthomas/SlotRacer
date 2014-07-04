using UnityEngine;
using System.Collections;

public class manager : MonoBehaviour {
	
	public GameObject player;
	public GameObject greenAI;
	public GameObject blueAI;
	public int score;
	public GUIText gameOver;
	public GUIText score_text;
	public bool isGameOver;

	// Use this for initialization
	void Start () {
		gameOver.gameObject.SetActive(false);
		score_text.gameObject.SetActive(false);
		isGameOver = false;
		
	}
	
	// Update is called once per frame
	void Update () {

	if(!player||isGameOver)
	{
		gameOver.gameObject.SetActive(true);
		
		score_text.text = "Score: "+score.ToString();
		score_text.gameObject.SetActive(true);
	}
	else
	{
			score = player.GetComponent<SeekSteer>().score;
	}
	
	}
}
