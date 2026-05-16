using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue 3 items with different priorities, then dequeue all of them.
    // Items added in order: ("A", 1), ("B", 5), ("C", 3)
    // Expected Result: Dequeue should return "B" first (priority 5), then "C" (priority 3),
    // then "A" (priority 1).
    // Defect(s) Found: The loop in Dequeue stopped one short of the end (index < Count - 1),
    // so the last item was never compared. Also, the dequeued item was never removed from
    // the queue, so calling Dequeue repeatedly kept returning the same value.
    public void TestPriorityQueue_HighestPriorityFirst()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 3);

        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
        Assert.AreEqual("A", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue multiple items where two share the highest priority.
    // Items added in order: ("A", 3), ("B", 5), ("C", 5), ("D", 2)
    // Expected Result: Dequeue should return "B" first (it was added before "C", and both
    // have the highest priority 5), then "C", then "A", then "D".
    // Defect(s) Found: The comparison used >= instead of >, which caused later items with
    // equal priority to incorrectly overwrite earlier ones. Per requirements, ties should
    // be broken by FIFO order (the earlier item wins).
    public void TestPriorityQueue_TieBreakingFifoOrder()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 3);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 5);
        priorityQueue.Enqueue("D", 2);

        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
        Assert.AreEqual("A", priorityQueue.Dequeue());
        Assert.AreEqual("D", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Verify that items are actually removed from the queue after Dequeue.
    // Add one item, dequeue it, then attempt to dequeue from the now-empty queue.
    // Expected Result: First Dequeue returns "Solo". Second Dequeue throws
    // InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: Dequeue did not actually remove the item from the queue,
    // so the queue stayed populated after dequeueing. Fixed by adding RemoveAt.
    public void TestPriorityQueue_RemovesItemFromQueue()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Solo", 1);

        Assert.AreEqual("Solo", priorityQueue.Dequeue());

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Expected InvalidOperationException because queue should be empty.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }

    [TestMethod]
    // Scenario: Verify that the LAST item added is also a candidate for highest priority.
    // Items added in order: ("A", 1), ("B", 2), ("C", 9) - "C" added last has highest priority.
    // Expected Result: Dequeue returns "C" first.
    // Defect(s) Found: The loop bound was "index < _queue.Count - 1", which skipped the
    // final item. So when the highest priority item was at the end of the queue, it was
    // never selected. Fixed by changing the bound to "index < _queue.Count".
    public void TestPriorityQueue_LastItemCanBeHighestPriority()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 2);
        priorityQueue.Enqueue("C", 9);

        Assert.AreEqual("C", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Try to dequeue from an empty queue.
    // Expected Result: An InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: None - the empty-queue check and exception throw were already
    // implemented correctly in the original Dequeue method.
    public void TestPriorityQueue_EmptyQueueThrows()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Expected InvalidOperationException when dequeueing from empty queue.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }
}