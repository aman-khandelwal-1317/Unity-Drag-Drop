using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{

    public GameObject selectedObject;
    public GameObject selectUI;

    private BuildingManager buildingManager;
    [SerializeField] private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();

    }

    // Update is called once per frame
    void Update()
    {

        if (!player.activeSelf && !selectUI.activeSelf && !buildingManager.ObjectPanel.activeSelf && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.collider.gameObject.CompareTag("Object"))
                {
                    Select(hit.collider.gameObject);
                }


            }
        }

        if (Input.GetMouseButtonDown(1) && selectedObject != null)
        {
            Deselect();
        }
    }

    private void Select(GameObject obj)
    {
        if (obj == selectedObject) return;
        if (selectedObject != null) Deselect();
        selectUI.SetActive(true);
        buildingManager.ObjectPanel.SetActive(false);
        buildingManager.isMoving = true;
        selectUI.GetComponent<RectTransform>().position = Input.mousePosition;
        selectedObject = obj;

    }

    public void Deselect()
    {
        selectUI.SetActive(false);
        selectedObject = null;
    }

    public void Delete()
    {
        GameObject objectToDestroy = selectedObject;
        Deselect();
        Destroy(objectToDestroy);
    }

    public void Move()
    {
        buildingManager.pendingObject = selectedObject;
        selectUI.SetActive(false);
    }


}
