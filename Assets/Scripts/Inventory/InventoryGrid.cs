using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryGrid : MonoBehaviour
{
    public PlayerInput playerInput;
    public float tileWidth = 64;
    public float tileHeight = 64;
    public RectTransform rectTransform;

    private void Start()
    {
        if (!playerInput){playerInput = FindObjectOfType<PlayerInput>();}
        if (!rectTransform){rectTransform = GetComponent<RectTransform>();}
    }

    Vector2 positionOnTheGrid = new Vector2();
    Vector2Int tileGridPosition = new Vector2Int();

    public Vector2Int GetTilePosition (Vector2 mousePosition)
    {
        positionOnTheGrid.x = mousePosition.x - rectTransform.position.x;
        positionOnTheGrid.y = mousePosition.y - rectTransform.position.y;

        tileGridPosition.x = (int)(positionOnTheGrid.x / tileWidth);
        tileGridPosition.y = (int)(positionOnTheGrid.y / tileHeight);

        return tileGridPosition;
    }
}
