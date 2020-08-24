using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNode : Node
{
    public bool active;
    
    // Start is called before the first frame update
    void Start()
    {
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active) {
            active = false;
            nextNode.Run();
        }
    }

    public override void operate(){
        Node.startNodes = new Stack();
        active = true;
    }
}
