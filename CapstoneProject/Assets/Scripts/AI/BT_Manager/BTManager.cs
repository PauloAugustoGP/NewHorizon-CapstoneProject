using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class BTManager : MonoBehaviour
{
    BTNode root;

    GameObject target;

    bool isInit;
	
	void Update ()
    {
        if(isInit)
            root.Run(target);
	}

    public void Init( DataFile file )
    {
        target = GameObject.FindWithTag("Player");

        root = ScriptableObject.CreateInstance("BTRoot") as BTNode;

        CreateBehaviorTree(file);

        isInit = true;
    }

    void CreateBehaviorTree( DataFile file )
    {
        for (int i = 0; i < file.GetEntrySize(); i++)
        {
            BTNode newNode = ScriptableObject.CreateInstance( file.GetEntry(i) ) as BTNode;
            newNode.Init(gameObject);
            root.AddChild(newNode);
        }
    }
}