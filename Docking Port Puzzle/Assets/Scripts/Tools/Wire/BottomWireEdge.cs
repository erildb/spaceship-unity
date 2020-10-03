using UnityEngine;
using Utils;

public class BottomWireEdge : Tool, IUsable
{
    [SerializeField]
    private GameEvent m_objectReleased;

    [HideInInspector]
    public WireSocket.Colors socketColor;

    public TopWireEdge topEdge;

    private WireSocket m_connectedSocket;

    private bool m_attached = false;

    private void Update()
    {
        //Release Wire if top is attached and user tries to pull it more than a specific length
        if (Vector3.Magnitude(transform.position - topEdge.transform.position) > 2f)
            GetReleased();
    }

    public void PerformAction(GameObject socket)
    {
        if (m_attached)
            return;

        if (!topEdge.attached)
            return;

        var wireSocket = socket.GetComponent<WireSocket>();

        if (!wireSocket)
            return;

        if (wireSocket.position == WireSocket.Position.TOP)
            return;

        m_attached = true;

        GetComponent<Rigidbody>().isKinematic = true;
        transform.SetParent(topEdge.transform);

        m_transformInfo.Stop();
        m_transformInfo = Tweener.Start(new FloatTweenSettings
        {
            duration = 0.5f,
            from = 0,
            to = 1,
            easing = Interpolations.Sinusoidal.InOut,
            onChanged = (amt) =>
            {
                transform.position = Vector3.Lerp(transform.position, socket.transform.position, amt);
            }
        }, this);

        socketColor = wireSocket.color;
        m_connectedSocket = wireSocket;

        if (topEdge.wireState == TopWireEdge.WireState.UNDAMAGED)
            wireSocket.AddPair(this);

        m_objectReleased.Raise();
    }

    protected override void GrabAction(Transform grabber)
    {
        if (!topEdge.attached)
            return;

        if (m_attached && topEdge.wireState == TopWireEdge.WireState.UNDAMAGED)
            m_connectedSocket.RemovePair(this);

        m_attached = false;
        transform.SetParent(grabber);
        GetComponent<Rigidbody>().isKinematic = true;
    }

    protected override void ReleaseAction()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        transform.SetParent(null);
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
