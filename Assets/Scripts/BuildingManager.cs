using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject Player;
    public GameObject ObjectPanel;
   
    public GameObject pendingObject;
    private Vector3 pos;
    private RaycastHit hit;
    private Material material;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Material[] materials;

    public float rotateAmount;

    public float gridSize;
    bool gridOn = true;
    public bool isMoving = false;

    public bool canPlace = true;
    [SerializeField] private Toggle gridToggle;


    // Update is called once per frame
    void Update()
    {
        
        if(pendingObject != null)
        {

            if (gridOn)
            {
                pendingObject.transform.position = new Vector3(
                    RoundToNearestGrid(pos.x), RoundToNearestGrid(pos.y),RoundToNearestGrid(pos.z));
            } else
            {
                pendingObject.transform.position = pos;
            }

            if(Input.GetMouseButtonDown(0)  && canPlace )
            {
                PlaceObject();
                if (!isMoving)
                {
                    isMoving = false;
                    ObjectPanel.SetActive(true);
                }
              
            }
            if(Input.GetKeyDown(KeyCode.R))
            {
                RotateObject();
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Player.active)
            {
                Player.SetActive(true);
            } else
            {
                Player.SetActive(false);
            }
           
        }

        if (pendingObject != null && pendingObject.GetComponent<MeshRenderer>() != null)
            UpdateMaterials();
    }

    public void PlaceObject()
    {
        if(pendingObject.GetComponent<MeshRenderer>() != null)
        {
            pendingObject.GetComponent<MeshRenderer>().material = material;
        }
    
        
        pendingObject = null;
    }

    public void RotateObject()
    {
        pendingObject.transform.Rotate(Vector3.up, rotateAmount);
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, 1000 , layerMask)) {

            pos = hit.point;

        }
    }

    float RoundToNearestGrid(float pos)
    {
        float xDiff = pos % gridSize;
        pos -= xDiff;
        if (xDiff > (gridSize / 2))
        {
            pos += gridSize;
        }
        return pos;
    }

    public void SelectObject(int index)
    {
       
        pendingObject = Instantiate(objects[index],pos,transform.rotation);
        if (pendingObject.GetComponent<MeshRenderer>() != null)
            material = pendingObject.GetComponent<MeshRenderer>().material;
        ObjectPanel.SetActive(false);
    }

    public void ToggleGrid()
    {
        if(gridToggle.isOn)
        {
            gridOn = true;
        } else
        {
            gridOn= false;
        }
    }

    public void UpdateMaterials()
    {
        if (canPlace)
        {
            pendingObject.GetComponent<MeshRenderer>().material = materials[0];
        }

        if (!canPlace)
        {
            pendingObject.GetComponent<MeshRenderer>().material = materials[1];
        }
    }


    public void ExitGame()
    {
        Application.Quit();
    }

   
}
