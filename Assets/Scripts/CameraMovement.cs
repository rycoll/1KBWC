using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{

    /* This should be attached to the main camera */

    //for constraining how far the map can be panned horizontally/vertically
    public int horizBound, vertBound;
    //for constraining how far the map can be zoomed in/out
    public int zoomMinBound, zoomMaxBound;
    public int zoomSpeed;
    private Camera cam;

    private void Start()
    {
        cam = this.GetComponent<Camera>();
    }

    private void Update()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        if (Mathf.Abs(this.transform.position.x + horiz) > horizBound)
            horiz = 0f;
        if (Mathf.Abs(this.transform.position.y + vert) > vertBound)
            vert = 0f;
        Vector3 move = new Vector3(horiz, vert, 0f);
        this.transform.Translate(move);

        /* this was from Tomb Rater, could still be relevant here?

        //the problem we want to avoid is scrolling on UI menus and camera simultaneously, when you only want one
        //it is a bit silly maybe ; a similar thing is done for MapTiles
        GameObject canvas = GameObject.Find("Canvas");
        GraphicRaycaster raycaster = canvas.GetComponent<GraphicRaycaster>();
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;
        List<RaycastResult> resultsList = new List<RaycastResult>();
        raycaster.Raycast(pointer, resultsList);
        foreach (RaycastResult result in resultsList)
        {
            if (result.gameObject.tag.Equals("Block Camera Scroll"))
            {
                return;
            }
        }
        */
        float scroll = Input.GetAxis("Mouse ScrollWheel") * -1 * zoomSpeed;
        if (cam.orthographicSize + scroll > zoomMaxBound || cam.orthographicSize + scroll < zoomMinBound)
        {
            scroll = 0f;
        }
        cam.orthographicSize += scroll;
        
    }

}
