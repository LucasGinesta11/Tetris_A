using UnityEngine;

public class Board : MonoBehaviour
{
    public static int w = 10;
    public static int h = 20;
    public static GameObject[,] grid = new GameObject[w, h];

    // Redondea Vector2 para que no tenga valores decimales.
    // Se utiliza para forzar coordenadas enteras (sin decimales) al mover las piezas.
    public static Vector2 RoundVector2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x),
            Mathf.Round(v.y));
    }

    // TODO: Devuelve true si pos (x, y) está dentro de la cuadrícula, false en caso contrario.
    public static bool InsideBorder(Vector2 pos)
    {
        if (pos.x < 0 || pos.y < 0 || pos.x >= w || pos.y >= h)
        {
            return false;
        }

        return true;
    }

    // TODO: Elimina todos los GameObjects en la fila Y y establece las celdas de la fila en null.
    // Puedes usar la función Destroy para eliminar los GameObjects.
    public static void DeleteRow(int y)
    {
        for (int x = 0; x < w; x++)
        {
            if (grid[x, y] != null)
            {
                Destroy(grid[x, y]);
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
                // Mover el bloque hacia abajo
                grid[x, y].transform.position += new Vector3(0, -1, 0);
                // Actualizar la cuadrícula
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
            if (grid[x, y] == null)
                return false;
        }

        return true;
    }

    // Elimina las filas completas.
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
}