using TMPro;
using UnityEngine;
using Utils;

public abstract class Tool : MonoBehaviour
{
    [SerializeField]
    private GameObject m_annotation;
    [SerializeField]
    private TextMeshPro m_text;

    protected TweenInfo m_transformInfo;
    protected TweenInfo m_annotationInfo;

    public virtual void GetGrabbed(Transform grabber)
    {
        GrabAction(grabber);

        m_transformInfo.Stop();
        m_transformInfo = Tweener.Start(new FloatTweenSettings
        {
            duration = 0.5f,
            from = 0,
            to = 1,
            easing = Interpolations.Sinusoidal.InOut,
            onChanged = (amt) =>
            {
                transform.position = Vector3.Lerp(transform.position, grabber.position, amt);
                transform.forward = Vector3.Lerp(transform.forward, grabber.forward, amt);
            }
        }, this);
    }

    protected abstract void GrabAction(Transform grabber);

    public virtual void GetReleased()
    {
        m_transformInfo.Stop();
        ReleaseAction();
    }

    protected abstract void ReleaseAction();

    protected virtual void ShowAnnotation()
    {
        m_annotationInfo.Stop();
        m_annotationInfo = Tweener.Start(new FloatTweenSettings
        {
            duration = 0.2f,
            from = 1f,
            to = 1,
            easing = Interpolations.Sinusoidal.InOut,
            onChanged = (amt) =>
            {
                m_annotation.GetComponent<CanvasGroup>().alpha = amt;
                m_text.alpha = amt;
            },
        }, this);
    }

    protected virtual void HideAnnotation()
    {
        m_annotationInfo.Stop();
        m_annotationInfo = Tweener.Start(new FloatTweenSettings
        {
            duration = 1f,
            from = 1,
            to = 0.2f,
            easing = Interpolations.Sinusoidal.InOut,
            onChanged = (amt) =>
            {
                m_annotation.GetComponent<CanvasGroup>().alpha = amt;
                m_text.alpha = amt;
            },
        }, this);
    }
}
