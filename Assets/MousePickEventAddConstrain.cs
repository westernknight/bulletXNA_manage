using UnityEngine;
using System.Collections;


public class MousePickEventAddConstrain : MonoBehaviour {

    public float rayDistance = 100f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        bool isMouseOver = DoRaycast();
        Debug.Log(isMouseOver);
        return;
        if (isMouseOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("here");
            }
        }
	}
    bool DoRaycast()
    {
        var testObject = gameObject;

        // ActionHelpers uses a cache to try and minimize Raycasts

        return IsMouseOver( rayDistance, 1);
    }
    private int mousePickLayerMaskUsed;
    private float mousePickDistanceUsed;
    private float mousePickRaycastTime;
    public RaycastHit mousePickInfo;
    void DoMousePick(float distance,int layerMask)
    {
        if (Camera.main!=null)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out mousePickInfo, distance, layerMask))
            {
                mousePickLayerMaskUsed = layerMask;
                mousePickDistanceUsed = distance;
                mousePickRaycastTime = Time.frameCount;
            }
            
        }
        return;
    }
    public GameObject MouseOver(float distance,int layerMask)
    {
        if (mousePickRaycastTime != Time.frameCount)
        {
            DoMousePick(distance, layerMask);
        }
        if (mousePickDistanceUsed<distance)
        {
            DoMousePick(distance, layerMask);
        }
        if (mousePickLayerMaskUsed==layerMask)
        {
            DoMousePick(distance, layerMask);
        }
        if (mousePickInfo.collider==null)
        {
            return null;
        }
        if (mousePickInfo.distance>=distance)
        {
            return null;
        }
        return mousePickInfo.collider.gameObject;
    }
    public  bool IsMouseOver(float distance,int layerMask)
    {
        return (gameObject==MouseOver(distance, layerMask));
    }
}
