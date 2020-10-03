using UnityEngine;
using Utils;

public class Screw : Tool, IScrewable
{
    [SerializeField]
    private GameState m_gameState;

    private TweenInfo POIInfo;
    private bool m_used = false;

    public void Remove()
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
                transform.localPosition = Vector3.Lerp(transform.localPosition, transform.localPosition + new Vector3(0, 0, 0.0002f), amt);
                transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, transform.eulerAngles + new Vector3(0, 0, -360f), amt);
            },
            onFinished = (amt) =>
            {
                GetComponent<Rigidbody>().isKinematic = false;
                m_used = true;
                m_gameState.AddScrew();
            }

        }, this);
    }

    public void Insert()
    {
        Debug.Log("Not usable for this task");
    }

    public override void GetGrabbed(Transform grabber)
    {
        Debug.Log("Not Grabbable for this task");
    }

    protected override void GrabAction(Transform grabber)
    {
        Debug.Log("Not Grabbable for this task");
    }

    public override void GetReleased()
    {
        Debug.Log("Not Grabbable for this task");
    }

    protected override void ReleaseAction()
    {
        Debug.Log("Not Grabbable for this task");
    }
}