using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class MovementPath : MonoBehaviour
{
    #region Enums
    public enum PathTypes //Types of movement paths
    {
        linear,
        loop
    }
    #endregion //Enums
    
    public PathTypes PathType; //Indicates type of path (Linear or Looping)
  
    public Transform[] PathSequence; //Array of all points in the path

    //OnDrawGizmos will draw lines between our points in the Unity Editor
    public void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeGameObject != this.gameObject) return;

        //Make sure that your sequence has points in it
        //and that there are at least two points to constitute a path
        if (PathSequence == null || PathSequence.Length < 2)
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
  
}
