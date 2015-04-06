using UnityEngine;
using System.Collections;
using BulletXNA;
using BulletXNA.BulletCollision;
using BulletXNA.BulletDynamics;
using BulletXNA.LinearMath;
using System.Collections.Generic;
public class BulletSphere : BulletObject
{

    // Use this for initialization
    void Start()
    {
        SphereCollider collider = GetComponent<SphereCollider>();


        ConvexShape colShape = new SphereShape(collider.radius);
        IndexedVector3 bulletScale = new IndexedVector3();
        bulletScale.X = transform.localScale.x;
        bulletScale.Y = transform.localScale.y;
        bulletScale.Z = transform.localScale.z;
        colShape.SetLocalScaling(ref bulletScale);

        LocalCreateRigidBody(colShape);

    }



}
