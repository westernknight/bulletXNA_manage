﻿using UnityEngine;
using System.Collections;
using BulletXNA;
using BulletXNA.BulletCollision;
using BulletXNA.BulletDynamics;
using BulletXNA.LinearMath;
using System.Collections.Generic;
public class BulletBox : BulletObject
{

    

    // Use this for initialization
    void Start()
    {
        BoxCollider collider = GetComponent<BoxCollider>();

        BoxShape colShape = new BoxShape(new IndexedVector3(collider.size.x / 2, collider.size.y / 2, collider.size.z / 2));

        IndexedVector3 bulletScale = new IndexedVector3();
        bulletScale.X = transform.localScale.x;
        bulletScale.Y = transform.localScale.y;
        bulletScale.Z = transform.localScale.z;
        colShape.SetLocalScaling(ref bulletScale);

        LocalCreateRigidBody(colShape);
    }

}
