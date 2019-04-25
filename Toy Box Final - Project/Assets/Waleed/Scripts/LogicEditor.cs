using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicEditor : MonoBehaviour
{
    private GameObject[] nodes;
    public GameObject MoveNodeprefab;
    public GameObject EventNodeprefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void checkAllAvaliableNodes()
    {
        nodes = GameObject.FindGameObjectsWithTag("UIDraggable");
    }

    public void simulate()
    {
        checkAllAvaliableNodes();

        foreach(GameObject node in nodes)
        {
            if(node.name== "MoveNodeComp")
            {
                print("Xpos is " + node.transform.Find("XposInputField").gameObject.GetComponent<InputField>().text);
                print("Ypos is " + node.transform.Find("YposInputField").gameObject.GetComponent<InputField>().text);
            }
            if (node.name == "onClickNodeComp")
            {
                print("Button is " + node.transform.Find("EventtInputField").gameObject.GetComponent<InputField>().text);
            }
        }
    }
    public void createMoveNode()
    {
        GameObject newMoveNode= Instantiate(MoveNodeprefab, new Vector3(100, 100, 0), Quaternion.identity) as GameObject;
        newMoveNode.transform.parent= GameObject.Find("EditorRegionPanel").transform;
    }
    public void createEventNode()
    {
        GameObject newEventNode = Instantiate(EventNodeprefab, new Vector3(100, 100, 0), Quaternion.identity) as GameObject;
        newEventNode.transform.parent = GameObject.Find("EditorRegionPanel").transform;
    }
    public void switchScene()
    {
        SceneManager.LoadScene("SceneName");
    }

   }
