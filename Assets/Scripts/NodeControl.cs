using UnityEngine;
public class NodeControl : MonoBehaviour
{
    DoublyLinkedCircularList<NodeControl> listAdjacentNodes=new DoublyLinkedCircularList<NodeControl>();
    public float energy;
    public void AddAdjacentNode(NodeControl adjacentNode,float energy)
    {
        listAdjacentNodes.InsertAtEnd(adjacentNode);
        this.energy = energy;
    }
    public NodeControl GetAdjacentNode()
    {
        return listAdjacentNodes.GetAtPosition(Random.Range(0, listAdjacentNodes.GetCount()));
    }
}
