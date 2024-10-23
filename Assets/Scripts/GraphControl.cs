using UnityEngine;
public class GraphControl : MonoBehaviour
{
    private DoublyLinkedCircularList<NodeControl> listAllNodes=new DoublyLinkedCircularList<NodeControl>();
    public TextAsset textNodesPositions;
    public string[] arrayNodeRowsPositions;
    public string[] arrayNodeColumnsPositions;
    public GameObject objectNodePrefab;

    public TextAsset textNodesConnections;
    public string[] arrayNodeRowsConnections;
    public string[] arrayNodeColumnsConnections;
    public EnemyControl currentEnemy;
    private void Start()
    {
       DrawNodes();
       ConnectNodes();
       SetInitialNode();
    }
    public void DrawNodes()
    {
        GameObject currentNode;
        arrayNodeRowsPositions = textNodesPositions.text.Split('\n');
        for (int i = 0; i < arrayNodeRowsPositions.Length; ++i)
        {
            arrayNodeColumnsPositions=arrayNodeRowsPositions[i].Split(';');
            Vector2 positionToCreate = new Vector2(float.Parse(arrayNodeColumnsPositions[0]), float.Parse(arrayNodeColumnsPositions[1]));
            currentNode = Instantiate(objectNodePrefab, positionToCreate, transform.rotation);
            currentNode.name = "NODE" + i.ToString();
            listAllNodes.InsertAtEnd(currentNode.GetComponent<NodeControl>());
        }
    }
    private void ConnectNodes()
    {
        arrayNodeRowsConnections=textNodesConnections.text.Split("\n");
        for(int i = 0;i < listAllNodes.GetCount(); ++i)
        {
            arrayNodeColumnsConnections=arrayNodeRowsConnections[i].Split(";");
            for(int j= 0;j < arrayNodeColumnsConnections.Length-1;++j)
            {
                listAllNodes.GetAtPosition(i).AddAdjacentNode(listAllNodes.GetAtPosition(int.Parse(arrayNodeColumnsConnections[j])),
                    float.Parse(arrayNodeColumnsConnections[j + 1]));
            }
        }
    }
    private void SetInitialNode()
    {
        currentEnemy.SetNewPosition(listAllNodes.GetAtPosition(0).gameObject.transform.position);
    }
}
