using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

public enum Condition
{
    LessThan,
    GreaterThan,
    Equal
}

public class ConditionNode : Node, InputInterface
{
    public Node elseNode;
    public Condition condition;
    public ValueNode input1 { get; set; }
    public ValueNode input2 { get; set; }
    public EndConditionNode endConditionNode;
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

            if (!elseNode)
            {
                Console._instance.errorMessage("Please connect the else node on " + this.GetType().Name);
                return;
            }

            if (!input1 || !input2)
            {
                Console._instance.errorMessage("Please connect the both inputs on " + this.GetType().Name);
                return;
            }

            bool boolean = false;
            switch (condition)
            {
                case Condition.LessThan:
                    {
                        boolean = input1.Out() < input2.Out();
                        break;
                    }
                case Condition.GreaterThan:
                    {
                        boolean = input1.Out() > input2.Out();
                        break;
                    }
                case Condition.Equal:
                    {
                        boolean = input1.Out() == input2.Out();
                        break;
                    }
            }

            if (boolean)
            {
                nextNode.Run();
            }
            else
            {
                elseNode.Run();
            }
        }
    }

    public void updateCondition(int selection)
    {
        switch (selection)
        {
            case 0: condition = Condition.LessThan; break;
            case 1: condition = Condition.GreaterThan; break;
            case 2: condition = Condition.Equal; break;
            default: throw new SystemException("Invalid selection on condition node");
        }
    }

    public override void operate()
    {
        Node.startNodes.Push(this);
        active = true;
    }
}