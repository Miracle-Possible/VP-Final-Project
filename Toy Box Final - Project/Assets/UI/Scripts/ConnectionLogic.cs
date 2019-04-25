using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEditor;

public class ConnectionLogic : MonoBehaviour
{
    private GameObject[] conns;
    private Button connectTo;
    private Button connectFrom;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void checkAllconnections()
    {
        conns = GameObject.FindGameObjectsWithTag("ConnectMe");
    }

    private GameObject FindGameObjectInChildWithTag(GameObject parent, string tag)
    {
        Transform t = parent.transform;

        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).gameObject.tag == tag)
            {
                return t.GetChild(i).gameObject;
            }

        }

        return null;
    }

    public void connectme()
    {
        checkAllconnections();
        bool isFrom = true;
        GameObject otherButton = null;
        foreach(GameObject btn in conns)
        {
            if (!btn.GetComponent<Button>().IsInteractable())
            {
                isFrom = false;
                otherButton = btn;
            }
        }
        GameObject thisbut = EventSystem.current.currentSelectedGameObject;

        if (isFrom)
        {
            thisbut.GetComponent<Button>().interactable = false;
        }
        else
        {
            //Gizmos.DrawLine(thisbut.transform.position, thisbut.transform.position);
            //GL.LINES(thisbut.transform.position, otherButton.transform.position);
           // print("From" + otherButton.transform.position);
            //print("To" + thisbut.transform.position);

            GameObject Istherechildnode = FindGameObjectInChildWithTag(otherButton.transform.parent.transform.parent.gameObject, "UIDraggable");
            if (Istherechildnode == null)
            {
            thisbut.transform.parent.transform.parent.transform.position = otherButton.transform.parent.transform.parent.transform.position + new Vector3(0, -otherButton.transform.parent.transform.parent.gameObject.GetComponent<RectTransform>().rect.height, 0);
            thisbut.transform.parent.transform.parent.SetParent(otherButton.transform.parent.transform.parent);
            gameObject.GetComponent<NodeContainerGO>().myNode.inputId = otherButton.transform.parent.transform.parent.GetComponent<NodeContainerGO>().myNode.id;
            otherButton.transform.parent.transform.parent.GetComponent<NodeContainerGO>().myNode.outputId = gameObject.GetComponent<NodeContainerGO>().myNode.id;




            }

            /*
            var line = gameObject.AddComponent<LineRenderer>();
            line.sortingLayerName = "OnTop";
            line.sortingOrder = 5;
            line.SetColors(Color.red, Color.red);
            line.SetVertexCount(2);
            line.SetPosition(0, thisbut.transform.position);
            line.SetPosition(1, otherButton.transform.position);
            line.SetWidth(2, 2);
            line.useWorldSpace = true;*/
            //Debug.DrawLine(otherButton.transform.position, thisbut.transform.position,Color.red,100f);
            otherButton.GetComponent<Button>().interactable = true;
            //Handles.DrawBezier(otherButton.transform.position, thisbut.transform.position, Vector3.zero, Vector3.zero, Color.red, null, 1f);
        }
    }   
}
