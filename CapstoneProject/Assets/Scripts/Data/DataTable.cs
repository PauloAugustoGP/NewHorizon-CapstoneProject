using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class DataTable : MonoBehaviour
{
    [SerializeField] string tablePath;
    [SerializeField] string tableRowType;

    Dictionary<string, float> table;
    List<string> header;

    void Awake()
    {
        table = new Dictionary<string, float>();
        header = new List<string>();
        
        SetupKeys();

        SetupDataTable(tableRowType);
    }

    void SetupKeys()
    {
        string line;

        StreamReader theReader = new StreamReader(Application.streamingAssetsPath + tablePath, Encoding.Default);
        
        line = theReader.ReadLine();

        if (line != null)
        {
            string[] entries = line.Split(',');
            if (entries.Length > 0)
                for (int i = 0; i < entries.Length; i++)
                {
                    table.Add(entries[i], 0.0f);
                    header.Add(entries[i]);
                }
        }

        theReader.Close();
    }

    ///<summary>
    ///Setup data for the current table, 
    ///parameter is the first string value 
    ///in the table row desired
    ///</summary>
    void SetupDataTable(string keyType)
    {
        string line;

        StreamReader theReader = new StreamReader(Application.streamingAssetsPath + tablePath, Encoding.Default);

        do
        {
            line = theReader.ReadLine();

            if (line != null)
            {
                string[] entries = line.Split(',');
                if (entries.Length > 0 && entries[0].Equals(keyType))
                {
                    for (int i = 1; i < entries.Length; i++)
                    {
                        table[header[i]] = float.Parse(entries[i]);
                    }
                        

                    line = null;
                }
            }
        }
        while (line != null);
        theReader.Close();
    }

    public void DisplayDataTable()
    {
        for (int i = 1; i < table.Count; i++)
            Debug.Log(header[i] + " = " + table[header[i]]);
    }

    public float GetTableValue(string key)
    {
        return table[key];
    }
}
