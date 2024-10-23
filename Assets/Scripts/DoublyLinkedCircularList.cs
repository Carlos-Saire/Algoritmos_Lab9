using System;
public class DoublyLinkedCircularList<T>
{
    public class Node
    {
        public Node Next { get; set; }
        public Node Previos { get; set; }
        public T Value { get; set; }
        public Node(T value)
        {
            Value = value;
            Next = null;
            Previos = null;
        }
    }

    private Node Hear;
    private int Count = 0;
 
    public int GetCount()
    {
        return Count;
    }
    public void InsertAtStart(T value)
    {
        Node newNode = new Node(value);
        if (Hear == null)
        {
            Hear = newNode;
            Hear.Next = Hear;
            Hear.Previos = Hear;
        }
        else
        {
            Node lastNode = Hear.Previos;
            newNode.Next = Hear;
            newNode.Previos = lastNode;
            Hear.Previos = newNode;
            lastNode.Next = newNode;
            Hear = newNode;
        }
        ++Count;
    }
    public void InsertAtEnd(T value)
    {
        Node newNode = new Node(value);
        if (Hear == null)
        {
            InsertAtStart(value);
        }
        else
        {
            Node lastNode = Hear.Previos;
            lastNode.Next = newNode;
            newNode.Previos = lastNode;
            newNode.Next = Hear;
            Hear.Previos = newNode;
            ++Count;
        }
    }
    public void InsertAtPosition(T value, int position)
    {
        Node newNode = new Node(value);
        if (Hear == null)
        {
            throw new NullReferenceException("La lista cirular esta vacia");
        }
        else if (position == 0)
        {
            InsertAtStart(value);
        }
        else if (position > Count)
        {
            throw new NullReferenceException("Fuera de los limites de la lista");
        }
        else if (position == Count)
        {
            InsertAtEnd(value);
        }
        else
        {
            int iterator = 0;
            Node tmp = Hear;
            while (iterator < position)
            {
                tmp = tmp.Next;
                ++iterator;
            }
            Node previosNode = tmp.Previos;
            tmp.Previos = newNode;
            previosNode.Next = newNode;
            newNode.Previos = previosNode;
            newNode.Next = tmp;
            ++Count;
        }
    }
    public void ModifyAtStart(T value)
    {
        if (Hear == null)
        {
            throw new NullReferenceException("La lista esta vacia");
        }
        else
        {
            Hear.Value = value;
        }
    }
    public void ModifyAtEnd(T value)
    {
        if (Hear == null)
        {
            throw new NullReferenceException("La lista esta vacia");
        }
        else
        {
            Hear.Previos.Value = value;
        }
    }
    public void ModifyAtPosition(T value, int position)
    {
        if (Hear == null)
        {
            throw new NullReferenceException("La lista cirular esta vacia");
        }
        else if (position == 0)
        {
            ModifyAtStart(value);
        }
        else if (position > Count)
        {
            throw new NullReferenceException("Fuera de los limites de la lista");
        }
        else if (position == Count)
        {
            ModifyAtEnd(value);
        }
        else
        {
            int iterator = 0;
            Node tmp = Hear;
            while (iterator < position)
            {
                tmp = tmp.Next;
                ++iterator;
            }
            tmp.Value = value;
        }
    }
    public Node GetAtHeat()
    {
        return Hear;
    }
    public T GetAtStart()
    {
        if (Hear == null)
        {
            throw new NullReferenceException("La lista esta vacia");
        }
        else
        {
            return Hear.Value;
        }
    }
    public T GetAtEnd()
    {
        if (Hear == null)
        {
            throw new NullReferenceException("La lista esta vacia");
        }
        else
        {
            return Hear.Previos.Value;
        }
    }
    public T GetAtPosition(int position)
    {
        if (Hear == null)
        {
            throw new NullReferenceException("La lista cirular esta vacia");
        }
        else if (position == 0)
        {
            return GetAtStart();
        }
        else if (position > Count)
        {
            throw new NullReferenceException("Fuera de los limites de la lista");
        }
        else if (position == Count)
        {
            return GetAtEnd();
        }
        else
        {
            int iterator = 0;
            Node tmp = Hear;
            while (iterator < position)
            {
                tmp = tmp.Next;
                ++iterator;
            }
            return tmp.Value;
        }
    }
    public void DeleteAtStart()
    {
        if (Hear == null)
        {
            throw new NullReferenceException("La lista esta vacia");
        }
        else
        {
            Node lastNode = Hear.Previos;
            Node NextNode = Hear.Next;
            lastNode.Next = NextNode;
            NextNode.Previos = lastNode;
            Hear = NextNode;
            --Count;
        }
    }
    public void DeleteAtEnd()
    {
        if (Hear == null)
        {
            throw new NullReferenceException("La lista esta vacia");
        }
        else
        {
            Node lastNode = Hear.Previos.Previos;
            lastNode.Next = Hear;
            Hear.Previos = lastNode;
            --Count;
        }
    }
    public void DeleteAtPosition(int position)
    {
        if (Hear == null)
        {
            throw new NullReferenceException("La lista cirular esta vacia");
        }
        else if (position == 0)
        {
            DeleteAtStart();
        }
        else if (position > Count)
        {
            throw new NullReferenceException("Fuera de los limites de la lista");
        }
        else if (position == Count)
        {
            DeleteAtEnd();
        }
        else
        {
            int iterator = 0;
            Node tmp = Hear;
            while (iterator < position)
            {
                tmp = tmp.Next;
                ++iterator;
            }
            Node PreviosNode = tmp.Previos;
            Node NextNode = tmp.Next;
            PreviosNode.Next = NextNode;
            NextNode.Previos = PreviosNode;
            --Count;
        }
    }
}
