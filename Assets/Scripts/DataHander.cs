using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class DataHander : MonoBehaviour
{
    [SerializeField] private ButtonManager buttonPrefab;
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private List<Item> items;

    public string label;
    private GameObject furniture;
    private int cr_id = 0;
    private static DataHander instance;
    public static DataHander Instance
    { 
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DataHander>();
            }
            return instance;
        }

    }
    void CreateButton()
    {
        foreach(Item i in items)
        {
            ButtonManager b = Instantiate(buttonPrefab, buttonContainer.transform);
            b.ItemId = cr_id;
            b.ButtonTexture = i.itemImage;
            cr_id ++;

        }
      
    }

    public void SetFurniture(int id)
    {
        furniture = items[id].itemPrefab;
    }
    public GameObject GetFurnitre()
    {
        return furniture;
    }
  /*  void LoadItems()
    {
        var items_obj = Resources.LoadAll("Items", typeof(Item));
        foreach (var item in items_obj)
        {
            items.Add(item as Item);
        }
    }*/
    private async void Start()
    {
        items = new List<Item>();
        //LoadItems();
        await Get(label);
        //unity tarafýndan belirlediðimiz label adýnda olan itemlarý yükleyecek

        CreateButton();
    }

    public async Task Get(string label)
    {
        var locations = await Addressables.LoadResourceLocationsAsync(label).Task;
        foreach(var location in locations)
        {
            var obj = await Addressables.LoadAssetAsync<Item>(location).Task;
            items.Add(obj);
        }
    }//asekron çalýþmasý için async methodlarý kullandým
}
