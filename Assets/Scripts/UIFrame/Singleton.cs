using UnityEngine;
using System.Collections;

/***
 * Singleton.cs
 *
 * @author LiuQing
 */
namespace Xsjm
{
    public class Singleton<T> where T :class,new ()
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (null == instance)
                {
                    instance = new T();
                }
                return instance;
            }
        }
    }
}
