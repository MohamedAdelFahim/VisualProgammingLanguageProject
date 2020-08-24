using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndNode : Node
{
    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            active = false;

            Debug.Log("End Node");
        }
    }

    public override void operate()
    {
        if (Node.startNodes.Count == 0)
        {
            active = true;
        }
        else
        {
            Console._instance.errorMessage("Invalid program: " + Node.startNodes.Peek().GetType().Name + "'s endpoint not connected after it");
        }
    }
}
