using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	public Vector3 touchPosition;
	public Camera GUICam;
	public Transform up,down,go;
	public bool isButtonPressed;
	public Transform car;
	// Use this for initialization

	void Start () {
	 	isButtonPressed = car.GetComponent<SeekSteer>().isButtonPressed; 
	}
	
	// Update is called once per frame
	void Update () {

		//GetTouchInput();
		GetMouseInput();
	
	}


	void GetMouseInput ()
		{
				touchPosition = Input.mousePosition;
				Ray ray = GUICam.ScreenPointToRay (touchPosition);
				RaycastHit hit;
				if (Input.GetMouseButton (0)) {
						if (Physics.Raycast (ray, out hit)) {
								if (hit.transform.name == "Go") {
										//up.gameObject.SetActive (false);
										//down.gameObject.renderer.enabled = false;
												car.GetComponent<SeekSteer>().isButtonPressed = true;
										Debug.Log ("GO");
								}
						}
				}

			if(Input.GetMouseButton(0))
			{
				if (Physics.Raycast (ray, out hit)) 
				{
					 if (hit.transform.name == "Up") {
						up.gameObject.SetActive(true);
						//go.gameObject.renderer.enabled = false;
						Debug.Log ("UP");
						}

					else if (hit.transform.name == "Down") {
					up.gameObject.SetActive(true);
					//go.gameObject.renderer.enabled = false;
					Debug.Log ("Down");
					}
				}
			}
			
			else
			{
				Debug.Log("Touch ended");
				go.gameObject.renderer.enabled = true;
				//up.gameObject.SetActive(true);
				down.gameObject.renderer.enabled = true;
				if(car)
					car.GetComponent<SeekSteer>().isButtonPressed = false; 
			}
			
			if(Input.GetMouseButtonUp(0))
			{
				if (Physics.Raycast (ray, out hit)) 
				{
					 if (hit.transform.name == "Up") {
						car.GetComponent<SeekSteer>().currentPathNumber++;
						Debug.Log ("UP done");
						}
						
						 if (hit.transform.name == "Down") {
						 	
						 	car.GetComponent<SeekSteer>().currentPathNumber--;
						
						Debug.Log ("Down done");
						}
				}

			}
			
			
			//Only for Debug Keyboard controls
			
			if(car)
			{
				if(Input.GetKey("up"))
				{
						car.GetComponent<SeekSteer>().isButtonPressed = true;
				}
				if(Input.GetKeyUp("left"))
				{
					car.GetComponent<SeekSteer>().currentPathNumber--;
				}
			
				if(Input.GetKeyUp("right"))
				{
					car.GetComponent<SeekSteer>().currentPathNumber++;
				}
			}

			

	}

	void GetTouchInput ()
		{
				if (Input.touchCount > 0 && (Input.GetTouch (0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Stationary)) {
						touchPosition = Input.GetTouch (0).position;
						Ray ray = GUICam.ScreenPointToRay (touchPosition);
						RaycastHit hit;

						if (Physics.Raycast (ray, out hit)) {
								if (hit.transform.name == "Go") {
										//up.gameObject.SetActive(false);
										down.gameObject.renderer.enabled = false;
										Debug.Log ("GO");
								} else if (hit.transform.name == "Up") {
										up.gameObject.SetActive(true);
										//go.gameObject.renderer.enabled = false;
										Debug.Log ("UP");
								} else if (hit.transform.name == "Down") {
										up.gameObject.SetActive(true);
										//go.gameObject.renderer.enabled = false;
										Debug.Log ("Down");
								}
						}
		
				}

				if (Input.touchCount > 0 && (Input.GetTouch (0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled))
				{
					
							Debug.Log("Touch ended");
			   				go.gameObject.renderer.enabled = true;
							up.gameObject.SetActive(true);
							down.gameObject.renderer.enabled = true;
					
					
				}
	}
	

}
