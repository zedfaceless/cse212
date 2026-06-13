public static class Trees
{
    /// <summary>
    /// Given a sorted list (sorted_list), create a balanced BST.
    /// </summary>
    public static BinarySearchTree CreateTreeFromSortedList(int[] sortedNumbers)
    {
        var bst = new BinarySearchTree(); // Create an empty BST to start with 
        InsertMiddle(sortedNumbers, 0, sortedNumbers.Length - 1, bst);
        return bst;
    }

    /// <summary>
    /// Insert the middle of the range into the BST, then recurse on each half.
    /// </summary>
    private static void InsertMiddle(int[] sortedNumbers, int first, int last, BinarySearchTree bst)
    {
        // PROBLEM 5:
        // Base case: if first passes last, there is nothing left in this range.
        if (first > last)
            return;

        // Find the middle of the current range and insert it. Inserting the
        // middle first keeps the tree balanced.
        int middle = (first + last) / 2;
        bst.Insert(sortedNumbers[middle]);

        // Recurse on the left half (before middle) and the right half (after middle).
        InsertMiddle(sortedNumbers, first, middle - 1, bst);
        InsertMiddle(sortedNumbers, middle + 1, last, bst);
    }
}