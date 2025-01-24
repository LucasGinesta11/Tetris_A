using UnityEngine;

public class Piece : MonoBehaviour
{
    
    private float fallTime = 1f;
    private float lastFall = 0f;
    
    // Start se llama antes de la primera actualización del frame.
    void Start()
    {
        // ¿Posición predeterminada no válida? Entonces es fin del juego.
        if (!IsValidBoard())
        {
            Debug.Log("FIN DEL JUEGO");
            Destroy(gameObject);
        }
    }

    // Update se llama una vez por cada frame.
    // Implementa todos los movimientos de la pieza: derecha, izquierda, rotar y bajar.
    void Update()
    {
        // Movimiento automático hacia abajo
        if (Time.time - lastFall >= fallTime)
        {
            // Mover la pieza hacia abajo
            transform.position += new Vector3(0, -1, 0);

            // Verificar si la nueva posición es válida
            if (IsValidBoard())
            {
                UpdateBoard(); // Actualizar la cuadrícula con la nueva posición
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
                Board.DeleteFullRows();
                Object.FindFirstObjectByType<Spawner>().SpawnNext();
                
                enabled = false;
            }

            lastFall = Time.time;
        }
        // Mover a la izquierda
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Modificar posición
            transform.position += new Vector3(-1, 0, 0);

            // Verificar si es válido
            if (IsValidBoard())
                // Es válido. Actualizar la cuadrícula.
                UpdateBoard();
            else
                // No es válido. Revertir.
                transform.position += new Vector3(1, 0, 0);
            // Implementar mover a la derecha (tecla RightArrow)
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);

            if (IsValidBoard())
            {
                UpdateBoard();
            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, 90); // Rota la pieza 90 grados

            if (IsValidBoard())
            {
                UpdateBoard();
            }
            else
            {
                transform.Rotate(0, 0, -90); // Deshace la rotación si no es válida
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0, -1, 0);

            if (IsValidBoard())
            {
                UpdateBoard();
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
            }
        }

        
    }

    // TODO: Actualiza la cuadrícula con la posición actual de la pieza. 
    void UpdateBoard()
    {
        for (int y = 0; y < Board.h; ++y)
        {
            for (int x = 0; x < Board.w; ++x)
            {
                if (Board.grid[x, y] != null && Board.grid[x, y].transform.parent == transform)
                {
                    Board.grid[x, y] = null;
                }
            }
        }
        
        foreach (Transform child in transform)

        {
            Vector2 v = Board.RoundVector2(child.position);
// Se redondea a posiciones enteras para cada cubo(child)
            Board.grid[(int)v.x, (int)v.y] = child.gameObject;
// Se actualiza la matriz grid con la REFERENCIA al bloque en su nueva posición
        }
    }

    // Devuelve si la posición actual de la pieza hace que la cuadrícula sea válida o no.
    bool IsValidBoard()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Board.RoundVector2(child.position);

            // ¿No está dentro del límite?
            if (!Board.InsideBorder(v))
                return false;

            // ¿Bloque en celda de la cuadrícula (y no es parte del mismo grupo)?
            if (Board.grid[(int)v.x, (int)v.y] != null &&
                Board.grid[(int)v.x, (int)v.y].transform.parent != transform)
                return false;
        }

        return true;
    }
}