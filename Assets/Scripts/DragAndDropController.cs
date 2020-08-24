using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class DragAndDropController : MonoBehaviour
{
    public CircleCollider2D[] points;
    public bool deletable = true;
    public bool isOrigin = true;
    public GameObject endNodePrefab = null;
    private GameObject endNode = null;
    bool isDragged = false;
    private GameObject clone = null;


    // Update is called once per frame
    void Update()
    {
        if (isDragged)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (endNode != null)
            {
                endNode.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector2(3, 0);
            }
        }
    }

    private void OnMouseOver()
    {
        if (isOrigin && Input.GetMouseButtonDown(0))
        {
            clone = Instantiate(this.gameObject);
            if (endNodePrefab != null && endNode == null)
            {
                clone.GetComponent<DragAndDropController>().endNode = Instantiate(endNodePrefab);
                if (clone.GetComponent<RepeatNode>() != null)
                {
                    clone.GetComponent<DragAndDropController>().endNode.GetComponent<EndRepeatNode>().repeatStartNode = clone.GetComponent<RepeatNode>();
                    clone.GetComponent<RepeatNode>().endRepeatNode = clone.GetComponent<DragAndDropController>().endNode.GetComponent<EndRepeatNode>();
                }
                if (clone.GetComponent<ConditionNode>() != null)
                {
                    clone.GetComponent<DragAndDropController>().endNode.GetComponent<EndConditionNode>().conditionNode = clone.GetComponent<ConditionNode>();
                    clone.GetComponent<ConditionNode>().endConditionNode = clone.GetComponent<DragAndDropController>().endNode.GetComponent<EndConditionNode>();
                }
            }
            foreach (CircleCollider2D collider in clone.GetComponent<DragAndDropController>().points)
            {
                collider.enabled = true;
            }
            clone.GetComponent<DragAndDropController>().isDragged = true;
            clone.GetComponent<DragAndDropController>().isOrigin = false;
        }
        else if (!isOrigin && Input.GetMouseButtonDown(0))
        {
            isDragged = true;
        }

        if (!isOrigin && deletable && Input.GetMouseButtonDown(1))
        {
            this.delete();
            if (GetComponent<RepeatNode>() != null)
            {
                GetComponent<RepeatNode>().endRepeatNode.GetComponent<DragAndDropController>().delete();
            }
            if (GetComponent<EndRepeatNode>() != null)
            {
                GetComponent<EndRepeatNode>().repeatNode.GetComponent<DragAndDropController>().delete();
            }
            if (GetComponent<ConditionNode>() != null)
            {
                GetComponent<ConditionNode>().endConditionNode.GetComponent<DragAndDropController>().delete();
            }
            if (GetComponent<EndConditionNode>() != null)
            {
                GetComponent<EndConditionNode>().conditionNode.GetComponent<DragAndDropController>().delete();
            }
        }
    }

    private void delete()
    {

        /*
        foreach (FlowStartpoint startpoint in gameObject.GetComponentsInChildren<FlowStartpoint>())
        {
            // Nothing to do here
        }
        */

        foreach (FlowEndpoint endpoint in gameObject.GetComponentsInChildren<FlowEndpoint>())
        {
            if (endpoint.startpoint)
            {
                if (!endpoint.startpoint.elseStartPoint)
                {
                    endpoint.startpoint.node.nextNode = null;
                }
                else
                {
                    (endpoint.startpoint.node as ConditionNode).elseNode = null;
                }
            }
        }

        foreach (ValueStartpoint startpoint in gameObject.GetComponentsInChildren<ValueStartpoint>())
        {
            if (startpoint.endpoint)
            {
                if (startpoint.endpoint.type == ValueEndpoint.Type.input1)
                {
                    (startpoint.endpoint.node as InputInterface).input1 = null;
                }
                else
                {
                    (startpoint.endpoint.node as InputInterface).input2 = null;
                }
            }
        }

        /*
        foreach (ValueEndpoint endpoint in gameObject.GetComponentsInChildren<ValueEndpoint>())
        {
            // Nothing to do here
        }
        */

        Destroy(gameObject);
    }

    private void OnMouseUp()
    {
        if (isOrigin)
        {
            clone.GetComponent<DragAndDropController>().isDragged = false;
            clone.GetComponent<DragAndDropController>().endNode = null;
            clone = null;
        }
        else
        {
            isDragged = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hi");
    }
}
