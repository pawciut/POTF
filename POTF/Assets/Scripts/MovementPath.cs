﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class MovementPath : MonoBehaviour
{
    
    public PathTypes PathType; //Indicates type of path (Linear or Looping)
    public int movementDirection = 1; //1 clockwise/forward || -1 counter clockwise/backwards
    public Transform[] PathSequence; //Array of all points in the path
    

    // (Unity Named Methods)
    //Update is called by Unity every frame
    void Update()
    {

    }

    //OnDrawGizmos will draw lines between our points in the Unity Editor
    //These lines will allow us to easily see the path that
    //our moving object will follow in the game
    public void OnDrawGizmos()
    {
        //Make sure that your sequence has points in it
        //and that there are at least two points to constitute a path
        if(PathSequence == null || PathSequence.Length < 2)
        {
            return; //Exits OnDrawGizmos if no line is needed
        }

        //Loop through all of the points in the sequence of points
        for(var i=1; i < PathSequence.Length; i++)
        {
            //Draw a line between the points
            Gizmos.DrawLine(PathSequence[i - 1].position, PathSequence[i].position);
        }

        //If your path loops back to the begining when it reaches the end
        if(PathType == PathTypes.loop)
        {
            //Draw a line from the last point to the first point in the sequence
            Gizmos.DrawLine(PathSequence[0].position, PathSequence[PathSequence.Length-1].position);
        }
    }
    

    //Coroutines run parallel to other fucntions

}
