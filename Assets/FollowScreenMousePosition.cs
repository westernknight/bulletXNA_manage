using UnityEngine;
using System.Collections;

public class FollowScreenMousePosition : MonoBehaviour
{

    Vector3 world;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetposition = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mouseposition = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            mouseposition.z = targetposition.z;
            world.x = Camera.main.ScreenToWorldPoint(mouseposition).x;
            world.y = Camera.main.ScreenToWorldPoint(mouseposition).y;

            transform.position = world;
        }
    }
}