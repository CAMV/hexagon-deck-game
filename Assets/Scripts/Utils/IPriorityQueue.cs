using System;

public interface IPriorityQueue<T, in TKey> where TKey : IComparable<TKey>
{
    /// <summary>
    /// Inserts an item with a priority
    /// </summary>
    /// <param name="item"></param>
    /// <param name="priority"></param>
    void Insert(T item, TKey priority);

    bool isEmpty();

    /// <returns>The element with the highest priority</returns>
    T Top();

    /// <summary>
    /// Deletes the element with the highest priority
    /// </summary>
    /// <returns>The deleted element</returns>
    T Pop();
}