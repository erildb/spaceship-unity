using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Container : MonoBehaviour, IOpenable
{
    [SerializeField]
    private Transform m_pivot;

    private TweenInfo POIInfo;
    private bool m_used = false;

    public void Open()
    {
        if (m_used)
            return;

        POIInfo.Stop();
        POIInfo = Tweener.Start(new FloatTweenSettings
        {
            duration = 1f,
            from = 0,
            to = 1,
            easing = Interpolations.Sinusoidal.InOut,
            onChanged = (amt) =>
            {
                m_pivot.localEulerAngles = Vector3.Lerp(m_pivot.localEulerAngles, new Vector3(0, 0, 60f), amt);
            },
            onFinished = (amt) =>
            {
                m_used = true;
            }
        }, this);
    }
}
