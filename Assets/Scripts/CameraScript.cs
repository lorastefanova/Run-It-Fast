using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;    // A variable that stores a reference to our Player
    public Vector3 offset;      // A variable that allows us to offset the position (x, y, z)
    private Vector3 moveVector;

    // Variables used for camera animaton at the start of the game
    private float transition = 0.0f;
    private float animationDuration = 2.0f;
    private Vector3 animationOffset = new Vector3(0, 5, 5);

    // Update is called once per frame
    private void Update()
    {
        moveVector = player.position + offset; 

        // X 
        moveVector.x = 0;

        // Y
        moveVector.y = Mathf.Clamp(moveVector.y, 3, 5);

        if (transition > 1.0f)
        {
            transform.position = moveVector; // Set our position to the players position and offset it
        }
        else
        {
            // Camera animation at the start of the game
            transform.position = Vector3.Lerp(moveVector + animationOffset, moveVector, transition);
            transition += Time.deltaTime * 1 / animationDuration;
            transform.LookAt(player.position + Vector3.up);
        }
    }
}
