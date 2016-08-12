using UnityEngine;
using System.Collections;

/***
 * UIFrameTest.cs
 *
 * @author LiuQing
 */
namespace Xsjm
{
    public class UIFrameTest : MonoBehaviour
    {
        void Start()
        {
            UIManager.Instance.OnShow = delegate(UIBase ui){
                Debug.Log(string.Format("Show UI Name is:{0}", ui.UIName));
            };
            UIManager.Instance.Show<TestPanel2>();
            
        }
    }
}
