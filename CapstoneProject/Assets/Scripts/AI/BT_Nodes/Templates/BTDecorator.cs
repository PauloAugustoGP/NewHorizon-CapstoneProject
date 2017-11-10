using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTDecorator : BTNode
{
    float speedRotation;

    protected enum DecoratorType
    {
        Inverter = 0,
        Rotator
    }
    protected DecoratorType currentType;

    public BTDecorator() : base()
    {
        CreateChildsList();

        currentType = DecoratorType.Inverter;
    }

    public override void Init(GameObject character, int ID, int parentID)
    {
        SetAgentAndTransform(character.GetComponent<Enemy>(), character.transform);

        SetNodeID(ID, parentID);

        speedRotation = agent.GetDataFromEnemyTable("Turn Rate");
    }

    public override int Run(GameObject target)
    {
        switch(currentType)
        {
            case DecoratorType.Inverter:
                {
                    return childs[0].Run( target ) * (-1);
                }

            case DecoratorType.Rotator:
                {
                    Vector3 direction = (new Vector3(target.transform.position.x, 0f, target.transform.position.z) - new Vector3(agentTransform.position.x, 0f, agentTransform.position.z)).normalized;
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    agentTransform.rotation = Quaternion.Slerp(agentTransform.transform.rotation, lookRotation, Time.deltaTime * speedRotation);

                    return childs[0].Run( target );
                }

            default:
                Debug.Log("ERROR");
                return -1;
        }
    }
}
