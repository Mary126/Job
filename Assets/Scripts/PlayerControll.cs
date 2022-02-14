using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControll : MonoBehaviour
{
    public float speed;
    public float timeToTakeResources;
    public int backpackSize;
    public Dictionary<string, float> playerResources = new Dictionary<string, float>();
    Collider2D isColliding;
    public Text backpack1;
    public Text backpack2;

    private void Start()
    {
        playerResources.Add("resource1", 0);
        playerResources.Add("resource2", 0);
    }
    public void MoveUp()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + speed);
    }
    public void MoveDown()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - speed);
    }
    public void MoveLeft()
    {
        transform.position = new Vector3(transform.position.x - speed, transform.position.y);
    }
    public void MoveRight()
    {
        transform.position = new Vector3(transform.position.x + speed, transform.position.y);
    }
    void GetResources(string resource, BuildingController buildingController)
    {
        float amnt = playerResources["resource1"] + playerResources["resource2"];
        if (amnt <= backpackSize && buildingController.warehouseResources["produced"] >= 1)
        {
            playerResources[resource] = Mathf.Lerp(playerResources[resource], playerResources[resource] + 1, timeToTakeResources);
            if (resource == "resource1") backpack1.text = Math.Abs(playerResources["resource1"]).ToString();
            else if (resource == "resource2") backpack2.text = Math.Abs(playerResources["resource2"]).ToString();
            buildingController.warehouseResources["produced"] =
                        Mathf.Lerp(buildingController.warehouseResources["produced"], buildingController.warehouseResources["produced"] - 1, timeToTakeResources);
            buildingController.resourceDisplay3.text = Math.Abs(buildingController.warehouseResources["produced"]).ToString();
        }
        
    }
    void PutResources(string type, BuildingController buildingController)
    {
        if (playerResources["resource1"] > 0 && buildingController.warehouseResources["consumable1"] < buildingController.warehouseCapacity)
        {
            buildingController.warehouseResources["consumable1"] =
                Mathf.Lerp(buildingController.warehouseResources["consumable1"], buildingController.warehouseResources["consumable1"] + 1, timeToTakeResources);
            playerResources["resource1"] = Mathf.Lerp(playerResources["resource1"], playerResources["resource1"] - 1, timeToTakeResources);
            buildingController.resourceDisplay1.text = Math.Abs(buildingController.warehouseResources["consumable1"]).ToString();
        }
        if (type == "InputWarehouse3" && playerResources["resource2"] > 0 && buildingController.warehouseResources["consumable2"] < buildingController.warehouseCapacity)
        {
            buildingController.warehouseResources["consumable2"] =
                Mathf.Lerp(buildingController.warehouseResources["consumable2"], buildingController.warehouseResources["consumable2"] + 1, timeToTakeResources);
            playerResources["resource2"] = Mathf.Lerp(playerResources["resource2"], playerResources["resource2"] - 1, timeToTakeResources);
            buildingController.resourceDisplay2.text = Math.Abs(buildingController.warehouseResources["consumable2"]).ToString();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isColliding = collision;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "OutputWarehouse1")
        {
            float dif = playerResources["resource1"] - (float)Math.Floor(playerResources["resource1"]);
            playerResources["resource1"] = (float)Math.Floor(playerResources["resource1"]);
            collision.GetComponent<WarehouseInstances>().buildingController.warehouseResources["produced"] += dif;
        }
        else if (collision.tag == "OutputWarehouse2")
        {
            float dif = playerResources["resource2"] - (float)Math.Floor(playerResources["resource2"]);
            playerResources["resource2"] = (float)Math.Floor(playerResources["resource2"]);
            collision.GetComponent<WarehouseInstances>().buildingController.warehouseResources["produced"] += dif;
        }
        else if (collision.tag == "InputWarehouse2")
        {
            float dif = (float)Math.Ceiling(playerResources["resource1"]) - playerResources["resource1"];
            playerResources["resource1"] = (float)Math.Ceiling(playerResources["resource1"]);
            collision.GetComponent<WarehouseInstances>().buildingController.warehouseResources["consumable1"] -= dif;

        }
        else if (collision.tag == "InputWarehouse3")
        {
            float dif1 = (float)Math.Ceiling(playerResources["resource1"]) - playerResources["resource1"];
            playerResources["resource1"] = (float)Math.Ceiling(playerResources["resource1"]);
            collision.GetComponent<WarehouseInstances>().buildingController.warehouseResources["consumable1"] -= dif1;
            float dif2 = (float)Math.Ceiling(playerResources["resource2"]) - playerResources["resource2"];
            playerResources["resource2"] = (float)Math.Ceiling(playerResources["resource2"]);
            collision.GetComponent<WarehouseInstances>().buildingController.warehouseResources["consumable2"] -= dif2;
        }
        isColliding = null;
    }
    private void Update()
    {
        if (isColliding != null)
        {
            switch (isColliding.tag)
            {
                case "OutputWarehouse1": GetResources("resource1", isColliding.GetComponent<WarehouseInstances>().buildingController); break;
                case "OutputWarehouse2": GetResources("resource2", isColliding.GetComponent<WarehouseInstances>().buildingController); break;
                case "InputWarehouse2": PutResources(isColliding.tag, isColliding.GetComponent<WarehouseInstances>().buildingController); break;
                case "InputWarehouse3": PutResources(isColliding.tag, isColliding.GetComponent<WarehouseInstances>().buildingController); break;
                default: break;
            }
        }
        backpack1.text = Math.Abs(playerResources["resource1"]).ToString();
        backpack2.text = Math.Abs(playerResources["resource2"]).ToString();
    }
}
