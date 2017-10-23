using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* NODE TEMPLATE */

public class BTNode : ScriptableObject
{
    int parentid;
    int id;
    
    protected List<BTNode> childs;

    protected Enemy agent;

    protected Transform agentTransform;

	public BTNode()
    {
        id = -1;
        parentid = -1;

        childs = null;

        agent = null;

        agentTransform = null;
    }

    public virtual void Init(GameObject character) { }
    public virtual int Run( GameObject target ) { return 0; }

    public void SetNodeID(int newID, int newParentID) { id = newID; parentid = newParentID; }

    public int GetNodeID() { return id; }
    public int GetNodeParentID() { return parentid; }

    /*************************************************************************/
    /*                          CHILD MANAGEMENT                             */
    /*************************************************************************/
    public void CreateChildsList() { childs = new List<BTNode>(); }

    /** ADD CHILD TO ARRAY OF NODES**/
    public virtual bool AddChild( BTNode newChild )
    {
        if (newChild.GetNodeParentID() == id)
        {
            childs.Add(newChild);
            return true;
        }
        else
        {
            for (int i = 0; i < childs.Count; i++ )
            {
                if (childs[i].AddChild(newChild))
                    return true;
            }

            return false;
        }
    }

    public bool AddChild( BTNode newChild, int index )
    {
        if (newChild.GetNodeParentID() == id)
        {
            childs.Insert(index, newChild);
            return true;
        }
        else
        {
            for (int i = 0; i < childs.Count; i++)
            {
                if (childs[i].AddChild(newChild, index))
                    return true;
            }

            return false;
        }
    }

    /** REMOVE CHILD FROM ARRAY OF NODES**/
    public void RemoveChild( BTNode child )
    {
        childs.Remove( child );
    }

    public void RemoveChild( int index )
    {
        childs.RemoveAt( index );
    }
    /*************************************************************************/


    /*************************************************************************/
    /*                          AGENT MANAGEMENT                             */
    /*************************************************************************/
    public void SetAgent(Enemy newAgent) { agent = newAgent; }
    public void SetAgentTransform(Transform newTransform) { agentTransform = newTransform; }
    public void SetAgentAndTransform(Enemy newAgent, Transform newTransform)
    {
        agent = newAgent;
        agentTransform = newTransform;
    }
    /*************************************************************************/
}