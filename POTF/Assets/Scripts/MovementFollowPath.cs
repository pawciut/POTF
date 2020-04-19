using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovementFollowPath : MonoBehaviour
{
    public enum MovementType  //Type of Movement
    {
        MoveTowards,
        LerpTowards
    }
    
    public MovementType Type = MovementType.MoveTowards; // Movement type used
    public MovementPath MyPath; // Reference to Movement Path Used
    public float Speed = 1; // Speed object is moving
    public float MaxDistanceToGoal = .1f; // How close does it have to be to the point to be considered at point

    public int movingTo = 0; //used to identify point in PathSequence we are moving to

    bool HasStarted;
    public bool AutoStart;

    private IEnumerator<Transform> pointInPath; //Used to reference points returned from MyPath.GetNextPathPoint

    // (Unity Named Methods)
    public void Start()
    {
        if (AutoStart)
            StartMovement();
    }

    public void StartMovement()
    {
        //Make sure there is a path assigned
        if (MyPath == null)
        {
            Debug.LogError("Movement Path cannot be null, I must have a path to follow.", gameObject);
            return;
        }

        //Sets up a reference to an instance of the coroutine GetNextPathPoint
        pointInPath = GetNextPathPoint();
        Debug.Log(pointInPath.Current);
        //Get the next point in the path to move to (Gets the Default 1st value)
        pointInPath.MoveNext();
        Debug.Log(pointInPath.Current);

        //Make sure there is a point to move to
        if (pointInPath.Current == null)
        {
            Debug.LogError("A path must have points in it to follow", gameObject);
            return; //Exit Start() if there is no point to move to
        }

        //Set the position of this object to the position of our starting point
        transform.position = Fixed2D(pointInPath.Current.position);

        HasStarted = true;
    }

    public void StopMovement()
    {
        HasStarted = false;
    }
     
    //Update is called by Unity every frame
    public void Update()
    {
        if (!HasStarted)
            return;


        //Validate there is a path with a point in it
        if (pointInPath == null || pointInPath.Current == null)
        {
            return; //Exit if no path is found
        }

        if (Type == MovementType.MoveTowards) //If you are using MoveTowards movement type
        {
            //Move to the next point in path using MoveTowards
            transform.position =
                Vector3.MoveTowards(transform.position,
                                    Fixed2D(pointInPath.Current.position),
                                    Time.deltaTime * Speed);
        }
        else if (Type == MovementType.LerpTowards) //If you are using LerpTowards movement type
        {
            //Move towards the next point in path using Lerp
            transform.position = Vector3.Lerp(transform.position,
                                                Fixed2D(pointInPath.Current.position),
                                                Time.deltaTime * Speed);
        }

        //Check to see if you are close enough to the next point to start moving to the following one
        //Using Pythagorean Theorem
        //per unity suaring a number is faster than the square root of a number
        //Using .sqrMagnitude 
        var distanceSquared = (Fixed2D(transform.position) - Fixed2D(pointInPath.Current.position)).sqrMagnitude;
        if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal) //If you are close enough
        {
            pointInPath.MoveNext(); //Get next point in MovementPath
        }
        //The version below uses Vector3.Distance same as Vector3.Magnitude which includes (square root)
        /*
        var distanceSquared = Vector3.Distance(transform.position, pointInPath.Current.position);
        if (distanceSquared < MaxDistanceToGoal) //If you are close enough
        {
            pointInPath.MoveNext(); //Get next point in MovementPath
        }
        */
    }


    //GetNextPathPoint() returns the transform component of the next point in our path
    //FollowPath.cs script will inturn move the object it is on to that point in the game
    public IEnumerator<Transform> GetNextPathPoint()
    {
        var movementDirection = MyPath.movementDirection;//TODO:ale w sumie moglbym to tutaj ustawiac i wtedy by byla potrzebna tylko jedna sciezka

        //Make sure that your sequence has points in it
        //and that there are at least two points to constitute a path
        if (MyPath.PathSequence == null || MyPath.PathSequence.Length < 1)
        {
            yield break; //Exits the Coroutine sequence length check fails
        }

        while (true) //Does not infinite loop due to yield return!!
        {
            //Return the current point in PathSequence
            //and wait for next call of enumerator (Prevents infinite loop)
            yield return MyPath.PathSequence[movingTo];
            //*********************************PAUSES HERE******************************************************//
            //If there is only one point exit the coroutine
            if (MyPath.PathSequence.Length == 1)
            {
                continue;
            }

            //If Linear path move from start to end then end to start then repeat
            if (MyPath.PathType == PathTypes.linear)
            {
                //If you are at the begining of the path
                if (movingTo <= 0)
                {
                    movementDirection = 1; //Seting to 1 moves forward
                }
                //Else if you are at the end of your path
                else if (movingTo >= MyPath.PathSequence.Length - 1)
                {
                    movementDirection = -1; //Seting to -1 moves backwards
                }
            }

            movingTo = movingTo + movementDirection;
            //movementDirection should always be either 1 or -1
            //We add direction to the index to move us to the
            //next point in the sequence of points in our path


            //For Looping path you must move the index when you reach 
            //the begining or end of the PathSequence to loop the path
            if (MyPath.PathType == PathTypes.loop)
            {
                //If you just moved past the last point(moving forward)
                if (movingTo >= MyPath.PathSequence.Length)
                {
                    //Set the next point to move to as the first point in sequence
                    movingTo = 0;
                }
                //If you just moved past the first point(moving backwards)
                if (movingTo < 0)
                {
                    //Set the next point to move to as the last point in sequence
                    movingTo = MyPath.PathSequence.Length - 1;
                }
            }
        }
    }


    Vector3 Fixed2D(Vector3 vector)
    {
        return new Vector3(vector.x, vector.y, 0);
    }
}
