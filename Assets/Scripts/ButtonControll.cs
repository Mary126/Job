using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControll : MonoBehaviour
{
    public PlayerControll playerControl;

    private void OnMouseDown()
    {
        switch (tag)
        {
            case "Up": playerControl.MoveUp(); break;
            case "Down": playerControl.MoveDown(); break;
            case "Left": playerControl.MoveLeft(); break;
            case "Right": playerControl.MoveRight(); break;
            default: Debug.Log("No button tag"); break;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
