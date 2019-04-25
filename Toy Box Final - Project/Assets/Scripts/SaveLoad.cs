using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoad : MonoBehaviour
{
    public static SaveLoad instance;

    string path;

    public string fullText;
    public string[] lineText;
    public string[] splitLine;

    public List<Node> nodes;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
            return;
        }

        //Deffine the path
        path = Application.dataPath + "/StreamingAssets/Project.txt";

        //Create Node creator
        
    }

    public void LoadProject()
    {
        //Read the file in path
        StreamReader reader = new StreamReader(path);
        fullText = reader.ReadToEnd();
        lineText = fullText.Split('\n');

        foreach (string line in lineText)
        {
            splitLine = line.Split(' ');
            switch (splitLine[0])
            {
                case "Node":
                    AddNode();
                    break;
                default:
                    Debug.LogError("Invalid input from project folder at line " + line);
                    break;
            }
        }

        reader.Close();
    }

    public void SaveProject()
    {
        //Write to the file in path
        StreamWriter writer = new StreamWriter(path, false);

        foreach (var node in nodes)
        {
            writer.WriteLine(node.ToString());
            print(node.ToString());
        }

        writer.Close();

    }

    public void AddNode()
    {
        try
        {
            nodes.Add(new Node(float.Parse(splitLine[1]), float.Parse(splitLine[2]), splitLine[3], int.Parse(splitLine[4]), int.Parse(splitLine[5]), int.Parse(splitLine[6]), splitLine[7], float.Parse(splitLine[8]), float.Parse(splitLine[9])));
        }
        catch (System.Exception)
        {
            Debug.LogError("Invalid line syntax at line " + lineText.ToString());
            throw;
        }
    }
}
