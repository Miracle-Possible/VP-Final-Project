using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicEditor : MonoBehaviour
{
    public static int ids = 0;

    private GameObject[] nodesGO;
    public Dictionary<int, Node> nodes;
    public GameObject MoveNodeprefab;
    public GameObject EventNodeprefab;

    public SaveLoad saveLoad;
    public NodeParser parser;

    // Start is called before the first frame update
    void Start()
    {
        saveLoad = SaveLoad.instance;
        parser = NodeParser.instance;
        ids = 0;
        nodes = new Dictionary<int, Node>();
    }

    // Update is called once per frame
    void Update()
    {
        if(saveLoad == null)
        {
            saveLoad = SaveLoad.instance;
        }

        if (parser == null)
        {
            parser = NodeParser.instance;
        }
    }

    void checkAllAvaliableNodes()
    {
        nodesGO = GameObject.FindGameObjectsWithTag("UIDraggable");
    }

    public void simulate()
    {
        checkAllAvaliableNodes();

        saveLoad.nodes.Clear();

        foreach (GameObject node in nodesGO)
        {
            if(node.name.Contains( "MoveNodeComp"))
            {
                print("Xpos is " + node.transform.Find("XposInputField").gameObject.GetComponent<InputField>().text);
                print("Ypos is " + node.transform.Find("YposInputField").gameObject.GetComponent<InputField>().text);

                Node myNode = node.GetComponent<NodeContainerGO>().myNode;
                myNode.xPos = node.transform.position.x;
                myNode.yPos = node.transform.position.y;

                myNode.moveX = float.Parse(node.transform.Find("XposInputField").gameObject.GetComponent<InputField>().text);
                myNode.moveY = float.Parse(node.transform.Find("YposInputField").gameObject.GetComponent<InputField>().text);

                saveLoad.nodes.Add(myNode);

            }
            if (node.name.Contains("onClickNodeComp"))
            {
                print("Button is " + node.transform.Find("EventtInputField").gameObject.GetComponent<InputField>().text);

                Node myNode = node.GetComponent<NodeContainerGO>().myNode;
                myNode.xPos = node.transform.position.x;
                myNode.yPos = node.transform.position.y;

                myNode.keyCode = node.transform.Find("EventtInputField").gameObject.GetComponent<InputField>().text;

                saveLoad.nodes.Add(myNode);

            }
        }

        parser.Parse();
    }
    public void createMoveNode()
    {
        GameObject newMoveNode= Instantiate(MoveNodeprefab, new Vector3(100, 100, 0), Quaternion.identity) as GameObject;
        newMoveNode.transform.parent= GameObject.Find("EditorRegionPanel").transform;

        int myId = ids++;
        Node newNode = new Node(100, 100, "move", myId, -1, -1, "a", 0, 0);
        newMoveNode.AddComponent<NodeContainerGO>().myNode = newNode;

        nodes.Add(myId, newNode);
    }
    public void createEventNode()
    {
        GameObject newEventNode = Instantiate(EventNodeprefab, new Vector3(100, 100, 0), Quaternion.identity) as GameObject;
        newEventNode.transform.parent = GameObject.Find("EditorRegionPanel").transform;

        int myId = ids++;
        Node newNode = new Node(100, 100, "onClick", myId, -1, -1, " ", 0, 0);
        newEventNode.AddComponent<NodeContainerGO>().myNode = newNode;

        nodes.Add(myId, newNode);
    }

    public void loadNodes()
    {
        ids = 0;
        nodesGO = GameObject.FindGameObjectsWithTag("UIDraggable");

        foreach (var item in nodesGO)
        {
            Destroy(item);
        }

        foreach (var item in saveLoad.nodes)
        {
            if(item.type == "move")
            {
                GameObject newMoveNode = Instantiate(MoveNodeprefab, new Vector3(item.xPos, item.yPos, 0), Quaternion.identity) as GameObject;
                newMoveNode.transform.parent = GameObject.Find("EditorRegionPanel").transform;

                newMoveNode.transform.Find("XposInputField").gameObject.GetComponent<InputField>().text = item.moveX + "";
                newMoveNode.transform.Find("YposInputField").gameObject.GetComponent<InputField>().text = item.moveY + "";

                newMoveNode.AddComponent<NodeContainerGO>().myNode = item;
            }

            if(item.type == "onClick")
            {
                GameObject newEventNode = Instantiate(EventNodeprefab, new Vector3(item.xPos, item.yPos, 0), Quaternion.identity) as GameObject;
                newEventNode.transform.parent = GameObject.Find("EditorRegionPanel").transform;

                newEventNode.transform.Find("EventtInputField").gameObject.GetComponent<InputField>().text = item.keyCode;

                newEventNode.AddComponent<NodeContainerGO>().myNode = item;
            }
            
            if (item.id + 1 > ids)
            {
                ids = item.id + 1;
            }
        }
    }

    public void switchScene()
    {

        SceneManager.LoadScene("Game View");
    }

   }
