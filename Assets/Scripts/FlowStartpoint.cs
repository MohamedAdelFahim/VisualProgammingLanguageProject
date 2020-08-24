using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowStartpoint : MonoBehaviour
{
    public Node node;
    public FlowEndpoint endpoint;
    public bool elseStartPoint;
    public LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (!FlowLineDrawer.isDrawing)
        {
            if (endpoint)
            {
                line.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 0f));
                line.SetPosition(1, new Vector3(endpoint.transform.position.x, endpoint.transform.position.y, 0f));
            }
            else
            {
                line.SetPosition(0, new Vector3(0f, 0f, 0f));
                line.SetPosition(1, new Vector3(0f, 0f, 0f));
            }
        }
    }
}
