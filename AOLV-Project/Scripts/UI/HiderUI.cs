using System.Collections;
using UnityEngine;

public class HiderUI
{
    public IEnumerator Hiding(CanvasGroup canvas)
    {
        while (canvas.alpha != 0)
        {
            canvas.alpha = Mathf.MoveTowards(canvas.alpha, 0, 3f * Time.deltaTime);
            yield return null;
        }
    }
}
