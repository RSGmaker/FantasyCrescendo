using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HouraiTeahouse.Editor {

    public static class SerializedPropertyUtil {

        /// <summary>
        /// Sets an array SerializedProperty equal to a enumerable source.
        /// </summary>
        /// <typeparam name="T">the type of object to insert</typeparam>
        /// <exception cref="ArgumentNullException">thrown if <paramref name="property"/> is null</exception>
        /// <exception cref="ArgumentException">thrown if <paramref name="property"/> is not an array property</exception>
        /// <param name="property">the array property to edit</param>
        /// <param name="source">the source collection to read values from</param>
        public static void SetArray<T>(this SerializedProperty property, IEnumerable<T> source, int start = 0) 
            where T : Object {
            if(property == null)
                throw new ArgumentNullException();
            if(!property.isArray)
                throw new ArgumentException();
            var i = start;
            foreach (var obj in source) {
                if (i >= property.arraySize)
                    property.InsertArrayElementAtIndex(i);
                property.GetArrayElementAtIndex(i).objectReferenceValue = obj;
                i++;
            }
            property.arraySize = i;
        }

        /// <summary>
        /// Appends elements at the end of an array SerializedProperty
        /// </summary>
        /// <typeparam name="T">the type of object to add to the array</typeparam>
        /// <param name="property">the SerializedProperty to append onto</param>
        /// <param name="source">the source collection to read values from</param>
        public static void AppendArray<T>(this SerializedProperty property, IEnumerable<T> source)
            where T : Object {
            property.SetArray(source, property.arraySize);
        }

    }
}
