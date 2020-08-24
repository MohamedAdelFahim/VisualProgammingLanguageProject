using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

public class ValueNode : Node
{
    public int number;
    public TMPro.TMP_InputField inputField;
    virtual public int Out()
    {
        Debug.Log("ValueNode hi");
        return number;
    }

    public void updateNumber(string inputFieldString)
    {
        if (!String.IsNullOrEmpty(inputFieldString))
        {
            number = Int32.Parse(inputFieldString);
        }
    }

    public void setZeroIfEmpty() {
        if (String.IsNullOrEmpty(inputField.text))
        {
            number = 0;
            inputField.text = "0";
        }
    }

    public override void operate() { }
}