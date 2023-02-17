using System.Collections;
using UnityEngine;

namespace Coroutines
{
    public class CoroutinesAdvancedExample : MonoBehaviour
    {
        void Start()
        {
            TestCustomCoroutine();

            // TestBuiltInCoroutines();

            // TestStopStringCoroutines();
            // TestStopEnumeratorCoroutines();
            // TestStopCoroutineCoroutines();
        }

        private void Update()
        {
            if (Time.time < 0.1)
            {
                Debug.Log($"CoroutinesAdvancedExample.Update: Time.time = {Time.time}");
            }
        }

        [ContextMenu("TestCustomEnumerator")]
        private void TestCustomCoroutine()
        {
            Debug.Log($"{GetType().Name}.TestCustomCoroutine: ");

            StartCoroutine(new CustomEnumerator());
        }

        [ContextMenu("TestCustomEnumerator2")]
        private void TestBuiltInCoroutines()
        {
            StartCoroutine(MyCoroutine());
        }

        private IEnumerator MyCoroutine()
        {
            yield return null;
            // yield return new WaitForSeconds(1f);
            // yield return new WaitForSecondsRealtime(1f);
            // yield return new WaitForFixedUpdate();
            // yield return new WaitForEndOfFrame();
            // CustomYieldInstruction
            // yield return new WaitUntil(() => true);
            // yield return new WaitWhile(() => false);
            // yield break;
        }

        #region Stop string coroutines

        [ContextMenu("TestStopStringCoroutines")]
        private void TestStopStringCoroutines()
        {
            StartCoroutine("StringCoroutine");
            // StartCoroutine("StopRegularCoroutine");
        }

        private IEnumerator StopRegularCoroutine()
        {
            yield return new WaitForSeconds(0.5f);
            StopCoroutine("StringCoroutine");
        }

        private IEnumerator StringCoroutine()
        {
            Debug.Log("CoroutinesAdvancedExample.RegularCoroutine: start");

            yield return new WaitForSeconds(1f);

            Debug.Log("CoroutinesAdvancedExample.RegularCoroutine: before stop");
            StopCoroutine("StringCoroutine");
            Debug.Log("CoroutinesAdvancedExample.RegularCoroutine: after stop");

            yield return new WaitForSeconds(1f);
            Debug.Log("CoroutinesAdvancedExample.RegularCoroutine: finish");
        }

        #endregion


        #region Stop enumerator coroutines

        private IEnumerator _enumeratorCoroutine;

        [ContextMenu("TestStopEnumeratorCoroutines")]
        private void TestStopEnumeratorCoroutines()
        {
            _enumeratorCoroutine = EnumeratorCoroutine();
            StartCoroutine(_enumeratorCoroutine);

            StartCoroutine(StopEnumeratorCoroutine());
        }

        private IEnumerator StopEnumeratorCoroutine()
        {
            yield return new WaitForSeconds(0.5f);
            StopCoroutine(_enumeratorCoroutine);
        }

        private IEnumerator EnumeratorCoroutine()
        {
            Debug.Log("CoroutinesAdvancedExample.EnumeratorCoroutine: start");

            yield return new WaitForSeconds(1f);

            Debug.Log("CoroutinesAdvancedExample.EnumeratorCoroutine: finish");
        }

        #endregion


        #region Stop coroutine coroutines

        private Coroutine _coroutineCoroutine;

        [ContextMenu("TestStopCoroutineCoroutines")]
        private void TestStopCoroutineCoroutines()
        {
            _coroutineCoroutine = StartCoroutine(CoroutineCoroutine());

            StartCoroutine(StopCoroutineCoroutine());
        }

        private IEnumerator StopCoroutineCoroutine()
        {
            yield return new WaitForSeconds(0.5f);
            StopCoroutine(_coroutineCoroutine);
        }

        private IEnumerator CoroutineCoroutine()
        {
            Debug.Log("CoroutinesAdvancedExample.CoroutineCoroutine: start");

            yield return new WaitForSeconds(1f);

            Debug.Log("CoroutinesAdvancedExample.CoroutineCoroutine: finish");
        }

        #endregion
    }

    public class CustomEnumerator : IEnumerator
    {
        private int _i = -1;

        public bool MoveNext()
        {
            Debug.Log($"MyEnumerator.MoveNext: i = {_i} on time: {Time.time}");

            _i += 1;

            return _i < 5;
        }

        public void Reset()
        {
            _i = -1;
        }

        public object Current => _i;
    }
}