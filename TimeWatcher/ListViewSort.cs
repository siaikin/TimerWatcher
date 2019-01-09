using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeWatcher
{

    class ListViewSort : IComparer
    {
        public delegate String GetComapreValue(Object obj);

        public static int ASCENDING = 1;
        public static int DESCENDING = -1;

        public GetComapreValue CompareMethod;

        // 默认升序排序
        public int SortDirectioon = ASCENDING;

        public ListViewSort()
            :this(ASCENDING)
        {
        }

        public ListViewSort(int direction)
        {
            this.SortDirectioon = direction;
        }

        public int Compare(Object x, Object y)
        {
            String xValue = CompareMethod(x);
            String yValue = CompareMethod(y);
            return String.Compare(xValue, yValue) * SortDirectioon;
        }
    }
}
