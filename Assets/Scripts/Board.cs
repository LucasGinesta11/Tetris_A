using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class Board : MonoBehaviour
{
    public static int w = 10;
    public static int h = 20;
    public static GameObject[,] grid = new GameObject[w, h];
    private static Spawner spawner;

    void Start()
    {
        spawner = Object.FindFirstObjectByType<Spawner>();
    }

    public static Vector2 RoundVector2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    public static bool InsideBorder(Vector2 pos)
    {
        return pos.x >= 0 && pos.y >= 0 && pos.x < w && pos.y < h;
    }
    
    public static void DeleteRow(int y)
    {
        for (int x = 0; x < w; x++)
        {
            if (grid[x, y] != null)
            {
                grid[x, y].SetActive(false);
                grid[x, y] = null;
            }
        }
    }
    
    public static void DecreaseRow(int y)
    {
        for (int x = 0; x < w; x++)
        {
            if (grid[x, y] != null)
            {
                grid[x, y].transform.position += new Vector3(0, -1, 0);
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
            }
        }
    }
    
    public static void DecreaseRowsAbove(int y)
    {
        for (int i = y; i < h; i++)
        {
            DecreaseRow(i);
        }
    }
    
    public static bool IsRowFull(int y)
    {
        for (int x = 0; x < w; x++)
        {
            if (grid[x, y] == null) return false;
        }
        return true;
    }
    
    public static void DeleteFullRows()
    {
        for (int y = 0; y < h; ++y)
        {
            if (IsRowFull(y))
            {
                Debug.Log("Fila completa");
                DeleteRow(y);
                DecreaseRowsAbove(y + 1);
                --y; 
            }
        }
    }
    
    public static void InitializeGrid(GameObject blockPrefab)
    {
        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                GameObject block = Instantiate(blockPrefab, new Vector3(x, y, 0), Quaternion.identity);
                block.SetActive(false);
                grid[x, y] = block; 
            }
        }
    }
    
    public static void ActivateBlock(int x, int y)
    {
        if (grid[x, y] != null)
        {
            grid[x, y].SetActive(true);
        }
    }
}
