using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GardenManager : MonoBehaviour
{
    [System.Serializable]
    public class FlowerData
    {
        public int spriteIndex; // nameフィールドを削除
    }

    [System.Serializable]
    public class FlowerDatabase
    {
        public FlowerData[] flowers; // JSONファイルの構造に合わせる
    }

    public GameObject flowerImagePrefab;
    public Sprite[] flowerSprites;
    public Vector2 gridSize = new Vector2(3, 3);
    public float spacing = 300f;
    public float flowerScale = 4f;
    public float yOffset = 200f;

    private FlowerDatabase flowerDatabase;

    void Start()
    {
        LoadFlowerData();
        GenerateGrid();
    }

    void LoadFlowerData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "garden.json");
        if(File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            flowerDatabase = JsonUtility.FromJson<FlowerDatabase>(dataAsJson);
        }
        else
        {
            Debug.LogError("Cannot find file!");
        }
    }

    void GenerateGrid()
    {
        RectTransform canvasRectTransform = GameObject.Find("Canvas").GetComponent<RectTransform>();

        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                GameObject flower = Instantiate(flowerImagePrefab, canvasRectTransform);
                RectTransform rt = flower.GetComponent<RectTransform>();
                if (rt != null)
                {
                    rt.anchoredPosition = new Vector2(x * spacing - (gridSize.x - 1) * spacing / 2, 
                                                      -y * spacing + (gridSize.y - 1) * spacing / 2 - yOffset);
                    rt.localScale = new Vector3(flowerScale, flowerScale, flowerScale);

                    Image flowerImage = flower.GetComponent<Image>();
                    int index = y * (int)gridSize.x + x;
                    if (flowerImage != null && index < flowerDatabase.flowers.Length)
                    {
                        int spriteIndex = flowerDatabase.flowers[index].spriteIndex;
                        if(spriteIndex < flowerSprites.Length)
                        {
                            flowerImage.sprite = flowerSprites[spriteIndex];
                        }
                    }
                }
                else
                {
                    Debug.LogError("Instantiated GameObject does not have a RectTransform component.");
                }
            }
        }
    }
}
