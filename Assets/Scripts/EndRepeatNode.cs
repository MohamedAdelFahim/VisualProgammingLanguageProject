using UnityEngine;
using UnityEditor;
using System.Collections;

public class EndRepeatNode : Node
{
    public Node repeatStartNode;
    public Node repeatNode;
    public int counter;
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

            Debug.Log("EndRepeat Node");
            if (repeatStartNode == null)
            {
                throw new System.Exception("No repeat node");
            }

            counter--;

            if (counter > 0)
            {
                Debug.Log("Iteration " + (counter + 1));
                Node.startNodes.Push(repeatNode);
                repeatStartNode.Run();
            }
            else
            {
                Debug.Log("Exiting Repeat");
                if (nextNode == null)
                {
                    throw new System.Exception("No next node");
                }

                nextNode.Run();
            }
        }
    }

    public override void operate()
    {
        if (Node.startNodes.Count != 0 && (Node.startNodes.Pop() as Node).nextNode == repeatStartNode)
        {
            active = true;
        }
        else
        {
            Console._instance.errorMessage("Invalid program: " + this.GetType().Name + " does not follow the right " + typeof(RepeatNode).Name);
        }
    }
}