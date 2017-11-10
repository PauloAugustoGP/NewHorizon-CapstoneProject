using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class DataFile : MonoBehaviour
{
    [SerializeField] string folderName;
    [SerializeField] string fileName;

    public struct DataFileBT
    {
        public string className;
        public int classID;
        public int parentClassID;
    }

    List<DataFileBT> entries;
    
	void Awake ()
    {
        entries = new List<DataFileBT>();

        SetupEntries(); 
	}

    void SetupEntries()
    {
        string line;

        StreamReader theReader = new StreamReader(Application.streamingAssetsPath + "/" + folderName + "/" + fileName, Encoding.Default);

        do
        {
            line = theReader.ReadLine();

            if (line != null)
            {
                string[] data = line.Split(',');
                DataFileBT newData;

                newData.className = data[0];
                newData.classID = int.Parse(data[1]);
                newData.parentClassID = int.Parse(data[2]);

                entries.Add( newData );
            }
        }
        while (line != null);
        theReader.Close();
    }

    public DataFileBT GetEntry( int index )
    {
        return entries[index];
    }

    public int GetEntrySize() { return entries.Count; }
}
