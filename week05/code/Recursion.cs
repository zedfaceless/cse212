using System.Collections;

public static class Recursion
{
    /// <summary>
    /// #############
    /// # Problem 1 #
    /// #############
    /// Using recursion, find the sum of 1^2 + 2^2 + 3^2 + ... + n^2.
    /// </summary>
    public static int SumSquaresRecursive(int n)
    {
        // Base case: nothing to add when n is 0 or negative.
        if (n <= 0)
            return 0;

        // Smaller problem: n^2 plus the sum of all squares below n.
        return (n * n) + SumSquaresRecursive(n - 1);
    }

    /// <summary>
    /// #############
    /// # Problem 2 #
    /// #############
    /// Using recursion, insert permutations of length 'size' from a list
    /// of 'letters' into the results list.
    /// </summary>
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        // Base case: once the built word reaches the desired size, save it.
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        // Smaller problem: try each remaining letter, remove it from the
        // available letters, and add it to the word so far.
        for (int i = 0; i < letters.Length; i++)
        {
            var lettersLeft = letters.Remove(i, 1);
            PermutationsChoose(results, lettersLeft, size, word + letters[i]);
        }
    }

    /// <summary>
    /// #############
    /// # Problem 3 #
    /// #############
    /// Count how many ways there are to climb 's' stairs taking 1, 2, or 3
    /// steps at a time. Uses memoization for performance.
    /// </summary>
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        // On the first call, create the memoization dictionary.
        if (remember == null)
            remember = new Dictionary<int, decimal>();

        // Base Cases
        if (s == 0)
            return 0;
        if (s == 1)
            return 1;
        if (s == 2)
            return 2;
        if (s == 3)
            return 4;

        // If we already solved this stair count, return the saved answer.
        if (remember.ContainsKey(s))
            return remember[s];

        // Solve using recursion, passing 'remember' along to each call.
        decimal ways = CountWaysToClimb(s - 1, remember)
                     + CountWaysToClimb(s - 2, remember)
                     + CountWaysToClimb(s - 3, remember);

        // Remember the result for later use.
        remember[s] = ways;
        return ways;
    }

    /// <summary>
    /// #############
    /// # Problem 4 #
    /// #############
    /// Using recursion, insert all possible binary strings for a given
    /// pattern (containing 0, 1, and * wildcards) into the results list.
    /// </summary>
    public static void WildcardBinary(string pattern, List<string> results)
    {
        // Find the first wildcard.
        int index = pattern.IndexOf('*');

        if (index == -1)
        {
            // Base case: no wildcards left, so this is a complete binary string.
            results.Add(pattern);
        }
        else
        {
            // Smaller problem: replace the first * with 0, then with 1,
            // and recurse on each new (shorter-on-wildcards) pattern.
            WildcardBinary(pattern[..index] + "0" + pattern[(index + 1)..], results);
            WildcardBinary(pattern[..index] + "1" + pattern[(index + 1)..], results);
        }
    }

    /// <summary>
    /// Use recursion to insert all paths that start at (0,0) and end at the
    /// 'end' square into the results list.
    /// </summary>
    public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<ValueTuple<int, int>>? currPath = null)
    {
        // If this is the first time running the function, then we need
        // to initialize the currPath list.
        if (currPath == null)
        {
            currPath = new List<ValueTuple<int, int>>();
        }

        // Add the current position to the path we're building.
        currPath.Add((x, y));

        // If we've reached the end, record this complete path.
        if (maze.IsEnd(x, y))
        {
            results.Add(currPath.AsString());
        }
        else
        {
            // Try moving in each of the four directions, but only if valid.
            if (maze.IsValidMove(currPath, x + 1, y))
                SolveMaze(results, maze, x + 1, y, currPath); // right
            if (maze.IsValidMove(currPath, x - 1, y))
                SolveMaze(results, maze, x - 1, y, currPath); // left
            if (maze.IsValidMove(currPath, x, y + 1))
                SolveMaze(results, maze, x, y + 1, currPath); // down
            if (maze.IsValidMove(currPath, x, y - 1))
                SolveMaze(results, maze, x, y - 1, currPath); // up
        }

        // Backtrack: remove the current position so other paths can reuse
        // this square. Without this, only the first path found would work.
        currPath.RemoveAt(currPath.Count - 1);
    }
}