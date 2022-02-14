using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingControll : MonoBehaviour
{
    public int warehouseCapacity;
    public Dictionary<string, int> warehouseResources1 = new Dictionary<string, int>();
    public Dictionary<string, int> warehouseResources2 = new Dictionary<string, int>();
    public Dictionary<string, int> warehouseResources3 = new Dictionary<string, int>();
    public int productionAmount;
    public float productionTime;
    public GameObject looseScreen;
    public Text looseInfo;

    // Start is called before the first frame update
    void Start()
    {
        warehouseResources1.Add("produced", 0);
        warehouseResources2.Add("consumable1", 20);
        warehouseResources2.Add("produced", 0);
        warehouseResources3.Add("consumable1", 20);
        warehouseResources3.Add("consumable2", 20);
        warehouseResources3.Add("produced", 0);
        InvokeRepeating("Building1Production", productionTime, 0.3f);
        InvokeRepeating("Building2Production", productionTime, 0.3f);
        InvokeRepeating("Building3Production", productionTime, 0.3f);
    }

    public void Building1Production()
    {
        if (warehouseResources1["produced"] <= warehouseCapacity)
        {
            warehouseResources1["produced"] += productionAmount;

            Debug.Log("Building 1 has " + warehouseResources2["produced"] + " produced");
        }
        else
        {
            looseScreen.SetActive(true);
            looseInfo.text = "«акончилось место на складе 1го производства";
            Time.timeScale = 0;
        }
    }
    public void Building2Production()
    {
        if (warehouseResources2["produced"] <= warehouseCapacity && warehouseResources2["consumable1"] >= productionAmount)
        {
            warehouseResources2["produced"] += productionAmount;
            warehouseResources2["consumable1"] -= productionAmount;
            Debug.Log("Building 2 has " + warehouseResources2["produced"] + " produced");
            Debug.Log("Building 2 has " + warehouseResources2["consumable1"] + " left");

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
    public void Building3Production()
    {
        if (warehouseResources3["produced"] <= warehouseCapacity && warehouseResources3["consumable1"] >= productionAmount && warehouseResources3["consumable2"] >= productionAmount)
        {
            warehouseResources3["produced"] += productionAmount;
            warehouseResources3["consumable1"] -= productionAmount;
            warehouseResources3["consumable2"] -= productionAmount;
            Debug.Log("Building 3 has " + warehouseResources3["produced"] + " produced");
            Debug.Log("Building 3 has " + warehouseResources3["consumable1"] + " left");
            Debug.Log("Building 3 has " + warehouseResources3["consumable2"] + " left");
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
}
