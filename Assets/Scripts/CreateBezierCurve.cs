using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBezierCurve : MonoBehaviour
{
    #region PROPERTIES
    public static CreateBezierCurve Instance { get; private set; }

    [SerializeField] private int stepLimit;
    [SerializeField] private GameObject spawnObj;

    private float stepCount;
    private List<List<Vector2>> processedControlPointList;
    #endregion

    #region UNITY CALLBACKS 
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        
    }

    private void Start()
    {
        stepCount = 0;
        processedControlPointList = new List<List<Vector2>>();
        //ProcessControlPoints();
        //GetCubicBerzierCurvePathPoints();
    }
    #endregion

    #region CURVE METHODS
    private void GetCubicBerzierCurvePathPoints()
    {
        Vector2 newPoint;
        Vector2 v1;
        Vector2 v2;
        Vector2 v3;
        float t = 0;

        for (int i=0;i<processedControlPointList.Count;i++)
        {
            stepCount = 0;
            while (stepCount <= stepLimit)
            {
                //P = (1−t)2P1 + 2(1−t)tP2 + t2P3

                t = stepCount / stepLimit;
                v1 = Mathf.Pow((1 - t), 2) * processedControlPointList[i][0];
                v2 = 2 * (1 - t) * t * processedControlPointList[i][1];
                v3 = Mathf.Pow(t, 2) * processedControlPointList[i][2];

                newPoint = v1 + v2 + v3;
                Instantiate(spawnObj, new Vector3(newPoint.x, newPoint.y ,0), Quaternion.identity);

                stepCount++;
            }
        }
    }

    internal void ProcessControlPoints(List<Vector2> v2List)
    {
        //FOR CUBIC BEZIER CURVE WE NEED 3 POINTS.
        //int limit = transformArray.Length+1 / 3;

        for (int i=0;i< v2List.Count; i+=2)
        {
            if(i+2 < v2List.Count)
            {
                List<Vector2> lst = new List<Vector2>();

                lst.Add(v2List[i]);
                lst.Add(v2List[i + 1]);
                lst.Add(v2List[i + 2]);

                processedControlPointList.Add(lst);
            }
           
        }

        GetCubicBerzierCurvePathPoints();


        //for (int i = 0; i < v2List.Length; i += 2)
        //{
        //    if (i + 2 < v2List.Length)
        //    {
        //        List<Vector2> lst = new List<Vector2>();

        //        lst.Add(new Vector2(v2List[i].position.x, v2List[i].position.z));
        //        lst.Add(new Vector2(v2List[i + 1].position.x, v2List[i + 1].position.z));
        //        lst.Add(new Vector2(v2List[i + 2].position.x, v2List[i + 2].position.z));

        //        processedControlPointList.Add(lst);
        //    }

        //}


    }
    #endregion
}
