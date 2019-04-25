using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public static int ids;

    public float xPos;
    public float yPos;

    public string type;
    public int id;
    public int inputId;
    public int outputId;

    //Node type specific data
    public string keyCode;

    public float moveX;
    public float moveY;


    public Node(float xPos, float yPos, string type, int id, int inputId, int outputId, string keyCode, float moveX, float moveY)
    {
        this.xPos = xPos;
        this.yPos = yPos;

        this.type = type;
        this.id = id;
        this.inputId = inputId;
        this.outputId = outputId;

        this.keyCode = keyCode;

        this.moveX = moveX;
        this.moveY = moveY;
    }

    public override string ToString()
    {
        return "Node " + xPos + " " + yPos + " " + type + " " + id + " " + inputId + " " + outputId + " " + keyCode + " " + moveX + " " + moveY;
    }

}
