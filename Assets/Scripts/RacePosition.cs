using UnityEngine;
using System.Collections;

public class RacePosition : MonoBehaviour {

	public GameObject[] Cars;
	public int redLap,greenLap,blueLap;
	public Transform[] Checkpoints;
	public int[] positions;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Cars[0])
			redLap = Cars[0].GetComponent<SeekSteer>().lapCounter;
		if(Cars[1])
			greenLap = Cars[1].GetComponent<SeekSteer>().lapCounter;
		if(Cars[2])
		blueLap = Cars[2].GetComponent<SeekSteer>().lapCounter;
		
		if(redLap>10)
		{
			Cars[0].GetComponent<SeekSteer>().isButtonPressed = false;
			this.GetComponent<manager>().isGameOver = true;
			Cars[0].GetComponent<SeekSteer>().isGameOver =true;
		}
		else if(greenLap>10)
		{
			Cars[1].GetComponent<SeekSteer>().isButtonPressed = false;
		}
		
		else if(blueLap>10)
		{
			Cars[2].GetComponent<SeekSteer>().isButtonPressed = false;
		}
	}
}
