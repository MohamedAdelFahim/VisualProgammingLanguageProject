using UnityEngine;
using UnityEditor;
using System.Collections;

public class PrintNode : Node, InputInterface
{
    public ValueNode input1 { get; set; }
    public ValueNode input2 { get; set; }
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

            if (!input1)
            {
                Console._instance.errorMessage("Please connect the input on " + this.GetType().Name);
                return;
            }

            Console._instance.appendText(input1.Out() + "");

            nextNode.Run();
        }
    }

    public override void operate()
    {
        active = true;
    }
}
