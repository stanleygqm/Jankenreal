﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Combat_statemachine : MonoBehaviour {
    //Initialized player jankens
    public GameObject p1rock;
    public GameObject p1paper;
    public GameObject p1scissors;
    public GameObject p1rock2;
    public GameObject p1paper2;
    public GameObject p1scissors2;
    public GameObject p1left;
    public GameObject p1right;
    //e1janken
    public GameObject e1rock;
    public GameObject e1paper;
    public GameObject e1scissors;
    public GameObject e1rock2;
    public GameObject e1paper2;
    public GameObject e1scissors2;
    public GameObject e1left;
    public GameObject e1right;
    //e2janken
    public GameObject e2rock;
    public GameObject e2paper;
    public GameObject e2scissors;
    public GameObject e2rock2;
    public GameObject e2paper2;
    public GameObject e2scissors2;
    public GameObject e2left;
    public GameObject e2right;
    //e3janken
    public GameObject e3rock;
    public GameObject e3paper;
    public GameObject e3scissors;
    public GameObject e3rock2;
    public GameObject e3paper2;
    public GameObject e3scissors2;
    public GameObject e3left;
    public GameObject e3right;
    //playerpanel
    public GameObject leftR;
    public GameObject leftP;
    public GameObject leftS;
    public GameObject rightR;
    public GameObject rightP;
    public GameObject rightS;
    //Initialized players(may not need)
    public Player_statemachine PSM;
	public Emeny_AIstatemachine ESM1, ESM2, ESM3;
	private HandleTurn HT;
	//variable for timers
	private int seconds_current = 0;
	private int seconds_max = 60;
	//battle turn state
	public enum turnState{
		START,
		PLAYERCHOICE,
		ENEMYCHOICE,
		ACTION
	}
	public turnState currentState;

	//List for storing existing player's information
	public List<HandleTurn> PerformList = new List<HandleTurn> ();// collect all the actions via HandleTurn Class
	public List<GameObject> PlayerInBattle = new List<GameObject>();// collect all the player existed in the field


	//initialize player input
	public enum PlayerGUI{ ACTIVATE, INPUT, DONE}
	public PlayerGUI playerInput;
	public HandleTurn playerChoice;
	//public List<GameObject> PlayerToManage = new List<GameObject>

	//initialize attack choice here


	//initialization of state
	void Start () {
		currentState = turnState.START;
		playerInput = PlayerGUI.ACTIVATE;
		PlayerInBattle.AddRange (GameObject.FindGameObjectsWithTag("Player"));
		PlayerInBattle.AddRange (GameObject.FindGameObjectsWithTag("AI"));
        p1rock = GameObject.Find("playerleftR");
        p1paper = GameObject.Find("playerleftP");
        p1scissors = GameObject.Find("playerleftS");
        p1rock.SetActive(true);
        p1paper.SetActive(false);
        p1scissors.SetActive(false);
        p1rock2 = GameObject.Find("playerrightR");
        p1paper2 = GameObject.Find("playerrightP");
        p1scissors2 = GameObject.Find("playerrightS");
        p1rock2.SetActive(true);
        p1paper2.SetActive(false);
        p1scissors2.SetActive(false);
        p1left = GameObject.Find("playerleft");
        p1right = GameObject.Find("playerright");
        p1left.SetActive(true);
        p1right.SetActive(true);
        leftR = GameObject.Find("Left_Rock");
        leftP = GameObject.Find("Left_Paper");
        leftS = GameObject.Find("Left_Scissors");
        rightR = GameObject.Find("Right_Rock");
        rightP = GameObject.Find("Right_Paper");
        rightS = GameObject.Find("Right_Scissors");
        e1paper = GameObject.Find("e1leftp");
        e1rock = GameObject.Find("e1leftr");
        e1scissors = GameObject.Find("e1lefts");
        e1paper2 = GameObject.Find("e1rightp");
        e1rock2 = GameObject.Find("e1rightr");
        e1scissors2 = GameObject.Find("e1rights");
        e1paper.SetActive(false);
        e1scissors.SetActive(false);
        e1scissors2.SetActive(false);
        e1paper2.SetActive(false);
        e2paper = GameObject.Find("e2leftp");
        e2rock = GameObject.Find("e2leftr");
        e2scissors = GameObject.Find("e2lefts");
        e2paper2 = GameObject.Find("e2rightp");
        e2rock2 = GameObject.Find("e2rightr");
        e2scissors2 = GameObject.Find("e2rights");
        e2paper.SetActive(false);
        e2scissors.SetActive(false);
        e2scissors2.SetActive(false);
        e2paper2.SetActive(false);
        e3paper = GameObject.Find("e3leftp");
        e3rock = GameObject.Find("e3leftr");
        e3scissors = GameObject.Find("e3lefts");
        e3paper2 = GameObject.Find("e3rightp");
        e3rock2 = GameObject.Find("e3rightr");
        e3scissors2 = GameObject.Find("e3rights");
        e3paper.SetActive(false);
        e3scissors.SetActive(false);
        e3scissors2.SetActive(false);
        e3paper2.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		//how the battle is progressed each turn
		//Debug.Log ("currentState: "+ currentState);
		switch(currentState){
		case(turnState.START):
			//PerformList = new List<HandleTurn> ();
			playerChoice = new HandleTurn ();
			currentState = turnState.PLAYERCHOICE;
			break;
		case(turnState.PLAYERCHOICE):
			if (playerInput == PlayerGUI.DONE) {currentState = turnState.ENEMYCHOICE;}
			break;
		case(turnState.ENEMYCHOICE):
			if (PerformList.Count == PlayerInBattle.Count) {currentState = turnState.ACTION;}
			break;
		case(turnState.ACTION):
			//put in the logic here
			battleLogic ();
			// Replace with transition animation
			currentState = turnState.START;
			break;
		}

		//how the player turn is progressed
		//Debug.Log ("playerInput: "+ playerInput);
		switch (playerInput){
		case(PlayerGUI.ACTIVATE):
			playerInput = PlayerGUI.INPUT;
			break;
		case(PlayerGUI.INPUT):
			if (playerChoice.AttackTarget != null) {
				playerInput = PlayerGUI.DONE;
			}
			break;
		case(PlayerGUI.DONE):
			if(currentState == turnState.PLAYERCHOICE){playerInput = PlayerGUI.ACTIVATE;}
			break;
		}	
	}

	void timerPlayer(){
		//counting down the time and force skip player's turn if no action taken after time limit
	}

	public void CollectActions(HandleTurn input){
			PerformList.Add (input); // recorded actions chosen by enemy
	}

	public void chooseRockLeft(){
        p1rock.SetActive(true);
        p1paper.SetActive(false);
        p1scissors.SetActive(false);
        Debug.Log ("Chosen Rock on the left");
		playerChoice.LeftAttackType = HandleTurn.janken.ROCK;
	}

	public void chooseScissorsLeft(){
        p1rock.SetActive(false);
        p1paper.SetActive(false);
        p1scissors.SetActive(true);
        Debug.Log ("Chosen Scissors on the left");
		playerChoice.LeftAttackType = HandleTurn.janken.SCISSORS;
	}

	public void choosePaperLeft(){
        p1rock.SetActive(false);
        p1paper.SetActive(true);
        p1scissors.SetActive(false);
        Debug.Log ("Chosen Paper on the left");
		playerChoice.LeftAttackType = HandleTurn.janken.PAPER;
	}

	public void chooseRockRight(){
        p1rock2.SetActive(true);
        p1paper2.SetActive(false);
        p1scissors2.SetActive(false);
        Debug.Log ("Chosen Rock on the right");
		playerChoice.RightAttackType = HandleTurn.janken.ROCK;
	}

	public void chooseScissorsRight(){
        p1rock2.SetActive(false);
        p1paper2.SetActive(false);
        p1scissors2.SetActive(true);
        Debug.Log ("Chosen Scissors on the right");
		playerChoice.RightAttackType = HandleTurn.janken.SCISSORS;
	}

	public void choosePaperRight(){
        p1rock2.SetActive(false);
        p1paper2.SetActive(true);
        p1scissors2.SetActive(false);
        Debug.Log ("Chosen Paper on the right");
		playerChoice.RightAttackType = HandleTurn.janken.PAPER;
	}

	public void enemySelected(){
		// update player choice of Attack target
		playerChoice.AttackTarget = this.gameObject;
	}
	public void battleLogic(){
		Debug.Log ("Battle Start");
		for (int i = 0; i < PerformList.Count; i++){
			for(int j = 0; j < 2; j++){
				if(PerformList[j].AttackTarget == PerformList[i].AttackGameObject){
					int resultLeft =  howToWin(PerformList[j].LeftAttackType, PerformList[i].RightAttackType);
					int resultRight =  howToWin(PerformList[j].RightAttackType, PerformList[i].LeftAttackType);
                    if (PerformList[i].Attacker == "e1")
                    {
                        if (PerformList[i].LeftAttackType == HandleTurn.janken.ROCK)
                        {
                            e1rock.SetActive(true);
                            e1scissors.SetActive(false);
                            e1paper.SetActive(false);
                        }
                        else if (PerformList[i].LeftAttackType == HandleTurn.janken.PAPER)
                        {
                            e1rock.SetActive(false);
                            e1scissors.SetActive(false);
                            e1paper.SetActive(true);
                        }
                        else if (PerformList[i].LeftAttackType == HandleTurn.janken.SCISSORS)
                        {
                            e1rock.SetActive(false);
                            e1scissors.SetActive(true);
                            e1paper.SetActive(false);
                        }
                        else if (PerformList[i].RightAttackType == HandleTurn.janken.ROCK)
                        {
                            e1rock2.SetActive(true);
                            e1scissors2.SetActive(false);
                            e1paper2.SetActive(false);
                        }
                        else if (PerformList[i].RightAttackType == HandleTurn.janken.PAPER)
                        {
                            e1rock2.SetActive(false);
                            e1scissors2.SetActive(false);
                            e1paper2.SetActive(true);
                        }
                        else if (PerformList[i].RightAttackType == HandleTurn.janken.SCISSORS)
                        {
                            e1rock2.SetActive(false);
                            e1scissors2.SetActive(true);
                            e1paper2.SetActive(false);
                        }
                    }
                    else if (PerformList[i].Attacker == "e2")
                    {
                        if (PerformList[i].LeftAttackType == HandleTurn.janken.ROCK)
                        {
                            e2rock.SetActive(true);
                            e2scissors.SetActive(false);
                            e2paper.SetActive(false);
                        }
                        else if (PerformList[i].LeftAttackType == HandleTurn.janken.PAPER)
                        {
                            e2rock.SetActive(false);
                            e2scissors.SetActive(false);
                            e2paper.SetActive(true);
                        }
                        else if (PerformList[i].LeftAttackType == HandleTurn.janken.SCISSORS)
                        {
                            e2rock.SetActive(false);
                            e2scissors.SetActive(true);
                            e2paper.SetActive(false);
                        }
                        else if (PerformList[i].RightAttackType == HandleTurn.janken.ROCK)
                        {
                            e2rock2.SetActive(true);
                            e2scissors2.SetActive(false);
                            e2paper2.SetActive(false);
                        }
                        else if (PerformList[i].RightAttackType == HandleTurn.janken.PAPER)
                        {
                            e2rock2.SetActive(false);
                            e2scissors2.SetActive(false);
                            e2paper2.SetActive(true);
                        }
                        else if (PerformList[i].RightAttackType == HandleTurn.janken.SCISSORS)
                        {
                            e2rock2.SetActive(false);
                            e2scissors2.SetActive(true);
                            e2paper2.SetActive(false);
                        }
                    }
                    else if (PerformList[i].Attacker == "e3")
                    {
                        if (PerformList[i].LeftAttackType == HandleTurn.janken.ROCK)
                        {
                            e3rock.SetActive(true);
                            e3scissors.SetActive(false);
                            e3paper.SetActive(false);
                        }
                        else if (PerformList[i].LeftAttackType == HandleTurn.janken.PAPER)
                        {
                            e3rock.SetActive(false);
                            e3scissors.SetActive(false);
                            e3paper.SetActive(true);
                        }
                        else if (PerformList[i].LeftAttackType == HandleTurn.janken.SCISSORS)
                        {
                            e3rock.SetActive(false);
                            e3scissors.SetActive(true);
                            e3paper.SetActive(false);
                        }
                        else if (PerformList[i].RightAttackType == HandleTurn.janken.ROCK)
                        {
                            e3rock2.SetActive(true);
                            e3scissors2.SetActive(false);
                            e3paper2.SetActive(false);
                        }
                        else if (PerformList[i].RightAttackType == HandleTurn.janken.PAPER)
                        {
                            e3rock2.SetActive(false);
                            e3scissors2.SetActive(false);
                            e3paper2.SetActive(true);
                        }
                        else if (PerformList[i].RightAttackType == HandleTurn.janken.SCISSORS)
                        {
                            e3rock2.SetActive(false);
                            e3scissors2.SetActive(true);
                            e3paper2.SetActive(false);
                        }
                    }
                    if (PerformList[j].Attacker == "e1")
                    {
                        if (PerformList[j].LeftAttackType == HandleTurn.janken.ROCK)
                        {
                            e1rock.SetActive(true);
                            e1scissors.SetActive(false);
                            e1paper.SetActive(false);
                        }
                        else if (PerformList[j].LeftAttackType == HandleTurn.janken.PAPER)
                        {
                            e1rock.SetActive(false);
                            e1scissors.SetActive(false);
                            e1paper.SetActive(true);
                        }
                        else if (PerformList[j].LeftAttackType == HandleTurn.janken.SCISSORS)
                        {
                            e1rock.SetActive(false);
                            e1scissors.SetActive(true);
                            e1paper.SetActive(false);
                        }
                        else if (PerformList[j].RightAttackType == HandleTurn.janken.ROCK)
                        {
                            e1rock2.SetActive(true);
                            e1scissors2.SetActive(false);
                            e1paper2.SetActive(false);
                        }
                        else if (PerformList[j].RightAttackType == HandleTurn.janken.PAPER)
                        {
                            e1rock2.SetActive(false);
                            e1scissors2.SetActive(false);
                            e1paper2.SetActive(true);
                        }
                        else if (PerformList[j].RightAttackType == HandleTurn.janken.SCISSORS)
                        {
                            e1rock2.SetActive(false);
                            e1scissors2.SetActive(true);
                            e1paper2.SetActive(false);
                        }
                    }
                    else if (PerformList[j].Attacker == "e2")
                    {
                        if (PerformList[j].LeftAttackType == HandleTurn.janken.ROCK)
                        {
                            e2rock.SetActive(true);
                            e2scissors.SetActive(false);
                            e2paper.SetActive(false);
                        }
                        else if (PerformList[j].LeftAttackType == HandleTurn.janken.PAPER)
                        {
                            e2rock.SetActive(false);
                            e2scissors.SetActive(false);
                            e2paper.SetActive(true);
                        }
                        else if (PerformList[j].LeftAttackType == HandleTurn.janken.SCISSORS)
                        {
                            e2rock.SetActive(false);
                            e2scissors.SetActive(true);
                            e2paper.SetActive(false);
                        }
                        else if (PerformList[j].RightAttackType == HandleTurn.janken.ROCK)
                        {
                            e2rock2.SetActive(true);
                            e2scissors2.SetActive(false);
                            e2paper2.SetActive(false);
                        }
                        else if (PerformList[j].RightAttackType == HandleTurn.janken.PAPER)
                        {
                            e2rock2.SetActive(false);
                            e2scissors2.SetActive(false);
                            e2paper2.SetActive(true);
                        }
                        else if (PerformList[j].RightAttackType == HandleTurn.janken.SCISSORS)
                        {
                            e2rock2.SetActive(false);
                            e2scissors2.SetActive(true);
                            e2paper2.SetActive(false);
                        }
                    }
                    else if (PerformList[j].Attacker == "e3")
                    {
                        if (PerformList[j].LeftAttackType == HandleTurn.janken.ROCK)
                        {
                            e3rock.SetActive(true);
                            e3scissors.SetActive(false);
                            e3paper.SetActive(false);
                        }
                        else if (PerformList[j].LeftAttackType == HandleTurn.janken.PAPER)
                        {
                            e3rock.SetActive(false);
                            e3scissors.SetActive(false);
                            e3paper.SetActive(true);
                        }
                        else if (PerformList[j].LeftAttackType == HandleTurn.janken.SCISSORS)
                        {
                            e3rock.SetActive(false);
                            e3scissors.SetActive(true);
                            e3paper.SetActive(false);
                        }
                        else if (PerformList[j].RightAttackType == HandleTurn.janken.ROCK)
                        {
                            e3rock2.SetActive(true);
                            e3scissors2.SetActive(false);
                            e3paper2.SetActive(false);
                        }
                        else if (PerformList[j].RightAttackType == HandleTurn.janken.PAPER)
                        {
                            e3rock2.SetActive(false);
                            e3scissors2.SetActive(false);
                            e3paper2.SetActive(true);
                        }
                        else if (PerformList[j].RightAttackType == HandleTurn.janken.SCISSORS)
                        {
                            e3rock2.SetActive(false);
                            e3scissors2.SetActive(true);
                            e3paper2.SetActive(false);
                        }
                    }
                    if (resultLeft == 1) {
						Debug.Log ( PerformList[j].Attacker + " win, " + PerformList[i].Attacker + " lose");
						if(PerformList[i].AttackGameObject == GameObject.FindWithTag("Player")){
							PerformList [i].AttackGameObject.GetComponent<Player_statemachine> ().player.RightHand_state = false;
                            p1right.SetActive(false);
                            p1rock2.SetActive(false);
                            p1paper2.SetActive(false);
                            p1scissors2.SetActive(false);
                            rightP.SetActive(false);
                            rightR.SetActive(false);
                            rightS.SetActive(false);

                        }
						else
						{
							PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state = false;
                            if (PerformList[i].Attacker == "e1")
                            {
                                GameObject.Find("e1right").SetActive(false);
                                GameObject.Find("e1rightr").SetActive(false);
                                GameObject.Find("e1rightp").SetActive(false);
                                GameObject.Find("e1rights").SetActive(false);
                            }
                            else if (PerformList[i].Attacker == "e2")
                            {
                                GameObject.Find("e2right").SetActive(false);
                                GameObject.Find("e2rightr").SetActive(false);
                                GameObject.Find("e2rightp").SetActive(false);
                                GameObject.Find("e2rights").SetActive(false);
                            }
                            else if (PerformList[i].Attacker == "e3")
                            {
                                GameObject.Find("e3right").SetActive(false);
                                GameObject.Find("e3rightr").SetActive(false);
                                GameObject.Find("e3rightp").SetActive(false);
                                GameObject.Find("e3rights").SetActive(false);
                            }
                        }
					} else {
						Debug.Log (PerformList[i].Attacker + " win, " + PerformList[j].Attacker + " lose");
						if (PerformList[j].AttackGameObject == GameObject.FindWithTag("Player"))
						{
							PerformList[j].AttackGameObject.GetComponent<Player_statemachine>().player.LeftHand_state = false;
                            p1left.SetActive(false);
                            p1rock.SetActive(false);
                            p1paper.SetActive(false);
                            p1scissors.SetActive(false);
                            leftR.SetActive(false);
                            leftP.SetActive(false);
                            leftS.SetActive(false);
                        }
						else
						{
							PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.LeftHand_state = false;
                            if (PerformList[j].Attacker == "e1")
                            {
                                GameObject.Find("e1left").SetActive(false);
                                GameObject.Find("e1leftr").SetActive(false);
                                GameObject.Find("e1lefts").SetActive(false);
                                GameObject.Find("e1leftp").SetActive(false);
                            }
                            else if (PerformList[j].Attacker == "e2")
                            {
                                GameObject.Find("e2left").SetActive(false);
                                GameObject.Find("e2leftr").SetActive(false);
                                GameObject.Find("e2lefts").SetActive(false);
                                GameObject.Find("e2leftp").SetActive(false);
                            }
                            else if (PerformList[j].Attacker == "e3")
                            {
                                GameObject.Find("e3left").SetActive(false);
                                GameObject.Find("e3leftr").SetActive(false);
                                GameObject.Find("e3lefts").SetActive(false);
                                GameObject.Find("e3leftp").SetActive(false);
                            }
                        }
					}
					if (resultRight == 1) {
						Debug.Log (PerformList[j].Attacker + " win, " + PerformList[i].Attacker + " lose");
						if (PerformList[i].AttackGameObject == GameObject.FindWithTag("Player"))
						{
							PerformList[i].AttackGameObject.GetComponent<Player_statemachine>().player.LeftHand_state = false;
                            p1left.SetActive(false);
                            p1rock.SetActive(false);
                            p1paper.SetActive(false);
                            p1scissors.SetActive(false);
                            leftR.SetActive(false);
                            leftP.SetActive(false);
                            leftS.SetActive(false);
                        }
						else
						{
							PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.LeftHand_state = false;
                            if (PerformList[i].Attacker == "e1")
                            {
                                GameObject.Find("e1left").SetActive(false);
                                GameObject.Find("e1leftr").SetActive(false);
                                GameObject.Find("e1lefts").SetActive(false);
                                GameObject.Find("e1leftp").SetActive(false);
                            }
                            else if (PerformList[i].Attacker == "e2")
                            {
                                GameObject.Find("e2left").SetActive(false);
                                GameObject.Find("e2leftr").SetActive(false);
                                GameObject.Find("e2lefts").SetActive(false);
                                GameObject.Find("e2leftp").SetActive(false);
                            }
                            else if (PerformList[i].Attacker == "e3")
                            {
                                GameObject.Find("e3left").SetActive(false);
                                GameObject.Find("e3leftr").SetActive(false);
                                GameObject.Find("e3lefts").SetActive(false);
                                GameObject.Find("e3leftp").SetActive(false);
                            }
                        }
					} else {
						Debug.Log (PerformList[i].Attacker + " win, " + PerformList[j].Attacker + " lose");
						if (PerformList[j].AttackGameObject == GameObject.FindWithTag("Player"))
						{
							PerformList[j].AttackGameObject.GetComponent<Player_statemachine>().player.RightHand_state = false;
                            p1right.SetActive(false);
                            p1rock2.SetActive(false);
                            p1paper2.SetActive(false);
                            p1scissors2.SetActive(false);
                            rightP.SetActive(false);
                            rightR.SetActive(false);
                            rightS.SetActive(false);
                        }
						else
						{
							PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state = false;
                            if (PerformList[j].Attacker == "e1")
                            {
                                GameObject.Find("e1right").SetActive(false);
                                GameObject.Find("e1rightr").SetActive(false);
                                GameObject.Find("e1rightp").SetActive(false);
                                GameObject.Find("e1rights").SetActive(false);
                            }
                            else if (PerformList[j].Attacker == "e2")
                            {
                                GameObject.Find("e2right").SetActive(false);
                                GameObject.Find("e2rightr").SetActive(false);
                                GameObject.Find("e2rightp").SetActive(false);
                                GameObject.Find("e2rights").SetActive(false);
                            }
                            else if (PerformList[j].Attacker == "e3")
                            {
                                GameObject.Find("e3right").SetActive(false);
                                GameObject.Find("e3rightr").SetActive(false);
                                GameObject.Find("e3rightp").SetActive(false);
                                GameObject.Find("e3rights").SetActive(false);
                            }
                        }
					}
				}
			}
		}
		Debug.Log ("Battle End");
		}

	public static int howToWin(HandleTurn.janken source, HandleTurn.janken target){
		if (source == HandleTurn.janken.PAPER && target == HandleTurn.janken.ROCK) {
			return 1;
		}
		else if (source == HandleTurn.janken.ROCK && target == HandleTurn.janken.SCISSORS) {
			return 1;
		}
		else if (source == HandleTurn.janken.SCISSORS && target == HandleTurn.janken.PAPER) {
			return 1;
		}
		return 0;
	}
	}