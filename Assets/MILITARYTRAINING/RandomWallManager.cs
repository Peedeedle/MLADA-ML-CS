////////////////////////////////////////////////////////////
// File: RandomWallManager.cs
// Author: Jack Peedle
// Date Created: 09/03/22
// Last Edited By: Jack Peedle
// Date Last Edited: 09/03/22
// Brief: 
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWallManager : MonoBehaviour {

    //
    public MTAGENT mtAgent;

    

    //
    public Transform BoxWallPosition1;

    //
    public GameObject[] wallsArray;

    //
    public int randomWallInt;

    //
    //public GameObject ZeroOneZero;

    //
    //public GameObject ZeroZeroOne;

    // Start is called before the first frame update
    void Start() {

        //
        //randomWallInt = Random.Range(0, wallsArray.Length);

        //
        //Instantiate(wallsArray[randomWallInt], BoxWallPosition1.position, BoxWallPosition1.rotation);

    }

    public void RandomizeInt() {

        wallsArray[0].SetActive(false);
        wallsArray[1].SetActive(false);
        wallsArray[2].SetActive(false);

        //
        randomWallInt = Random.Range(0, wallsArray.Length);


        //
        //Instantiate(wallsArray[randomWallInt], BoxWallPosition1.position, BoxWallPosition1.rotation);

        wallsArray[randomWallInt].SetActive(true);

        

        //ringToFollow = wallsArray[randomWallInt].gameObject.GetComponentsInChildren<MTRINGS>();

        //ringToFollow = wallsArray[randomWallInt].gameObject.transform.Find("SM_Prop_Plane_Ring_01");

        //ringToFollow = wallsArray[randomWallInt].transform.gameObject.GetChild(randomWallInt);

        //GameObject RingGameobject = wallsArray[randomWallInt].gameObject.transform.gameObject.GetComponentInChildren<MTRINGS>();

        //wallsArray[randomWallInt].gameObject.transform.Find("thiss");




    }

    // Update is called once per frame
    void Update() {

        

    }
}
