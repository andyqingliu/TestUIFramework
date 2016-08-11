using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***
 * testListAdd.cs
 *
 * @author LiuQing
 */
namespace Xsjm
{
    public class testListAdd : MonoBehaviour
    {
        private List<TestObj> srcList = new List<TestObj>();
        private List<TestObj> targetList = new List<TestObj>();

        void Awake()
        {
            InitSrcList();
            InitTestList();
        }

        void Start()
        {
            for (int i = 0; i < srcList.Count; i++)
            {
                TestObj srcObj = srcList[i];
                for (int j = 0; j < targetList.Count; j++)
                {
                    TestObj to = targetList[j];
                    if (!(to.Id == srcObj.Id && to.No == srcObj.No))
                    {
                        targetList.Add(srcObj);
                    }
                }
            }
        }

        private void InitSrcList()
        {
            TestObj to1 = new TestObj(0, "OOOO", 1, "This is 0000");
            TestObj to2 = new TestObj(8, "OOOO", 9, "This is 0000");
            TestObj to3 = new TestObj(1, "OOOO", 2, "This is 0000");
            srcList.Add(to1);
            srcList.Add(to2);
            srcList.Add(to3);
        }

        private void InitTestList()
        {
            for (int i = 0; i < 3; i++)
            {
                TestObj to = new TestObj(i, i + "KKKK", i + 1, "This is KKKK" + (i + 1).ToString());
                targetList.Add(to);
            }
        }
    }

    public class TestObj
    {
        public int Id;
        public string Name;
        public int No;
        public string Desc;

        public TestObj(int id,string name,int no,string desc)
        {
            this.Id = id;
            this.Name = name;
            this.No = no;
            this.Desc = desc;
        }
    }
}
