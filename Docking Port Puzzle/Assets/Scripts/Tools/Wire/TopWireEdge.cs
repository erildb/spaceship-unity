using UnityEngine;
using Utils;

public class TopWireEdge : Tool, IUsable
{
    [SerializeField]
    public WireState wireState;

    [SerializeField]
    private GameEvent m_objectReleased;

    [SerializeField]
    private Transform m_bottomEdge;

    public bool attached = false;

    public WireSocket.Colors socketColor;

    [System.Serializable]
    public enum WireState
    {
        UNDAMAGED,
        DAMAGED
    }

    private void Start()
    {
        switch (wireState)
        {
            case WireState.UNDAMAGED:
                gameObject.GetComponent<LineRenderer>().material = Resources.Load("Materials/Wire", typeof(Material)) as Material;
                break;
            case WireState.DAMAGED:
                gameObject.GetComponent<LineRenderer>().material = Resources.Load("Materials/WireBroken", typeof(Material)) as Material;
                break;
        }
    }

    public void PerformAction(GameObject socket)
    {
        if (attached)
            return;

        if (socket?.GetComponent<WireSocket>()?.position != WireSocket.Position.TOP)
            return;

        attached = true;

        GetComponent<Rigidbody>().isKinematic = true;
        transform.SetParent(socket.transform);

        m_transformInfo.Stop();
        m_transformInfo = Tweener.Start(new FloatTweenSettings
        {
            duration = 0.5f,
            from = 0,
            to = 1,
            easing = Interpolations.Sinusoidal.InOut,
            onChanged = (amt) =>
            {
                transform.position = Vector3.Lerp(transform.position, socket.transform.position - new Vector3(0, 0.03f, 0), amt);
            }
        }, this);

        socketColor = socket.GetComponent<WireSocket>().color;

        m_objectReleased.Raise();
    }
    //Do not grab if it is attached. Can be detached only with wirecutter
    public override void GetGrabbed(Transform grabber)
    {
        if (attached)
            return;

        base.GetGrabbed(grabber);
    }
    //Do not released if it is attached. Can be detached only with wirecutter
    public override void GetReleased()
    {
        if (attached)
            return;

        base.GetReleased();
    }

    protected override void GrabAction(Transform grabber)
    {
        transform.SetParent(grabber);
        GetComponent<Rigidbody>().isKinematic = true;
    }

    protected override void ReleaseAction()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        transform.SetParent(null);
    }

    public void Detach()
    {
        attached = false;
        GetReleased();
        m_bottomEdge.GetComponent<Tool>().GetReleased();
        m_bottomEdge.SetParent(transform);
    }

    protected override void ShowAnnotation()
    {
        Debug.Log("No Annotation");
    }

    protected override void HideAnnotation()
    {
        Debug.Log("No Annotation");
    }
}
