﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {


	public Remap remap;
	public Gesture gesture;
	public float delaySwipeLeft;
	public float delaySwipeRight;
	public float sensitivitySwipeLeft;
	public float sensitivitySwipeRight;
	private List<GameObject> stages;
	private List<Vector3> initialPosition;

	public Image temporizador;

	private int task;

	private float gameTime;

	private bool moving;
	private float movingTime;
	public float targetMovingTime;

	private int stageNumber;
	private int newStage;
	private int currentStage;
	private int targetTask;
	private int counterTask;
	

	private bool alternateHand;

	public GameObject part1;
	public GameObject part3;
	public GameObject ending;
	public GameObject GameGO;
	public GameObject GameCanvas;

	public Image circle;
	public Text stageText;
	public Text taskD;
	public Text times;
    public Text ScoreTXT;

	private int score;



	// Use this for initialization
	void Start () {


		stages= new List<GameObject>(); 
		initialPosition= new List<Vector3>(); 

		foreach (Transform t in transform) {

			stages.Add (t.gameObject);
			initialPosition.Add (t.gameObject.transform.position);
		}

		//////////////


		newStage = Mathf.RoundToInt(Random.value * 9);

		while(newStage == currentStage) {

			newStage = Mathf.RoundToInt(Random.value * 9);

		}

		gameTime = 90;

		generateTask ();

		stageNumber = 1;
        ScoreTXT.text = "SCORE:" +0;
    }


	public int getScore(){

		return score;
	}

	public void setScore(int value){

		score = value;
	}

	private void generateTask(){

		task = Mathf.RoundToInt(Random.value*5);

		if (task == 1 || task==4 || task==5) {

			targetTask = Mathf.RoundToInt (Random.value * 3 + 3);

			while (targetTask!=2 && targetTask!=4 && targetTask!=6) {

				targetTask = Mathf.RoundToInt (Random.value * 3 + 3);
			}


		} else {
		
			targetTask=Mathf.RoundToInt(Random.value*2+1);
		
		}

		counterTask = targetTask;
	
		times.text=counterTask+"x";
		circle.fillAmount = 1;
	}

	private void gameTimeHandler(){

		if (gameTime > 0) {
	
			gameTime -= Time.deltaTime;
			temporizador.fillAmount = remap.remap (gameTime, 0, 90, 0, 1);
		
		} else {
		
			this.gameObject.SetActive (false);
		
		}

	}


	private void taskToDo(){

	
		if (task == 0 && !moving) {
		
			////////////////////CONTEÚDO Gráfico//////////////////

			stageText.text= "STAGE "+ (stageNumber).ToString()+" -";
			taskD.text = "PINCH!";


			if (counterTask > 0) {

				if (gesture.pinch ()) {

						counterTask--;

					if (counterTask >=1) {
						times.text = counterTask + "x";	
					}
					
			    	circle.fillAmount = remap.remap(counterTask,0,targetTask,0,1);
				}


			} else {

				GameCanvas.gameObject.SetActive (false);
				movingTime = 0;
				moving = true;
				circle.fillAmount = 1;
				stageNumber++;
                score += 10;
                ScoreTXT.text = "SCORE: " + score;
            }
			 
		}

		if (task == 1 && !moving) {

			////////////////////CONTEÚDO Gráfico//////////////////
		

			stageText.text= "STAGE "+ (stageNumber).ToString()+" -";
			taskD.text = "CLOSE AND OPEN \nYOUR HAND";


			if (counterTask > 0) {

					if (gesture.closeHand () && !alternateHand) {
						
						alternateHand = true;
				
						counterTask--;

					if (counterTask >=1) {
						times.text = counterTask + "x";	
					}

                    circle.fillAmount = remap.remap(counterTask, 0, targetTask, 0, 1);
                }


					if (gesture.openHand () && alternateHand) {
					
						alternateHand = false;
				
						counterTask--;

					if (counterTask >=1) {
						times.text = counterTask + "x";	
					}
                    circle.fillAmount = remap.remap(counterTask, 0, targetTask, 0, 1);


                }
			

			} else {

				alternateHand = false;
				GameCanvas.gameObject.SetActive (false);
				movingTime = 0;
				moving = true;
				circle.fillAmount = 1;
				stageNumber++;
                score += 10;
                ScoreTXT.text = "SCORE: "+score;
            }

		}

		if (task == 2 && !moving) {

			stageText.text= "STAGE "+ (stageNumber).ToString()+" -";
			taskD.text = "SWIPE LEFT";


			if (counterTask > 0) {

				if (gesture.swipeLeft (sensitivitySwipeLeft, delaySwipeLeft) ) {

						counterTask--;

					if (counterTask >=1) {
						times.text = counterTask + "x";	
					}
                    circle.fillAmount = remap.remap(counterTask, 0, targetTask, 0, 1);
                }



			} else {

				GameCanvas.gameObject.SetActive (false);
				movingTime = 0;
				moving = true;
				circle.fillAmount = 1;
				stageNumber++;
                score += 10;
                ScoreTXT.text = "SCORE: "+score;
            }

		}

		if (task == 3 && !moving) {

			stageText.text= "STAGE "+ (stageNumber).ToString()+" -";
			taskD.text = "SWIPE RIGHT";


			if (counterTask > 0) {

				if (gesture.swipeRight (sensitivitySwipeRight, delaySwipeRight) ) {

					Debug.Log ("Entra");
						counterTask--;

					if (counterTask >= 1) {
						times.text = counterTask + "x";	
					}
                    circle.fillAmount = remap.remap(counterTask, 0, targetTask, 0, 1);
                }



			} else {
				
				GameCanvas.gameObject.SetActive (false);
				movingTime = 0;
				moving = true;
				circle.fillAmount = 1;
				stageNumber++;
                score += 10;
                ScoreTXT.text = "SCORE: " + score;
            }

		}
	
		if (task == 4 && !moving) {

			stageText.text= "STAGE "+ (stageNumber).ToString()+" -";
			taskD.text = "HORNS THEN \nOPEN YOUR HAND!";


			if (counterTask > 0) {

				if (gesture.horns () && !alternateHand) {

					alternateHand = true;

					counterTask--;

					
					if (counterTask >=1) {
						
						times.text = counterTask + "x";	

					}

                    circle.fillAmount = remap.remap(counterTask, 0, targetTask, 0, 1);
                }


				if (gesture.openHand () && alternateHand) {

					alternateHand = false;
		
					counterTask--;

					if (counterTask >=1) {
						times.text = counterTask + "x";	
					}
                    circle.fillAmount = remap.remap(counterTask, 0, targetTask, 0, 1);

                }


			} else {
				
				GameCanvas.gameObject.SetActive (false);
				alternateHand = false;
				movingTime = 0;
				moving = true;
				circle.fillAmount = 1;
				stageNumber++;
                score += 10;
                ScoreTXT.text = "SCORE: " + score;
            }

		}

		if (task == 5 && !moving) {

			stageText.text= "STAGE "+ (stageNumber).ToString()+" -";
			taskD.text = "SWIPE LEFT, THEN \nSWIPE RIGHT";


			if (counterTask > 0) {

				if (gesture.swipeLeft (sensitivitySwipeLeft, delaySwipeLeft) && !alternateHand) {

					alternateHand = true;

						counterTask--;

					if (counterTask >= 1) {
						
						times.text = counterTask + "x";	
					}
                    circle.fillAmount = remap.remap(counterTask, 0, targetTask, 0, 1);
                }


				if (gesture.swipeRight (sensitivitySwipeRight, delaySwipeRight) && alternateHand) {

					alternateHand = false;
				
						counterTask--;


					if (counterTask >=1) {
						times.text = counterTask + "x";	
					}
                    circle.fillAmount = remap.remap(counterTask, 0, targetTask, 0, 1);

                }


			} else {
				
				GameCanvas.gameObject.SetActive (false);
				alternateHand = false;
				movingTime = 0;
				moving = true;
				circle.fillAmount = 1;
				stageNumber++;
                score += 10;
                ScoreTXT.text = "SCORE: " + score;
            }

		}


	}


	private void nextStage(){
	


		if (moving) {
			

			GameCanvas.gameObject.SetActive (false);

			if (movingTime < targetMovingTime) {
				
			
				stages [currentStage].SetActive (true);
				stages [newStage].SetActive (true);

				movingTime += Time.deltaTime;

				stages [currentStage].transform.position = Vector3.Lerp (initialPosition [currentStage], new Vector3 (initialPosition [currentStage].x, initialPosition [currentStage].y + 525, initialPosition [currentStage].z), movingTime * (movingTime / targetMovingTime));
				stages [newStage].transform.position = Vector3.Lerp (new Vector3 (initialPosition [newStage].x, initialPosition [newStage].y - 525, initialPosition [newStage].z), initialPosition [newStage], movingTime * (movingTime / targetMovingTime));

			} else {


				stages [currentStage].SetActive (false);
				currentStage = newStage;

				newStage = Mathf.RoundToInt(Random.value * 9);

				while(newStage == currentStage) {

					newStage = Mathf.RoundToInt(Random.value * 9);

				}
				movingTime = 0;
				generateTask ();
				moving = false;

			}

		} else {

			GameCanvas.gameObject.SetActive (true);
		}
	
	}
		

	// Update is called once per frame
	void Update () {

		if (gameTime > 0) {
		
			taskToDo ();
			nextStage ();
	
			//Debug.Log(alternateHand);
			gameTimeHandler ();

		} else {
			temporizador.fillAmount = 1;

			circle.fillAmount = 1;
			stageNumber = 1;
			GameCanvas.gameObject.SetActive (false);
			gameTime = 90;
			generateTask ();
			ending.SetActive (true);
			this.gameObject.SetActive (false);
		}



	}
}
