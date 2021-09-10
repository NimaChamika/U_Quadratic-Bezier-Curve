using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputHndler : MonoBehaviour
{
    #region PROPERTIES
    private bool mLock;

	private Vector3 mX;
	private Vector3 mTempX;

	private Vector3 mPo;
	private Vector3 mTempPo;

	[SerializeField] private GameObject PointRed;
	[SerializeField] private GameObject PointBlue;

	private List<Vector2> P_V;

	private Vector3 mPrevPo;
	private Vector3 mNextPo;
    #endregion

    #region UNITY CALLBACKS
    void Start(){

		P_V = new List<Vector2>();
		mLock = false;
	
	}
		
	void Update(){

		mPo = Camera.main.ScreenToWorldPoint(Input.mousePosition);//mPo position is captured in each Update() call!

		if(!mLock && Input.GetMouseButtonDown(0)){

			mLock = true;//this is to prevent calling the same Input.GetMouseButtonDown(0) twice!
			mX=mPo;
		}
			
		if(mLock){

			mTempX = mPo;

			//Controll points to draw the Curve will be only recorded if the distance from the last point is greater than 1 
			//1 is the sensitivity of the Curve

			if(Vector3.Distance(mTempX,mX)>0.5f){ 

				mTempPo = new Vector3(mPo.x,mPo.y,0);
				GameObject.Instantiate(PointRed,mTempPo+new Vector3(0,0,0.01f),Quaternion.identity);
				P_V.Add(mTempPo);

				mX= mTempX;
			}
			else{

				GameObject.Instantiate(PointBlue,new Vector3(mPo.x,mPo.y,0.01f),Quaternion.identity);

			}

		}



		if(Input.GetMouseButtonUp(0))
        {
			mLock = false;
            CreateBezierCurve.Instance.ProcessControlPoints(P_V);		
		}
			

	}
    #endregion

}
