using UnityEngine;

public class Wrench : Tool, IUsable
{
    public void PerformAction(GameObject obj)
    {
        Debug.Log("Not functional for this task");
    }

    protected override void GrabAction(Transform grabber)
    {
     //   HideAnnotation();
        transform.SetParent(grabber);
        GetComponent<Rigidbody>().isKinematic = true;
    }

    protected override void ReleaseAction()
    {
     //   ShowAnnotation();
        GetComponent<Rigidbody>().isKinematic = false;
        transform.SetParent(null);
    }
}