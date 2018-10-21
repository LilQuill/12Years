using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//THIS CLASS WILL BE ON ALL HOMEWORK PREFABS

public class HomeworkManager : MonoBehaviour {



    HomeworkInterface OwO;

	GameObject graCon;
	GradeController gc;
	
	int[] toCalcScores = new int[2]; //This holds the number correct and the number of questions for homework


    public void Start()
    {
        OwO = GameObject.Find("HomeworkInterface").GetComponent<HomeworkInterface>();

		OwO = transform.parent.gameObject.GetComponent<HomeworkInterface>();

		graCon = GameObject.Find("Grade");
		gc = (GradeController)graCon.GetComponent(typeof(GradeController));
	}
	
    

	public void checkDone(string output) {
		string copied = " ";
		//GameObject copiedText = transform.parent.GetChild (1).gameObject;
		//Debug.Log ((MathHWGenerateText)copiedText.GetComponent ("MathHWGenerateText"));
		
		MathHWGenerateText math = transform.GetChild (1).GetChild(0).GetChild(1).gameObject.GetComponent<MathHWGenerateText>();
		EngHWGenerateText eng = transform.GetChild (1).GetChild(0).GetChild (1).gameObject.GetComponent<EngHWGenerateText> ();

		if (eng == null) {
			copied = math.getAnswer ();
			Debug.Log ("math " +copied);
			checkHWCorrect(math, output);
			displayLetterGrade();
		} else {
			copied = eng.getAnswer ();
			Debug.Log ("eng: "+copied);
			checkHWCorrect(eng, output);
			displayLetterGrade();
		}
		
		OwO.hwSubmitted(true);
		
		GameObject copiedText = transform.parent.GetChild(1).gameObject;
	}

	//returns the array that holds the score and the number of questions
	public int[] returnScoreArray()
	{
		return toCalcScores;
	}
	
	
	public void displayLetterGrade()
	{
		
		
		//Debug.Log ("called displayLetterGrade in Typable");
		
		int percent = calculateScore (toCalcScores [0], toCalcScores [1]);
		//Debug.Log ("percent: " + percent);
		if (OwO.HWLeft > 1 || BackgroundStats.getStress() >= 100) {
			char letterGrade = calculateLetterGrade (percent);
			Debug.Log (letterGrade);
			
			gc.showGrade (letterGrade);
			//Debug.Log ("ran GradeController from Typable");
		}
	}

	void checkHWCorrect(EngHWGenerateText engHW, string answer) //ENGLISH
	{
		int score = 0;
		int totalAnswers = 0;
		
		
		string temp = " " + answer + " "; //adding spaces here makes the for loop so easy and means less lines of conditionals
		totalAnswers = 5;
		score = 5;
		Debug.Log ("assignment length: "+ engHW.assignmentWords.Length);
		for (int x = 0; x < engHW.assignmentWords.Length; x++)
		{
			string search = " " + engHW.assignmentWords[x] + " ";

			if (!temp.Contains(search))
			{
				if(engHW.assignmentWords.Length == 1) {
					score = 2; //if the hw is one word, getting it wrong gets you a D
				} else if(engHW.assignmentWords.Length == 2 ) {
					if(score == 5)
						score = 3; //if the hw is 2 words, one wrong is a C, two wrong is an F
					else
						score = 1; 
				} else if(engHW.assignmentWords.Length == 3 && score == 5) {
					score = score-2; //if the hw is 3 words, A->C->D->F
				} else {
					score--;
				}
			}
		}

		/*
		Original grading algorithm, gets percent of words correct
		for (int x = 0; x < engHW.assignmentWords.Length; x++)
		{
			if (engHW.assignmentWords[x] != null) //assignmentWords is an array that holds all the words you have to copy
			{
				totalAnswers++;
				string search = " " + engHW.assignmentWords[x] + " ";
				
				if (temp.Contains(search))
				{
					score++;
				}
			}
		}
		*/
		toCalcScores[0] = score;
		toCalcScores[1] = totalAnswers;
		
		Debug.Log("eng hw score: " + score + "/" + totalAnswers);
	}
	
	void checkHWCorrect(MathHWGenerateText mathHW, string answer) //MATH
	{
		int score = 0;
		int totalAnswers = 5; //there's only ever 1 math question
		int correctAnswer = 0;
		int answerInt = 100;
			if(int.TryParse(mathHW.getAnswer(), out correctAnswer)&& //these two lines turn strings into ints.
					int.TryParse(answer, out answerInt)) { //It takes the string and then sets the int to the out variable. idk why it does this so weird.
			int difference = Mathf.Abs(correctAnswer-answerInt);
			if (difference == 0) {
				score = 5;
			} else if (difference < 3) {
				score = 4;
			} else if (difference < 6 || Mathf.Abs (correctAnswer) == Mathf.Abs (correctAnswer)) {
				score = 3;
			} else if (difference < 11) {
				score = 2;
			} else {
				score = 0;
			}
			Debug.Log (score);
		} else { //if the answer is not only numbers
			score = 0;
		}
		
		
		toCalcScores[0] = score;
		toCalcScores[1] = totalAnswers;
		
		Debug.Log("math hw score: " + score + "/" + totalAnswers);

	}
	
	//calculates a percent score on homework.
	public int calculateScore(int score, int totalAnswers)
	{
		if (totalAnswers != 0)
		{
			//Debug.Log((int)((score * 100) / totalAnswers));
            return (int)((score * 100) / totalAnswers);
        }
        else //this shouldn't happen!!!
        {
			Debug.Log("a mistake was made and sakshi is a fool");
			return -1;
        }
    }

    public char calculateLetterGrade(int percent)
    {
        if (percent >= 90)
        {
            return 'a';
        }
        else if (percent >= 80)
        {
            BackgroundStats.changeStress(3);
            return 'b';
        }
        else if (percent >= 60)
        {
            BackgroundStats.changeStress(7);
            return 'c';
        }
        else if (percent >= 40)
        {
            BackgroundStats.changeStress(10);
            return 'd';
        }
        else
        {
            BackgroundStats.changeStress(15);
            return 'f';
        }
    }

}
