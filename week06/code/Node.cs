public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        // PROBLEM 1: Ignore duplicates. If the value already exists at this
        // node, do nothing so the tree stays a set of unique values.
        if (value == Data)
            return;

        if (value < Data)
        {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else
        {
            // Insert to the right
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
    }

    public bool Contains(int value)
    {
        // PROBLEM 2: Found it at this node.
        if (value == Data)
            return true;

        if (value < Data)
        {
            // The value would be in the left subtree if it exists.
            if (Left is null)
                return false;            // nowhere left to look
            return Left.Contains(value); // keep searching left
        }
        else
        {
            // The value would be in the right subtree if it exists.
            if (Right is null)
                return false;             // nowhere right to look
            return Right.Contains(value); // keep searching right
        }
    }

    public int GetHeight()
    {
        // PROBLEM 4: Height = 1 + the taller of the two subtrees.
        // A null child contributes a height of 0.
        int leftHeight = Left is null ? 0 : Left.GetHeight();
        int rightHeight = Right is null ? 0 : Right.GetHeight();
        return 1 + Math.Max(leftHeight, rightHeight);
    }
}