using System.Collections;
using UnityEngine;

namespace Coroutines
{
    public class CoroutinesBasicExample : MonoBehaviour
    {
        private bool _isRepeating;

        void Start()
        {
            StartCoroutine(SimpleCoroutine());

            // _isRepeating = true;
            // StartCoroutine(CoroutineWithCondition());

            // Alternative way of starting coroutines
            // StartCoroutine("SimpleCoroutine");
            // StartCoroutine(nameof(SimpleCoroutine));
        }

        #region Simple coroutine

        // jIMPORTANT test in play and editor modes
        [ContextMenu("StartSimpleCoroutine")]
        private void StartSimpleCoroutine()
        {
            StartCoroutine(SimpleCoroutine());
        }

        private IEnumerator SimpleCoroutine()
        {
            Debug.Log($"CoroutinesBasicExample.SimpleCoroutine: before {Time.time}");

            yield return new WaitForSeconds(1f);

            Debug.Log($"CoroutinesBasicExample.SimpleCoroutine: after {Time.time}");
        }

        #endregion


        private IEnumerator CoroutineWithCondition()
        {
            Debug.Log($"CoroutinesBasicExample.CoroutineWithCondition: start {Time.time}");

            while (_isRepeating)
            {
                yield return null;
            }

            Debug.Log($"CoroutinesBasicExample.CoroutineWithCondition: finish {Time.time}");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log($"CoroutinesBasicExample.Update: stop repeating {Time.time}");

                _isRepeating = false;
            }
        }
    }
}