using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowLineDrawer : MonoBehaviour
{
    private LineRenderer line;
    private GameObject startPoint;
    private Vector2 mousePos;
    private Vector2 startMousePos;
    public static bool isDrawing;

    // Start is called before the first frame update
    void Start()
    {
        isDrawing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && isDrawing)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            line.SetPosition(0, new Vector3(startMousePos.x, startMousePos.y, 0f));
            line.SetPosition(1, new Vector3(mousePos.x, mousePos.y, 0f));
        }


        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider && hit.collider.GetComponent<FlowStartpoint>())
            {
                startPoint = hit.collider.gameObject;
                line = startPoint.GetComponent<LineRenderer>();
                isDrawing = true;
                startMousePos = startPoint.transform.position;
            }
        }

        if (Input.GetMouseButtonUp(0) && isDrawing)
        {
            isDrawing = false;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider && hit.collider.gameObject.GetComponent<FlowEndpoint>() && !hit.collider.gameObject.GetComponent<FlowEndpoint>().startpoint && startPoint.GetComponent<FlowStartpoint>().node != hit.collider.GetComponent<FlowEndpoint>().node)
            {
                if (!startPoint.GetComponent<FlowStartpoint>().elseStartPoint)
                {
                    startPoint.GetComponent<FlowStartpoint>().node.nextNode = hit.collider.gameObject.GetComponent<FlowEndpoint>().node;
                }
                else
                {
                    ((ConditionNode)startPoint.GetComponent<FlowStartpoint>().node).elseNode = hit.collider.gameObject.GetComponent<FlowEndpoint>().node;
                }

                if (startPoint.GetComponent<FlowStartpoint>().endpoint)
                {
                    startPoint.GetComponent<FlowStartpoint>().endpoint.startpoint = null;
                    startPoint.GetComponent<FlowStartpoint>().endpoint = null;
                }

                startPoint.GetComponent<FlowStartpoint>().endpoint = hit.collider.gameObject.GetComponent<FlowEndpoint>();
                hit.collider.gameObject.GetComponent<FlowEndpoint>().startpoint = startPoint.GetComponent<FlowStartpoint>();
            }
            else
            {
                if (hit.collider && hit.collider.gameObject.GetComponent<FlowEndpoint>() && startPoint.GetComponent<FlowStartpoint>().node == hit.collider.GetComponent<FlowEndpoint>().node)
                {
                    Console._instance.errorMessage("You cannot connect a node to itself");
                }

                if (hit.collider && hit.collider.gameObject.GetComponent<FlowEndpoint>() && hit.collider.gameObject.GetComponent<FlowEndpoint>().startpoint)
                {
                    Console._instance.errorMessage("This endpoint is already connected");
                }

                if (startPoint)
                {
                    if (!startPoint.GetComponent<FlowStartpoint>().elseStartPoint)
                    {
                        startPoint.GetComponent<FlowStartpoint>().node.nextNode = null;
                    }
                    else
                    {
                        ((ConditionNode)startPoint.GetComponent<FlowStartpoint>().node).elseNode = null;
                    }

                    if (startPoint.GetComponent<FlowStartpoint>().endpoint)
                    {
                        startPoint.GetComponent<FlowStartpoint>().endpoint.startpoint = null;
                        startPoint.GetComponent<FlowStartpoint>().endpoint = null;
                    }

                    startPoint = null;
                }
                if (line)
                {
                    line.SetPosition(0, new Vector3(0f, 0f, 0f));
                    line.SetPosition(1, new Vector3(0f, 0f, 0f));
                }
            }

            line = null;
        }
    }
}
