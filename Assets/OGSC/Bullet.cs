////////////////////////////////////////////////////////////
// File: Bullet.cs
// Author: Jack Peedle
// Date Created: 27/02/22
// Last Edited By: Jack Peedle
// Date Last Edited: 27/02/22
// Brief: 
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{

    //
    public int damage = 100;

    //
    public void OnTriggerEnter(Collider other) {

        //
        if (other.TryGetComponent<TAgent>(out TAgent tAgent)) {

            //
            

        }

    }

}
