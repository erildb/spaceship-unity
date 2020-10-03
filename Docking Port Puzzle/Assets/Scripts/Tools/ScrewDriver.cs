using UnityEngine;

public class ScrewDriver : Tool, IUsable
{
    public void PerformAction(GameObject obj)
    {
        obj?.GetComponent<IScrewable>()?.Remove();
    }

    protected override void GrabAction(Transform grabber)
    {
        HideAnnotation();
        transform.SetParent(grabber);
        GetComponent<Rigidbody>().isKinematic = true;
    }

    protected override void ReleaseAction()
    {
        ShowAnnotation();
        GetComponent<Rigidbody>().isKinematic = false;
        transform.SetParent(null);
    }
}