using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] GameObject start;
    [SerializeField] GameObject end;

    [SerializeField] GameObject link;

    [SerializeField] float linkWidth = 0.2f;
    [SerializeField] int numLinks = 5;

    private float iterX, iterY;
    GameObject[] links;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        links = new GameObject[numLinks];
        float linkAngle = Vector2.SignedAngle(start.transform.position, end.transform.position);
        float linkLength = Vector2.Distance(start.transform.position, end.transform.position) / numLinks;

        iterX = (end.transform.position.x - start.transform.position.x) / numLinks;
        iterY = (end.transform.position.y - start.transform.position.y) / numLinks;

        links[0] = GenerateLink(start, linkLength, linkWidth, linkAngle, 0, 0);
        links[0].GetComponent<HingeJoint2D>().autoConfigureConnectedAnchor = false;

        for(int i = 1; i < numLinks; i++)
        {
            GameObject linkInstance = GenerateLink(links[i - 1], linkLength, linkWidth, linkAngle, iterX, iterY);
            links[i] = linkInstance;
            
        }
        //start.GetComponent<HingeJoint2D>().connectedBody = links[0].GetComponent<Rigidbody2D>();
        //end.GetComponent<HingeJoint2D>().connectedBody = links[numLinks - 1].GetComponent<Rigidbody2D>();
    }

    GameObject GenerateLink(GameObject startConnect,  float length, float width, float angle, float xOffset, float yOffset)
    {
        GameObject linkInstance = Instantiate(link, transform);
        linkInstance.transform.localScale = new Vector3(length, width);
        linkInstance.transform.rotation = Quaternion.Euler(0, 0, angle);
        Vector3 pos = new Vector3(startConnect.transform.position.x + xOffset, startConnect.transform.position.y + yOffset);
        linkInstance.transform.position = pos;

        HingeJoint2D hj = linkInstance.GetComponent<HingeJoint2D>();
        hj.connectedBody = startConnect.GetComponent<Rigidbody2D>();
        hj.useConnectedAnchor = true;
        hj.anchor = new Vector2(-(length / 2),0);

        return linkInstance;
    }
 
    // Update is called once per frame
    void Update()
    {
        
    }
}
