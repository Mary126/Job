using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingController : MonoBehaviour 
{
    public float warehouseCapacity;
    public Dictionary<string, float> warehouseResources = new Dictionary<string, float>();
    public int productionAmount;
    public float productionTime;
    public int buildingNumber;
    public Text waningInfo1;
    public Text warningInfo2;
    public Text resourceDisplay1;
    public Text resourceDisplay2;
    public Text resourceDisplay3;
    public bool consumableResource1;
    public bool consumableResource2;
    // Start is called before the first frame update
    void Start()
    {
        warehouseResources.Add("produced", 0);
        warehouseResources.Add("consumable1", 10);
        warehouseResources.Add("consumable2", 10);
    }
    void BuildingProduction()
    {
        if (warehouseResources["produced"] < warehouseCapacity)
        {
            // builging 2
            if (consumableResource1 == true && warehouseResources["consumable1"] > 0 && consumableResource2 == false)
            {
                warehouseResources["consumable1"] = Mathf.Lerp(warehouseResources["consumable1"], warehouseResources["consumable1"] - productionAmount, productionTime);
                resourceDisplay1.text = warehouseResources["consumable1"].ToString();
            }
            //building 3
            else if (consumableResource2 == true && warehouseResources["consumable2"] > 0 && warehouseResources["consumable1"] > 0)
            {
                warehouseResources["consumable1"] = Mathf.Lerp(warehouseResources["consumable1"], warehouseResources["consumable1"] - productionAmount, productionTime);
                warehouseResources["consumable2"] = Mathf.Lerp(warehouseResources["consumable2"], warehouseResources["consumable2"] - productionAmount, productionTime);
                resourceDisplay1.text = warehouseResources["consumable1"].ToString();
                resourceDisplay2.text = warehouseResources["consumable2"].ToString();
            }
            else if (consumableResource1 == true || consumableResource2 == true)
            {
                waningInfo1.text = "Ќет ресурсов дл€ " + buildingNumber + "го производства";
                return;
            }
            warehouseResources["produced"] = Mathf.Lerp(warehouseResources["produced"], warehouseResources["produced"] + productionAmount, productionTime);
            resourceDisplay3.text = warehouseResources["produced"].ToString();
        }
        else
        {
            warningInfo2.text = "«акончилось место на складе " + buildingNumber + "го производства";
        }
    }
    // Update is called once per frame
    void Update()
    {
        BuildingProduction();
    }
}
