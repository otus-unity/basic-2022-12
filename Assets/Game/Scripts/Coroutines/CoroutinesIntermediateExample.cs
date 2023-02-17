using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coroutines
{
    public class CoroutinesIntermediateExample : MonoBehaviour
    {
        [ContextMenu("TestForLoop")]
        private void TestForLoop()
        {
            var count = 5;
            for (var i = 0; i < count; i++)
            {
                Debug.Log($"CoroutinesIntermediateExample.TestForLoop: {i}");
            }
        }

        [ContextMenu("TestForeachLoop")]
        private void TestForeachLoop()
        {
            var integers = new[] { 1, 2, 3, 4, 5 };
            foreach (var i in integers)
            {
                Debug.Log($"CoroutinesIntermediateExample.TestForeachLoop: {i}");
            }

            List<int> integersList = new List<int>(3);
            integersList.Add(10);
            integersList.Add(20);
            integersList.Add(30);
            foreach (var i in integersList)
            {
                Debug.Log($"CoroutinesIntermediateExample.TestForeachLoop: {i}");
            }

            var dictionary = new Dictionary<string, MonoBehaviour>();
            dictionary.Add("first", this);
            dictionary.Add("second", null);
            foreach (var keyAndValue in dictionary)
            {
                Debug.Log($"CoroutinesIntermediateExample.TestForeachLoop: {keyAndValue.Key}, {keyAndValue.Value}");
            }
        }

        [ContextMenu("TestForeachLoopWithExplicitEnumerator")]
        private void TestForeachLoopWithExplicitEnumerator()
        {
            foreach (var element in GetNumbersEnumerator())
            {
                Debug.Log($"CoroutinesIntermediateExample.TestForeachLoopWithExplicitEnumerator: {element}");
            }
        }

        // jIMPORTANT Dive into IEnumerable and IEnumerator
        private IEnumerable GetNumbersEnumerator()
        {
            yield return 3;
            yield return 5;
            yield return 8;
        }

        [ContextMenu("TestEndlessList")]
        private void TestEndlessList()
        {
            var protection = 0;
            foreach (var element in EndlessList())
            {
                protection += 1;
                if (protection == 10)
                    return;

                Debug.Log($"CoroutinesIntermediateExample.TestEndlessList: element = {element}");
            }
        }

        private IEnumerable EndlessList()
        {
            var i = 0;
            while (true)
            {
                i += 1;
                yield return i;
            }
        }

        [ContextMenu("TestNotSoEndlessList")]
        private void TestNotSoEndlessList()
        {
            foreach (var element in NotSoEndlessList(3))
            {
                Debug.Log($"CoroutinesIntermediateExample.TestNotSoEndlessList: element = {element}");
            }
        }

        private IEnumerable NotSoEndlessList(int count)
        {
            var i = 0;
            while (i < count)
            {
                i += 1;
                yield return i;
            }
        }
    }
}