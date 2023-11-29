using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GardenManager : MonoBehaviour
{
    public GameObject flowerImagePrefab; // UI Imageプレハブをアサインする
    public Sprite[] flowerSprites; // 花のスプライトの配列
    public Vector2 gridSize = new Vector2(3, 3); // グリッドのサイズ
    public float spacing = 300f; // グリッド内のスペース
    public float flowerScale = 4f; // Inspectorから調整可能なスケール
    public float yOffset = 200f; // Inspectorから調整可能なYオフセット

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
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
                                                      -y * spacing + (gridSize.y - 1) * spacing / 2 - yOffset); // Apply the Y-offset here
                    rt.localScale = new Vector3(flowerScale, flowerScale, flowerScale); // Apply the scale set in Inspector

                    // Change the sprite of the flower
                    Image flowerImage = flower.GetComponent<Image>();
                    if (flowerImage != null && flowerSprites.Length > 0)
                    {
                        // Select a sprite randomly or by some other logic
                        Sprite newSprite = flowerSprites[Random.Range(0, flowerSprites.Length)];
                        flowerImage.sprite = newSprite;
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
