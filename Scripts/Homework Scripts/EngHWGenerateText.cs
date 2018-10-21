using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class EngHWGenerateText : MonoBehaviour {

	public int wordCountLowerBound;
	public int wordCountUpperBound;
	string text;
	//string content;
	public string[] assignmentWords; //the words that show up on the homework sprite that get copied
	
	// Use this for initialization
	void Start()
	{
		GameController controller = GameObject.Find("GameController").GetComponent<GameController>();
		wordCountLowerBound = controller.minWordsAccess;
		wordCountUpperBound = controller.maxWordsAccess;
		//setContent();
		//what was here previously is in the willIsAnIdiot() method now
		willIsAnIdiot();
		
	}
	void willIsAnIdiot()
	{
		//sets length of assignmentWords to the max words

		
		int wordCount = Random.Range(wordCountLowerBound, wordCountUpperBound);
		assignmentWords = new string[wordCount];
		string path;
		FileStream fs;
		//this is for paths to be different
		try {

			path = Application.dataPath + "/Assets/Resources/WordsLvl2.txt"; //for the build
			fs = new FileStream(path, FileMode.Open);
		} catch {
			path = "Assets/Resources/WordsLvl2.txt"; //for testing in unity
			fs = new FileStream(path, FileMode.Open);
		}
		string content = "";
		using (StreamReader read = new StreamReader(fs, true))
		{
			content = read.ReadToEnd();
			
		}
		
		string[] words = Regex.Split(content, "\r\n?|\n", RegexOptions.Singleline);
		
		int currentWordIndex = 0;
		Debug.Log (words);
		text = "";
		for (int i = 0; i < wordCount; i++)
		{
			currentWordIndex = Random.Range(0, words.Length - 1);
			text += words[currentWordIndex];
			assignmentWords[i] = words[currentWordIndex]; //adds the word that was added to the string to the array
			//Debug.Log(assignmentWords[i]);
			if (i != wordCount - 1)
				text += " ";
		}
		
		//Text display = transform.parent.GetChild (C:\Users\saksh\Documents\12 Years a Student\Assets\Scripts\EngHWGenerateText.cstransform.GetSiblingIndex () + 1).gameObject.GetComponent<Text>();
		//Debug.Log (display);
		GetComponent<Text>().text = text;
		
		
	}
	
	public string getAnswer()
	{
		return text;
	}
	
	private void setContent()
	{
		//content = "";
	}
}
