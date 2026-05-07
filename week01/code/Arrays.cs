public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // PLAN:
        // Step 1: Creating double array sized to hold length multiples.
        // Step 2: loop from i = 0 up to i < length.
        // Step 3: at each position i calculate number * (i + 1) and store it
        //         in the array at position i. use (i + 1) because the first
        //         multiple is 1x the number, not 0x (since i starts at 0).
        // Step 4: after the loop finishes, return the filled array.
        double[] result = new double[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = number * (i + 1);
        }
        return result;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // PLAN:
        // Step 1: slice out the back portion the last 'amount' items.
        //         use GetRange(data.Count - amount, amount) which starts at
        //         index (Count - amount) and grabs 'amount' items.
        // Step 2: slice out the front portion, everything before the back slice.
        //         use GetRange(0, data.Count - amount) which starts at index 0
        //         and grabs (Count - amount) items.
        // Step 3: clear the original list so we can rebuild it in the new order.
        // Step 4: add the BACK portion first (this becomes the new front),
        //         then add the FRONT portion (this becomes the new back).
        List<int> back = data.GetRange(data.Count - amount, amount);
        List<int> front = data.GetRange(0, data.Count - amount);
        data.Clear();
        data.AddRange(back);
        data.AddRange(front);
    }
}
