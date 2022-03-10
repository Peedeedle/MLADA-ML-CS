using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OGSCEnemyManager : MonoBehaviour
{

    //
    public GameObject enemy1;

    //
    public GameObject enemy2;

    //
    public GameObject enemy3;

    //
    public GameObject enemy4;

    //
    public GameObject enemy5;

    public void Update() {
        
        //
        if (!enemy1 && !enemy2 && !enemy3 && !enemy4 && !enemy5) {

            //
            enemy1.SetActive(true);

            //
            enemy2.SetActive(true);

            //
            enemy3.SetActive(true);

            //
            enemy4.SetActive(true);

            //
            enemy5.SetActive(true);

        }
        
    }

}
