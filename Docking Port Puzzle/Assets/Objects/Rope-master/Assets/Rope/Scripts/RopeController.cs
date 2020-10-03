using UnityEngine;

namespace Rope
{
    public class RopeController : MonoBehaviour
    {
        [SerializeField]
        private GameObject fragmentPrefab;

        [SerializeField]
        private int fragmentCount = 4;

        [SerializeField]
        private GameObject[] fragments;

        private float[] xPositions;
        private float[] yPositions;
        private float[] zPositions;

        private CatmullRomSpline splineX;
        private CatmullRomSpline splineY;
        private CatmullRomSpline splineZ;

        private int splineFactor = 2;

        void Start()
        {
            fragments[0].transform.position = transform.position;

            var lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = (fragmentCount - 1) * splineFactor + 1;

            xPositions = new float[fragments.Length];
            yPositions = new float[fragments.Length];
            zPositions = new float[fragments.Length];

            splineX = new CatmullRomSpline(xPositions);
            splineY = new CatmullRomSpline(yPositions);
            splineZ = new CatmullRomSpline(zPositions);
        }

        void LateUpdate()
        {
            var lineRenderer = GetComponent<LineRenderer>();

            for (var i = 0; i < fragments.Length; i++)
            {
                var position = fragments[i].transform.position;
                xPositions[i] = position.x;
                yPositions[i] = position.y;
                zPositions[i] = position.z;
            }

            for (var i = 0; i < (fragments.Length - 1) * splineFactor + 1; i++)
            {
                lineRenderer.SetPosition(i, new Vector3(
                    splineX.GetValue(i / (float)splineFactor),
                    splineY.GetValue(i / (float)splineFactor),
                    splineZ.GetValue(i / (float)splineFactor)));
            }
        }
    }
}
