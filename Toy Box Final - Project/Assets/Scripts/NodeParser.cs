using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Refrences: 
//https://answers.unity.com/questions/531953/creating-a-multidimensional-array-list.html
//https://answers.unity.com/questions/713224/retrieve-the-information-from-dictionary-in-c.html
//https://answers.unity.com/questions/677070/sorting-a-list-linq.html

public class NodeParser : MonoBehaviour
{
    public static NodeParser instance;

    public SaveLoad projectSave;
    public List<Node> nodes;

    public Dictionary<int, Node> nodeHeads;
    public Dictionary<int, List<Node>> nodeBodies;

    public GameObject player;
    public List<Event> events;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        projectSave = SaveLoad.instance;
        PrepareDataForParsing();
    }

    private void Update()
    {
        if(projectSave == null)
        {
            projectSave = SaveLoad.instance;
        }

        try
        {
            player = GameObject.FindGameObjectWithTag("Player");
            ExecuteProject();

        }
        catch (System.Exception)
        {

            throw;
        }
    }

    public void PrepareDataForParsing()
    {
        nodeHeads = new Dictionary<int, Node>();
        nodeBodies = new Dictionary<int, List<Node>>();

        nodes.Clear();
        nodeHeads.Clear();
        nodeBodies.Clear();

        foreach (var item in projectSave.nodes)
        {
            nodes.Add(item);
        }
    }

    public void Parse()
    {
        PrepareDataForParsing();

        //Sort nodes by id
        nodes.Sort((p1, p2) => p1.id.CompareTo(p2.id));

        foreach (var node in nodes)
        {
            if (node.outputId != -1)
            {
                nodeHeads.Add(node.id, node);
                nodeBodies.Add(node.id, new List<Node>());

                if (node.inputId != -1)
                {
                    nodeBodies[node.inputId].Add(node);
                }
            }
            else
            {
                nodeBodies[node.inputId].Add(node);
            }
        }

        DebugParsedData();
        ExecuteProject();
    }

    public void DebugParsedData()
    {

        foreach (KeyValuePair<int, Node> item in nodeHeads)
        {
            Debug.Log("Head  key: " + item.Key + " value: " + item.Value.id);

            foreach (var subItem in nodeBodies[item.Key])
            {
                Debug.Log("Item: " + subItem);
            }

        }
    }

    public void ExecuteProject()
    {
        foreach (KeyValuePair<int, Node> item in nodeHeads)
        {
            if (item.Value.inputId == -1)
            {
                ExecuteNodeHead(item.Value);
            }
        }
    }

    public void ExecuteNodeHead(Node node)
    {
        switch (node.type)
        {
            case "onClick":

                if (Input.GetKey(node.keyCode))
                {
                    Debug.Log("clicked " + node.keyCode);

                    if (nodeHeads.ContainsKey(node.outputId))
                    {
                        Debug.Log("heads " + node.outputId);
                        ExecuteNodeHead(nodeHeads[node.outputId]);
                    }
                    else if (nodeBodies.ContainsKey(node.id))
                    {
                        Debug.Log("Bodies " + node.id);
                        foreach (var item in nodeBodies[node.id])
                        {
                            Debug.Log("Body " + item);
                            ExecuteNode(item);
                        }
                    }
                    else
                    {
                        Debug.Log("Head with no child " + node.id);
                    }
                }
                break;
            default:
                break;
        }
    }

    public void ExecuteNode(Node node)
    {
        Debug.Log("Body " + node.type);
        switch (node.type)
        {
            case "move":
                Debug.Log("move " + node.id);
                player.transform.position += new Vector3(node.moveX * Time.deltaTime, node.moveY * Time.deltaTime, 0);
                break;
            default:
                break;
        }

    }
}
