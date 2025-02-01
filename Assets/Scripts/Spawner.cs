using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] piecePrefabs;
    private List<GameObject> piecesPool = new List<GameObject>();
    public GameObject blockPrefab;

    void Start()
    {
        
        foreach (GameObject prefab in piecePrefabs)
        {
            GameObject piece = Instantiate(prefab, transform.position, Quaternion.identity);
            piece.SetActive(false);
            piecesPool.Add(piece);
        }
        
        Board.InitializeGrid(blockPrefab);
        ActivateNextPiece();
    }

    void Update()
    {
    }

    public void ActivateNextPiece()
    {
        //Seleccionar pieza aleatoria del Pool
        int randomIndex = Random.Range(0, piecesPool.Count);
        GameObject piece = piecesPool[randomIndex];

        //Asegurarse de que este inactiva
        while (piece.activeInHierarchy)
        {
            randomIndex = Random.Range(0, piecesPool.Count);
            piece = piecesPool[randomIndex];
        }
        
        //Activar la pieza
        piece.transform.position = new Vector3(5, 16, 0);
        piece.SetActive(true);
        piece.GetComponent<Piece>().enabled = true;
    }
}