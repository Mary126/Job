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
    public BuildingControll buildingControll;
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
    void GetResources(string type, string resource)
    {
        float amnt = playerResources["resource1"] + playerResources["resource2"];
        if (amnt <= backpackSize && type == "OutputWarehouse1" && buildingControll.warehouseResources1["produced"] >= 1)
        {
            playerResources[resource] = Mathf.Lerp(playerResources[resource], playerResources[resource] + 1, timeToTakeResources);
            buildingControll.warehouseResources1["produced"] =
                        Mathf.Lerp(buildingControll.warehouseResources1["produced"], buildingControll.warehouseResources1["produced"] - 1, timeToTakeResources);
        }
    }
    void PutResources(string type)
    {
        if (type == "InputWarehouse2")
        {
            if (playerResources["resource1"] >= 0 && buildingControll.warehouseResources2["consumable1"] <= buildingControll.warehouseCapacity)
            {
                buildingControll.warehouseResources2["consumable1"] = 
                    Mathf.Lerp(buildingControll.warehouseResources2["consumable1"], buildingControll.warehouseResources2["consumable1"] + 1, timeToTakeResources);
                playerResources["resource1"] = Mathf.Lerp(playerResources["resource1"], playerResources["resource1"] - 1, timeToTakeResources);
            }
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
            buildingControll.warehouseResources1["produced"] += dif;
        }
        else if (collision.tag == "OutputWarehouse2")
        {
            float dif = playerResources["resource2"] - (float)Math.Floor(playerResources["resource2"]);
            playerResources["resource2"] = (float)Math.Floor(playerResources["resource2"]);
            buildingControll.warehouseResources2["produced"] += dif;
        }
        else if (collision.tag == "InputWarehouse2")
        {
            float dif = (float)Math.Ceiling(playerResources["resource1"]) - playerResources["resource1"];
            playerResources["resource1"] = (float)Math.Ceiling(playerResources["resource1"]);
            buildingControll.warehouseResources2["produced"] -= dif;

        }
        //playerResources["resource2"] = Mathf.Round(playerResources["resource2"]);
        isColliding = null;
    }
    private void Update()
    {
        if (isColliding != null)
        {
            switch (isColliding.tag)
            {
                case "OutputWarehouse1": GetResources(isColliding.tag, "resource1"); break;
                case "OutputWarehouse2": GetResources(isColliding.tag, "resource2"); break;
                case "InputWarehouse2": PutResources(isColliding.tag); break;
                case "InputWarehouse3": PutResources(isColliding.tag); break;
                default: break;
            }
        }
        backpack1.text = playerResources["resource1"].ToString();
        backpack2.text = playerResources["resource2"].ToString();
    }
}
