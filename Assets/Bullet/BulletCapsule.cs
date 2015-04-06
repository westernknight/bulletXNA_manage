using UnityEngine;
using System.Collections;
using BulletXNA;
using BulletXNA.BulletCollision;
using BulletXNA.BulletDynamics;
using BulletXNA.LinearMath;
using System.Collections.Generic;
public class BulletCapsule : BulletObject
{
     
    void Start()
    {
        CapsuleCollider collider = GetComponent<CapsuleCollider>();


        ConvexShape colShape = new CapsuleShape(collider.radius, collider.height/2);
        IndexedVector3 bulletScale = new IndexedVector3();
        bulletScale.X = transform.localScale.x;
        bulletScale.Y = transform.localScale.y;
        bulletScale.Z = transform.localScale.z;
        colShape.SetLocalScaling(ref bulletScale);

        LocalCreateRigidBody(colShape);
    }

}
