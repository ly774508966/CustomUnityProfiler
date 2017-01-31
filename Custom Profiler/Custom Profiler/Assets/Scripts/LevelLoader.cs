using UnityEngine;
using System.Collections;

[System.Serializable]
public class ColorToPrefab
{
    public Color32 color;
    public GameObject prefab;
}

public class LevelLoader : MonoBehaviour {

    public Texture2D levelMap;
    public ColorToPrefab[] colorToPrefab;

    // Use this for initialization
    void Start()
    {
        LoadMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EmptyMap()
    {
        //find any children objects and destroy them

        while (transform.childCount > 0)
        {
            Transform c = transform.GetChild(0);
            c.SetParent(null); //make child become batman
            Destroy(c.gameObject);
        }
    }

    public void LoadMap()
    {
        EmptyMap();

        Color32[] allPixels = levelMap.GetPixels32();

        int width = levelMap.width;
        int height = levelMap.height;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                SpawnTileAt(allPixels[(y * width) + x], x, y);
            }
        }
    }

    void SpawnTileAt(Color32 c, int x, int y)
    {
        //find the right color in our map

        //if pixel transparent then ignore
        if (c.a <= 0)
        {
            return;
        }

        //if want to speed up use dictionary look up
        foreach (ColorToPrefab ctp in colorToPrefab)
        {
            if (c.Equals(ctp.color))
            {
                //spawn prefab at the right location
                GameObject go = (GameObject)Instantiate(ctp.prefab, new Vector3(x, 0, y), Quaternion.identity);
                go.transform.parent = gameObject.transform;
                return;
            }
        }
        //if we got here then there was no colour matched

        Debug.LogError("colour: " + c.ToString() + " was not matched");
    }
}
