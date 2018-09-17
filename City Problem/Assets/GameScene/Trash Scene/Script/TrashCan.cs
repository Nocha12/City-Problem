using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour {

	Coroutine scaleC;

	public IEnumerator ScaleEffect(Transform target){      
        if (scaleC != null)
            StopCoroutine(scaleC);
        
        scaleC = StartCoroutine(Scaling(target, new Vector3(1.2f, 1.2f, 1), 0.1f));
        yield return new WaitForSeconds(0.1f);
        
        if (scaleC != null)
            StopCoroutine(scaleC);
        
        scaleC = StartCoroutine(Scaling(target, new Vector3(1, 1, 1), 0.1f));
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator Scaling(Transform target, Vector3 scaleEnd, float time)
    {
        float t = 0f;

        Vector3 startScale = target.localScale;
        AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        while (t < 1f)
        {
            t += Time.deltaTime / time;

            target.localScale = Vector3.Lerp(startScale, scaleEnd, curve.Evaluate(t));

            yield return null;
        }
    }   
}
