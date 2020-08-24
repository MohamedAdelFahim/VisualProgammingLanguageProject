using UnityEngine;
using UnityEditor;
using System.Collections;

public class EndConditionNode : Node
{
    public ConditionNode conditionNode;
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

            nextNode.Run();

        }
    }

    public override void operate()
    {
        if (Node.startNodes.Count != 0 && (Node.startNodes.Pop() as Node) == conditionNode)
        {
            active = true;
        }
        else
        {
            Console._instance.errorMessage("Invalid program: " + this.GetType().Name + " does not follow the right " + typeof(ConditionNode).Name);
        }
    }
}