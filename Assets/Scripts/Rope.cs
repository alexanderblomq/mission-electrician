using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] GameObject start;
    [SerializeField] GameObject end;

    [SerializeField] GameObject link;

    [SerializeField] float linkWidth = 0.2f;
    [SerializeField] int numLinks = 5;
    GameObject[] links;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        links = new GameObject[numLinks];
        float linkAngle = Vector2.Angle(start.transform.position, end.transform.position);
        float linkLength = Vector2.Distance(start.transform.position, end.transform.position) / numLinks;

        links[0] = GenerateLink(start.GetComponent<Rigidbody2D>(), linkLength, linkWidth, linkAngle);

        for(int i = 1; i < numLinks; i++)
        {
            GameObject linkInstance = GenerateLink(links[i - 1].GetComponent<Rigidbody2D>(), linkLength, linkWidth, linkAngle);
            
            links[i] = linkInstance;
        }


    }

    GameObject GenerateLink(Rigidbody2D startConnect,  float length, float width, float angle)
    {
        GameObject linkInstance = Instantiate(link, transform);
        linkInstance.transform.localScale = new Vector3(length, width);
        linkInstance.transform.rotation = new Quaternion(0, 0, angle, 0);

        HingeJoint2D hj = linkInstance.GetComponent<HingeJoint2D>();
        hj.connectedBody = startConnect;

        hj.useConnectedAnchor = true;
        hj.anchor = new Vector2(0, -(length/2));           // bottom of this segment (local)
        hj.connectedAnchor = new Vector2(0, (length/2));

        return linkInstance;
    }
}
