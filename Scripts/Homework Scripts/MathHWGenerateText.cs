using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MathHWGenerateText : MonoBehaviour {

	int answer;

	public int maxNum;

	void Start () {
		maxNum = GameObject.Find ("GameController").GetComponent<GameController> ().maxNumberAccess;
		string[] ops = { "+", "-", "x", "/" };

		int int1 = Random.Range (0, maxNum);
		int int2 = Random.Range (0, maxNum);
		int int3 = Random.Range (0, maxNum);
		int int4 = Random.Range (0, maxNum);
		int int5;
		int int6;

		string op1;
		string op2;
		string op3;

		if ( int2 != 0  && int1 % int2 == 0 ) {
			op1 = ops [Random.Range (0, 3)];
		} else {
			op1 = ops [Random.Range (0, 2)];
		}
		int5 = operate (int1, int2, op1);
					
		if (int4 != 0  && int3 % int4 == 0) {
			op3 = ops [Random.Range (0, 3)];
		} else {
			op3 = ops [Random.Range (0, 2)];
		}
		int6 = operate (int3, int4, op3);

		if (int6 !=0  && int5 % int6 == 0) {
			op2 = ops [Random.Range (0, 3)];
		} else {
			op2 = ops [Random.Range (0, 2)];
		}
		answer = operate (int5, int6, op2);

		string text = "(" + int1 + op1 + int2 + ")" + op2 + "(" + int3 + op3 + int4 + ")";





		//Text display = transform.parent.GetChild (transform.GetSiblingIndex () + 1).gameObject.GetComponent<Text>();
		//Debug.Log (display);
		GetComponent<Text> ().text = text;

	}

	int operate(int one, int two, string operation)
	{
		switch (operation) {
		case "+":
			return one + two;
		case "-":
			return one - two;
		case "x":
			return one * two;
		case "/":
			return one / two;
		}
		return -1;
	}

	public string getAnswer() {
		return answer.ToString();
	}

}
