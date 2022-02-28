////////////////////////////////////////////////////////////
// File: Enemy.cs
// Author: Jack Peedle
// Date Created: 27/02/22
// Last Edited By: Jack Peedle
// Date Last Edited: 27/02/22
// Brief: 
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{

    //
    public int startingHealth = 100;

    //
    private int currentHealth;

    //
    private Vector3 startPosition;

    //
    private void Start() {

        //
        startPosition = transform.position;

        //
        currentHealth = startingHealth;

    }

    //
    public void GetShot(int damage) {

        //
        currentHealth -= damage;

        //
        if (currentHealth <= 0) {

            //
            Die();

        }

        //
        //ApplyDamage(damage);

    }

    //
    private void ApplyDamage(int damage) {

        //
        currentHealth -= damage;

        //
        if (currentHealth <= 0) {

            //
            Die();

        }


    }

    //
    private void Die() {

        //
        Debug.Log("I Died");

        //
        Respawn();

    }

    //
    private void Respawn() {

        //
        currentHealth = startingHealth;

        //
        transform.position = startPosition;

    }

    //
    private void OnMouseDown() {

        //
        GetShot(startingHealth);

    }


}
