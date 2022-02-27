////////////////////////////////////////////////////////////
// File: OGSCAgent.cs
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

public class OGSCAgent : Agent{

    //
    public Transform shootingPoint;

    //
    public int minStepsBetweenShots = 50;

    //
    public int damage = 100;

    //
    private bool bShotAvailable = true;

    //
    private int stepsUntilShotAvailable = 0;

    //
    private Vector3 startingPosition;

    //
    private Rigidbody rb;

    //
    private void Shoot() {

        //
        if (!bShotAvailable) {

            //
            return;

        }

        //
        var layerMask = 1 << LayerMask.NameToLayer("Enemy");

        //
        var direction = transform.forward;

        //
        Debug.Log("Shot");

        //
        Debug.DrawRay(shootingPoint.position, direction * 200f, Color.green, 2f);

        //
        if (Physics.Raycast(shootingPoint.position, direction, out var hit, 200f, layerMask)) {

            //
            hit.transform.GetComponent<Enemy>().GetShot(damage, this);

        }

        //
        bShotAvailable = false;

        //
        stepsUntilShotAvailable = minStepsBetweenShots;


    }


    //
    private void FixedUpdate() {

        //
        if (!bShotAvailable) {

            //
            stepsUntilShotAvailable--;

            //
            if (stepsUntilShotAvailable <= 0)
                bShotAvailable = true;

        }

    }

    //
    public override void OnActionReceived(float[] vectorAction) {

        //
        if (Mathf.RoundToInt(vectorAction[0]) >= 1) {

            //
            Shoot();

        }

    }

}
