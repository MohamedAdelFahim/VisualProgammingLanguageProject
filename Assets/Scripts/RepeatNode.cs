using UnityEngine;
using UnityEditor;
using System.Collections;

public class RepeatNode : Node, InputInterface
{
    private bool active;

    [HideInInspector]
    public int counter;
    public ValueNode input1 { get; set; }
    public ValueNode input2 { get; set; }
    public EndRepeatNode endRepeatNode;

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

            Debug.Log("Repeat Node");

            if (!input1)
            {
                Console._instance.errorMessage("Please connect the input on " + this.GetType().Name);
                return;
            }

            if (input1.Out() < 0)
            {
                Console._instance.errorMessage(this.GetType().Name + "'s input needs to be >= 0");
                return;
            }

            counter = input1.Out();

            if (endRepeatNode)
            {
                endRepeatNode.counter = counter;
                endRepeatNode.repeatStartNode = nextNode;
                endRepeatNode.repeatNode = this;
                if (counter <= 0)
                {
                    endRepeatNode.Run();
                }
                else if (nextNode)
                {
                    nextNode.Run();
                }
                else
                {
                    throw new System.Exception("No next node");
                }
            }
            else
            {
                throw new System.Exception("No end repeat node");
            }
        }
    }

    public override void operate()
    {
        Node.startNodes.Push(this);
        active = true;
    }
}