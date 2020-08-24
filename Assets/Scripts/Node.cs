using UnityEngine;
using UnityEditor;
using System.Collections;

public abstract class Node : MonoBehaviour
{
    public Node nextNode;
    public static Stack startNodes;

    public void Run()
    {
        if (nextNode == null && this.GetType() != typeof(EndNode))
        {
            Console._instance.errorMessage("Please connect the next node on " + this.GetType().Name);
        }
        else
        {
            this.operate();
        }
    }
    public abstract void operate();

}