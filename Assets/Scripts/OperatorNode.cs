using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

public enum Operator
{
    Plus,
    Minus,
    Multiply,
    IntegerDivision
}
public class OperatorNode : ValueNode, InputInterface
{
    public Operator oprtr;
    public ValueNode input1 { get; set; }
    public ValueNode input2 { get; set; }

    override public int Out()
    {
        if (!input1 || !input2)
        {
            Console._instance.errorMessage("Please connect the both inputs on " + this.GetType().Name);
            throw new SystemException("Missing inputs on operator node");
        }

        switch (oprtr)
        {
            case Operator.Plus:
                {
                    Debug.Log("Adding " + input1.Out());
                    Debug.Log(" + ");
                    Debug.Log(input2.Out());
                    return input1.Out() + input2.Out();
                }
            case Operator.Minus:
                {
                    return input1.Out() - input2.Out();
                }
            case Operator.Multiply:
                {
                    return input1.Out() * input2.Out();
                }
            case Operator.IntegerDivision:
                {
                    if (input2.Out() == 0)
                    {
                        Console._instance.errorMessage("You cannot divide by zero");
                        throw new SystemException("Divide by zero on operator node");
                    }
                    return input1.Out() / input2.Out();
                }
            default:
                {
                    Debug.Log("Invalid Operator");
                    return 0;
                }
        }
    }

    public void updateOperator(int selection)
    {
        switch (selection)
        {
            case 0: oprtr = Operator.Plus; break;
            case 1: oprtr = Operator.Minus; break;
            case 2: oprtr = Operator.Multiply; break;
            case 3: oprtr = Operator.IntegerDivision; break;
            default: throw new SystemException("Invalid selection on operator node");
        }
    }
}

