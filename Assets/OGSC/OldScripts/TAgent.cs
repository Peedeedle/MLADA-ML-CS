////////////////////////////////////////////////////////////
// File: TAgent.cs
// Author: Jack Peedle
// Date Created: 27/02/22
// Last Edited By: Jack Peedle
// Date Last Edited: 27/02/22
// Brief: 
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class TAgent : Agent{

    //
    public int startingHealth = 100;

    //
    private int currentHealth;

    //
    public void OnTriggerEnter(Collider other) {

        //
        if (other.TryGetComponent<Bullet>(out Bullet bullet)) {

            //
            EndEpisode();

        }

    }

    //
    public override void OnEpisodeBegin() {

        //
        currentHealth = startingHealth;

        //
        transform.localPosition = new Vector3(Random.Range(-3f, +3f), 0, Random.Range(0.5f, 3f));

    }

}
