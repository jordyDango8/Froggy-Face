using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesController : MonoBehaviour
{
    [SerializeField]
    GameObject[] obstacles;
    [SerializeField]
    Sprite[] rocksSprites;
    [SerializeField]
    float sizeMin = 0.2f;
    [SerializeField]
    float sizeMax = 0.5f;
    
    void Start()
    {
        foreach (GameObject obstacle in obstacles)
        {
            //obstacle.SetActive(false); // for fast test
            RockBehaviour rock = obstacle.GetComponent<RockBehaviour>();
            float size = Random.Range(sizeMin, sizeMax);
            rock.ChangeSize(new Vector3(size, size, size));
            rock.ChangeRotation(new Vector3(0, 0, Random.Range(0f, 360f)));
            rock.ChangeSprite(rocksSprites[Random.Range(0, rocksSprites.Length)]);
            rock.AddCollider();                            
        }
    }

}
