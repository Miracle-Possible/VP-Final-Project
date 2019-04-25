using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NodesUio : MonoBehaviour
{
    public const string DRAGGABLE_NODE = "UIDraggable";
    private bool dragging = false;

    private Vector2 orginalPos;
    private Transform object2drag;
    private Image objectToDragImage;
    public GameObject EditorRegionGO;

    List<RaycastResult> hitObjects = new List<RaycastResult>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GetDragTransformFromUnderMouse()!=null)
        {
            object2drag = GetDragTransformFromUnderMouse();
            if (object2drag != null)
            {
                dragging = true;

                object2drag.SetAsLastSibling();

                orginalPos = object2drag.position;
                objectToDragImage = object2drag.GetComponent<Image>();
                objectToDragImage.raycastTarget = false;
            }
        }

        if (dragging)
        {
            object2drag.position = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Transform object2Replace = GetDragTransformFromUnderMouse();

            if(object2Replace!=null)
            {
                //object2drag.position = object2Replace.position;
                //object2Replace.position = orginalPos;
            }
            else
            {
                if (GetDragTransformFromUnderMouse() != null)
                {
                    object2drag.position = Input.mousePosition;
                }

                if (object2drag != null && object2drag.transform.parent.gameObject != EditorRegionGO.gameObject)
                {
                    object2drag.transform.SetParent(EditorRegionGO.transform);
                }

            }
            if (objectToDragImage != null)
            {
                objectToDragImage.raycastTarget = true;
            }
                object2drag = null;
                dragging = false;
        }
    }

    private GameObject GetObjectunderMouse()
    {
        var pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;
        EventSystem.current.RaycastAll(pointer, hitObjects);

        if (hitObjects.Count <= 0) return null;

        return hitObjects[0].gameObject;
    }

    private Transform GetDragTransformFromUnderMouse()
    {
        GameObject clickedObj = GetObjectunderMouse();
        //print(clickedObj.tag);

        if (clickedObj != null && clickedObj.tag == DRAGGABLE_NODE)
        {
            //print("found");
            return clickedObj.transform;
        }
        return null;
    }

}
