using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{
    public LayerMask clickableLayer;

    public Texture2D pointer;//normal cursor
    public Texture2D target;//for clickable world Objects
    public Texture2D doorway;//doorways
    public Texture2D combat;//for combat cursors

    public EventVector3 onClickEnvironment;

    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit,50f,clickableLayer.value))
        {
            bool door = false;
            bool item = false;
            if (hit.collider.tag == "Doorway")
            {
                Cursor.SetCursor(doorway, new Vector2(16, 16), CursorMode.Auto);
                door = true;
            }
            else if(hit.collider.tag =="Item")
            {
                Cursor.SetCursor(combat, new Vector2(16, 16), CursorMode.Auto);
                item = true;
            }
            else
            {
                Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
            }
            
            if(Input.GetMouseButtonDown(0))
            {
                if(door)
                {
                    Transform doorway = hit.collider.gameObject.transform;
                    onClickEnvironment.Invoke(doorway.position);
                    //Debug.Log("DOOR");
                }
                else if(item)
                {
                    Transform itemPos = hit.collider.gameObject.transform;
                    onClickEnvironment.Invoke(itemPos.position);
                    //Debug.Log("ITEM");
                }
                else
                {
                    onClickEnvironment.Invoke(hit.point);
                }
            }
        }
        else
            Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
    }
}

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }
