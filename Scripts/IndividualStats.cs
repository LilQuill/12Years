using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualStats {// : MonoBehaviour {

    //fields
    private int stat;
    private int change; //Having a change field means Snazzy progress bars

    //constructor
    public IndividualStats ()
    {
        stat = 50;
        change = 0;
    }
    //constructor for anything that doesn't start at 50
    public IndividualStats (int x)
    {
        stat = x;
        change = 0;
    }


    //getter methods
    public int getStat()
    {
        return stat;
    }

    public int getChange()
    {
        return change;
    }


    //setter methods
    public void changeStat(int x)
    {
        stat += x;
    }

    public void setStat()
    {
        stat += change;
        change = 0;
    }

	public void sakshiIsAFool(int x) { //sets variable
		stat = x;
	}

    //makes sure stress doesn't go beyond 0 or 100
    public void sakshiCantRead()
    {
        if ((stat+change) > 100)
        {
            sakshiIsAFool(100);
        }
        else if ((stat+change) < 0)
        {
            sakshiIsAFool(0);
        }
    }

    


    /*//Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
	}*/
}
