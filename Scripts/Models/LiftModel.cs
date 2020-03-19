using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftModel {
    public int currentLevel;
    public List<int> floorsQueue;
    public LiftModel()
    {
        floorsQueue = new List<int>();
    }
    public void Enqueue(int floor)
    {
        floorsQueue.Add(floor);
    }

    public void Dequeue()
    {
        Debug.Log($"{floorsQueue[0]} dequed");
        floorsQueue.RemoveAt(0);
    }
}