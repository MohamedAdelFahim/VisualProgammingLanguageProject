using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueLineDrawer : MonoBehaviour
{
    private LineRenderer line;
    private GameObject startpoint;
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
            if (hit.collider && hit.collider.GetComponent<ValueStartpoint>())
            {
                if (startpoint == hit.collider.gameObject && startpoint.GetComponent<ValueStartpoint>().endpoint)
                {
                    switch (startpoint.GetComponent<ValueStartpoint>().endpoint.type)
                    {
                        case ValueEndpoint.Type.input1:
                            (startpoint.GetComponent<ValueStartpoint>().endpoint.node as InputInterface).input1 = null;
                            break;
                        case ValueEndpoint.Type.input2:
                            (startpoint.GetComponent<ValueStartpoint>().endpoint.node as InputInterface).input2 = null;
                            break;
                    }
                }

                startpoint = hit.collider.gameObject;
                line = startpoint.GetComponent<LineRenderer>();
                isDrawing = true;
                startMousePos = startpoint.transform.position;
            }
        }

        if (Input.GetMouseButtonUp(0) && isDrawing)
        {
            isDrawing = false;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider && hit.collider.gameObject.GetComponent<ValueEndpoint>() && !hit.collider.gameObject.GetComponent<ValueEndpoint>().startpoint && startpoint.GetComponent<ValueStartpoint>().node != hit.collider.GetComponent<ValueEndpoint>().node)
            {
                if (startpoint.GetComponent<ValueStartpoint>().endpoint)
                {
                    startpoint.GetComponent<ValueStartpoint>().endpoint.startpoint = null;

                    switch (startpoint.GetComponent<ValueStartpoint>().endpoint.type)
                    {
                        case ValueEndpoint.Type.input1:
                            (startpoint.GetComponent<ValueStartpoint>().endpoint.node as InputInterface).input1 = null;
                            break;
                        case ValueEndpoint.Type.input2:
                            (startpoint.GetComponent<ValueStartpoint>().endpoint.node as InputInterface).input2 = null;
                            break;
                    }

                    startpoint.GetComponent<ValueStartpoint>().endpoint = null;
                }

                ValueEndpoint endpoint = hit.collider.gameObject.GetComponent<ValueEndpoint>();
                endpoint.startpoint = startpoint.GetComponent<ValueStartpoint>();
                startpoint.GetComponent<ValueStartpoint>().endpoint = endpoint;

                switch (endpoint.type)
                {
                    case ValueEndpoint.Type.input1:
                        (endpoint.node as InputInterface).input1 = startpoint.GetComponent<ValueStartpoint>().node;
                        break;
                    case ValueEndpoint.Type.input2:
                        (endpoint.node as InputInterface).input2 = startpoint.GetComponent<ValueStartpoint>().node;
                        break;
                }
            }
            else
            {
                if (hit.collider && hit.collider.gameObject.GetComponent<ValueEndpoint>() && startpoint.GetComponent<ValueStartpoint>().node == hit.collider.GetComponent<ValueEndpoint>().node)
                {
                    Console._instance.errorMessage("You cannot connect a node to itself");
                }

                if (hit.collider && hit.collider.gameObject.GetComponent<ValueEndpoint>() && hit.collider.gameObject.GetComponent<ValueEndpoint>().startpoint)
                {
                    Console._instance.errorMessage("This endpoint is already connected");
                }

                if (startpoint.GetComponent<ValueStartpoint>().endpoint)
                {
                    startpoint.GetComponent<ValueStartpoint>().endpoint.startpoint = null;

                    switch (startpoint.GetComponent<ValueStartpoint>().endpoint.type)
                    {
                        case ValueEndpoint.Type.input1:
                            (startpoint.GetComponent<ValueStartpoint>().endpoint.node as InputInterface).input1 = null;
                            break;
                        case ValueEndpoint.Type.input2:
                            (startpoint.GetComponent<ValueStartpoint>().endpoint.node as InputInterface).input2 = null;
                            break;
                    }

                    startpoint.GetComponent<ValueStartpoint>().endpoint = null;
                }

                if (line)
                {
                    line.SetPosition(0, new Vector3(0f, 0f, 0f));
                    line.SetPosition(1, new Vector3(0f, 0f, 0f));
                }
            }

            line = null;

            startpoint = null;
        }
    }
}
