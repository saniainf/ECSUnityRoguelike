using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutinesTest : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine(GlobalCoroutine());
    }

    IEnumerator GlobalCoroutine()
    {
        for (int i = 0; i < 5; i++)
            RegularCoroutine(i).ParallelCoroutinesGroup(this, "test");

        while (CoroutineExtension.GroupProcessing("test"))
            yield return null;

        Debug.Log("Group 1 finished");

        for (int i = 10; i < 15; i++)
            RegularCoroutine(i).ParallelCoroutinesGroup(this, "anotherTest");

        while (CoroutineExtension.GroupProcessing("anotherTest"))
            yield return null;

        Debug.Log("Group 2 finished");
    }

    IEnumerator RegularCoroutine(int id)
    {
        int iterationsCount = Random.Range(1, 5);

        for (int i = 1; i <= iterationsCount; i++)
        {
            yield return new WaitForSeconds(1);
        }

        Debug.Log(string.Format("{0}: Coroutine {1} finished", Time.realtimeSinceStartup, id));
    }
}
