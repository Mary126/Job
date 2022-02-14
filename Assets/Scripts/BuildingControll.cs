using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingControll : MonoBehaviour
{
    public float warehouseCapacity;
    public Dictionary<string, float> warehouseResources1 = new Dictionary<string, float>();
    public Dictionary<string, float> warehouseResources2 = new Dictionary<string, float>();
    public Dictionary<string, float> warehouseResources3 = new Dictionary<string, float>();
    public int productionAmount;
    public float productionTime;
    public GameObject looseScreen;
    public Text looseInfo;
    public Text resource1;
    public Text resource2;
    public Text resource3;
    public Text inputWarehouse2Resource1;
    public Text inputWarehouse3Resource1;
    public Text inputWarehouse3Resource2;

    // Start is called before the first frame update
    void Start()
    {
        warehouseResources1.Add("produced", 0);
        warehouseResources2.Add("consumable1", 10);
        warehouseResources2.Add("produced", 0);
        warehouseResources3.Add("consumable1", 10);
        warehouseResources3.Add("consumable2", 10);
        warehouseResources3.Add("produced", 0);
    }
    void Building1Production()
    {
        if (warehouseResources1["produced"] < warehouseCapacity)
        {
            warehouseResources1["produced"] = Mathf.Lerp(warehouseResources1["produced"], warehouseResources1["produced"] + productionAmount, productionTime);
            resource1.text = warehouseResources1["produced"].ToString();
        }
        else
        {
            looseScreen.SetActive(true);
            looseInfo.text = "«акончилось место на складе 1го производства";
            Time.timeScale = 0;
        }
    }
    void Building2Production()
    {
        if (warehouseResources2["produced"] <= warehouseCapacity && warehouseResources2["consumable1"] >= productionAmount)
        {
            warehouseResources2["produced"] = Mathf.Lerp(warehouseResources2["produced"], warehouseResources2["produced"] + productionAmount, productionTime);
            warehouseResources2["consumable1"] = Mathf.Lerp(warehouseResources2["consumable1"], warehouseResources2["consumable1"] - productionAmount, productionTime);
            resource2.text = warehouseResources1["produced"].ToString();
            inputWarehouse2Resource1.text = warehouseResources2["consumable1"].ToString();

        }
        else if (warehouseResources2["consumable1"] < productionAmount)
        {
            looseScreen.SetActive(true);
            looseInfo.text = "Ќет ресурсов дл€ 2го производства";
            Time.timeScale = 0;
        }
        else if (warehouseResources2["produced"] > warehouseCapacity)
        {
            looseScreen.SetActive(true);
            looseInfo.text = "«акончилось место на складе 2го производства";
            Time.timeScale = 0;
        }
    }
    void Building3Production()
    {
        if (warehouseResources3["produced"] <= warehouseCapacity && warehouseResources3["consumable1"] >= productionAmount && warehouseResources3["consumable2"] >= productionAmount)
        {
            warehouseResources3["produced"] = Mathf.Lerp(warehouseResources3["produced"], warehouseResources3["produced"] + productionAmount, productionTime);
            warehouseResources3["consumable1"] = Mathf.Lerp(warehouseResources3["consumable1"], warehouseResources3["consumable1"] - productionAmount, productionTime);
            warehouseResources3["consumable2"] = Mathf.Lerp(warehouseResources3["consumable2"], warehouseResources3["consumable2"] - productionAmount, productionTime); ;
            resource3.text = warehouseResources3["produced"].ToString();
            inputWarehouse3Resource1.text = warehouseResources3["consumable1"].ToString();
            inputWarehouse3Resource2.text = warehouseResources3["consumable2"].ToString();

        }
        else if (warehouseResources3["consumable1"] < productionAmount || warehouseResources3["consumable2"] < productionAmount)
        {
            looseScreen.SetActive(true);
            looseInfo.text = "Ќет ресурсов дл€ 3го производства";
            Time.timeScale = 0;
        }
        else if (warehouseResources3["produced"] > warehouseCapacity)
        {
            looseScreen.SetActive(true);
            looseInfo.text = "«акончилось место на складе 3го производства";
            Time.timeScale = 0;
        }
    }
    private void Update()
    {
        Building1Production();
        Building2Production();
        Building3Production();
    }
}
