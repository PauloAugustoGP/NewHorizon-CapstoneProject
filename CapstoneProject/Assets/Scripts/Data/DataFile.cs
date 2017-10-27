using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class DataFile : MonoBehaviour
{
    [SerializeField] string filePath;

    List<string> entries;
    
	void Awake ()
    {
        entries = new List<string>();

        SetupEntries(); 
	}

    void SetupEntries()
    {
        string line;

        StreamReader theReader = new StreamReader(Application.streamingAssetsPath + filePath, Encoding.Default);

        do
        {
            line = theReader.ReadLine();

            if (line != null)
            {
                entries.Add( line );
            }
        }
        while (line != null);
        theReader.Close();
    }

    public string GetEntry( int index )
    {
        return entries[index];
    }

    public int GetEntrySize() { return entries.Count; }
}
