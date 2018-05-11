﻿using BepuPhysics.Collidables;
using BepuUtilities;
using System;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using Quaternion = BepuUtilities.Quaternion;

namespace BepuPhysics.CollisionDetection.CollisionTasks
{
    public struct SpherePairDistanceTester : IPairDistanceTester<SphereWide, SphereWide>
    {
        public void Test(ref SphereWide a, ref SphereWide b, ref Vector3Wide offsetB, ref QuaternionWide orientationA, ref QuaternionWide orientationB,
            out Vector<int> intersected, out Vector<float> distance, out Vector3Wide closestA, out Vector3Wide normal)
        {
            Vector3Wide.Length(ref offsetB, out var centerDistance);
            //Note the negative 1. By convention, the normal points from B to A.
            var inverseDistance = new Vector<float>(-1f) / centerDistance;
            Vector3Wide.Scale(ref offsetB, ref inverseDistance, out normal);
            distance = centerDistance - a.Radius - b.Radius;

            var negativeRadiusA = -a.Radius;
            Vector3Wide.Scale(ref normal, ref negativeRadiusA, out closestA);
            intersected = Vector.LessThanOrEqual(distance, Vector<float>.Zero);
        }
    }

    //TODO: Could just use an analytic time of impact for two spheres if there's ever a performance issue.
}
