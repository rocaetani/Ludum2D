using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BubbleSpawn : MonoBehaviour
{

    public List<int> HeightsToSpawn;

    public GameObject BubblePrefab;

    public float DistanceToSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        if (HeightsToSpawn == null)
        {
            HeightsToSpawn = new List<int>();
        }

        //CreateBubbles();
    }

    public void CreateBubbles()
    {
        foreach (int height in HeightsToSpawn)
        {
            int side = Random.Range(0, 1);
                Vector3 position;
                if (side == 0)
                {
                    position = new Vector3(0, height, 0);
                }
                else
                {
                    position = new Vector3(0, height, 0);
                }

                Instantiate(BubblePrefab, position, quaternion.identity);
        }
    }

    // Update is called once per frame
    
    void Update()
    {

        int createHeight = 0;
        foreach (int height in HeightsToSpawn)
        {
            if (GameObjectAccess.Player.transform.position.y < height + 10)
            {
                int side = Random.Range(0, 2);
                Vector3 position;
                if (side == 0)
                {
                    position = new Vector3(-DistanceToSpawn, height, 0);
                }
                else
                {
                    position = new Vector3(DistanceToSpawn, height, 0);
                }

                Instantiate(BubblePrefab, position, quaternion.identity);
                createHeight = height;
            }
        }

        if (createHeight != 0)
        {
            HeightsToSpawn.Remove(createHeight);
        }
    }
    
}
