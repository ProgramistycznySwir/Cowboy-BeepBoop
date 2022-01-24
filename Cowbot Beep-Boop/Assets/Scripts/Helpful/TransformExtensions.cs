using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cowbot_Beep_Boop.Helpful
{
    public static class TransformExtensions
    {
        public static T[] GetComponentsInChildrenWithDepthOne<T>(this Transform transform)
        {
            List<T> result = new();
            foreach(Transform child in transform)
            {
                var component = child.GetComponent<T>();
                if(component is not null)
                    result.Add(component);
            }
            return result.ToArray();
        }
        static T[] GetComponentsInChildrenWithDepth<T>(this Transform transform, int depth = int.MaxValue)
        {
            throw new NotImplementedException("This method causes stack-overflow!");
            if(depth is int.MaxValue)
                return transform.GetComponentsInChildren<T>();
            if(depth <= 0 || transform.childCount is 0)
                return new T[]{transform.GetComponent<T>()};
            
            List<T> result = new();
            foreach(Transform child in transform)
                result.AddRange(child.GetComponentsInChildrenWithDepth<T>(depth-1));
            return result.ToArray();
        }
    }
}