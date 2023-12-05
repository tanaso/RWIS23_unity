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
        public int spriteIndex; 
    }

    [System.Serializable]
    public class FlowerDatabase
    {
        public FlowerData[] flowers;
    }

    private GameObject flowerImagePrefab;
    private Sprite[] phaseSprites; // Array for phase sprites
    private Sprite[] flowerSprites; // Array for flower sprites
    private Vector2 gridSize = new Vector2(3, 3);
    private float spacing = 300f;
    private float flowerScale = 4f;
    private float yOffset = 200f;

    private FlowerDatabase flowerDatabase;

    void Start()
    {
        InitializeFields();
        LoadFlowerData();
        GenerateGrid();
    }

    void InitializeFields()
    {
        flowerImagePrefab = Resources.Load<GameObject>("Flower");

        // Initialize the phaseSprites array
        int numberOfPhaseSprites = 3; // Replace with actual number of phase sprites
        phaseSprites = new Sprite[numberOfPhaseSprites];
        for (int i = 0; i < numberOfPhaseSprites; i++)
        {
            phaseSprites[i] = Resources.Load<Sprite>($"Images/Garden/phase_{i}"); // Replace with actual path and naming pattern
        }

        // Initialize the flowerSprites array
        int numberOfFlowerSprites = 8; // Replace with actual number of flower sprites
        flowerSprites = new Sprite[numberOfFlowerSprites];
        flowerSprites[0] = Resources.Load<Sprite>($"Images/Garden/Black");
        flowerSprites[1] = Resources.Load<Sprite>($"Images/Garden/Blue");
        flowerSprites[2] = Resources.Load<Sprite>($"Images/Garden/Orange");
        flowerSprites[3] = Resources.Load<Sprite>($"Images/Garden/Pink");
        flowerSprites[4] = Resources.Load<Sprite>($"Images/Garden/Purple");
        flowerSprites[5] = Resources.Load<Sprite>($"Images/Garden/Red");
        flowerSprites[6] = Resources.Load<Sprite>($"Images/Garden/White");
        flowerSprites[7] = Resources.Load<Sprite>($"Images/Garden/Yellow");

        // for (int i = 0; i < numberOfFlowerSprites; i++)
        // {
        //     flowerSprites[i] = Resources.Load<Sprite>($"Images/Garden/flower{i}"); // Replace with actual path and naming pattern
        // }
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
