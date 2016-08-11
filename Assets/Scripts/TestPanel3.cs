using UnityEngine;
using System.Collections;

/***
 * TestPanel3.cs
 *
 * @author LiuQing
 */
namespace Xsjm
{
    public class TestPanel3 : UIBase
    {
        public UIButton Btn1;
        public UIButton Btn2;
        public UIButton Btn3;
        public UIButton Btn4;
        public UIButton Btn5;
        public UIButton Btn6;

        public UIButton Close;

        protected override void OnAwake()
        {
            EventDelegate.Add(Btn1.onClick, OnBtn1);
            EventDelegate.Add(Btn2.onClick, OnBtn2);
            EventDelegate.Add(Btn3.onClick, OnBtn3);
            EventDelegate.Add(Btn4.onClick, OnBtn4);
            EventDelegate.Add(Btn5.onClick, OnBtn5);
            EventDelegate.Add(Btn6.onClick, OnBtn6);

            EventDelegate.Add(Close.onClick, OnBtnClose);
        }

        protected override void OnOpen(object data = null)
        {
            Debug.Log(string.Format("OnOpen is calling,param is {0}", data == null ? "NULL" : data.ToString()));
        }

        protected override void OnClose()
        {
            Debug.Log(string.Format("OnClose is calling!"));
        }

        private void OnBtn1()
        {
            Debug.Log(string.Format("Click Btn1"));
        }

        private void OnBtn2()
        {
            Debug.Log(string.Format("Click Btn2"));
        }

        private void OnBtn3()
        {
            Debug.Log(string.Format("Click Btn3"));
        }

        private void OnBtn4()
        {
            Debug.Log(string.Format("Click Btn4"));
        }

        private void OnBtn5()
        {
            Debug.Log(string.Format("Click Btn5"));
        }

        private void OnBtn6()
        {
            Debug.Log(string.Format("Click Btn6"));
            UIManager.Instance.Show<TestPanel2>(string.Format("woshi1234"));
        }

        private void OnBtnClose()
        {
            UIManager.Instance.Hide<TestPanel3>();
        }
    }
}
