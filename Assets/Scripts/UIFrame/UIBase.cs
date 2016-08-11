using UnityEngine;
using System.Collections;

/***
 * UIBase.cs
 *
 * @author LiuQing
 */
namespace Xsjm
{
    public class UIBase : MonoBehaviour
    {
        //UI打开时带的参数
        protected object Data
        {
            get;
            private set;
        }

        public string UIName
        {
            get { return this.gameObject.name; }
        }

        public bool IsOpen
        {
            get
            {
                return this.gameObject.activeSelf;
            }
        }

        private void Awake()
        {
            OnAwake();
        }

        private void Start()
        {
            OnStart();
        }

        #region Public Methods

        public void Open()
        {
            NGUITools.SetActive(this.gameObject, true);
            OnOpen(Data);
        }

        public void Close()
        {
            OnClose();
            NGUITools.SetActive(this.gameObject, false);
        }

        public void Destroy()
        {
            OnDestroy();
            Object.Destroy(this.gameObject);
        }

        public void SetData(object data)
        {
            this.Data = data;
        }

        #endregion

        protected virtual void OnAwake()
        {

        }

        protected virtual void OnStart()
        {

        }

        protected virtual void OnOpen(object data = null)
        {

        }

        protected virtual void OnClose()
        {

        }

        protected virtual void OnDestroy()
        {

        }
    }
}
