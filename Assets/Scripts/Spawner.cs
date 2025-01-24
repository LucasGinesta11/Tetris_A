using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Pieces
    public GameObject[] pieces;
    void Start()
    {
        SpawnNext();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnNext()
    {
        //Random index
        int i = Random.Range(0, pieces.Length);
        
        Vector3 v3 = new Vector3(4, 15, 0);
        
        //Spawn group at current position
        Instantiate(pieces[i], transform.position, Quaternion.identity);
        
    }
}
