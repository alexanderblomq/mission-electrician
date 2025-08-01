using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] GameObject squareObj;
    [SerializeField] int gridWidth = 10;
    [SerializeField] int gridLength = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float squareWidth = squareObj.GetComponent<Transform>().localScale.x;

        for(int y = 0; y < gridLength; y++)
        {
            for(int x = 0; x < gridWidth; x++)
            {
                float xPos = x * squareWidth;
                float yPos = y * squareWidth;

                Instantiate(squareObj, new Vector2(xPos, yPos), new Quaternion());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
