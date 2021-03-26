using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MaterialCatalog : MonoBehaviour
{
    public List<Material> materials = new List<Material>();

    #region Singleton
    public static MaterialCatalog instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("[MaterialCatalog.cs] - Multiple MaterialCatalog(s) found!");
            Destroy(gameObject);
        }

        materials = Resources.LoadAll<Material>("Materials").OfType<Material>().ToList();
        // Debug.Log("[MaterialCatalog.cs] - " + materials);
    }
    #endregion 

    public List<Material> GetAllColors()
    {
        return materials;
    }

    public Material GetColor(string color)
    {
        return materials.Find(x => x.name.Equals(color));
    }
}
