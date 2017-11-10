using System.Collections;
using System.Collections.Generic;
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
        root.Init(gameObject, 0, -1);

        CreateBehaviorTree(file);

        isInit = true;
    }

    void CreateBehaviorTree( DataFile file )
    {
        for (int i = 0; i < file.GetEntrySize(); i++)
        {
            DataFile.DataFileBT newClass = file.GetEntry(i);
            BTNode newNode = ScriptableObject.CreateInstance( newClass.className ) as BTNode;
            newNode.Init(gameObject, newClass.classID, newClass.parentClassID);
            root.AddChild(newNode);
        }
    }
}