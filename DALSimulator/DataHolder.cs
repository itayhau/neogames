using CoreProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALSimulator
{
    public static class DataHolder
    {
        public static List<PurchaseInfo> Data { get; set; }

        public static Dictionary<string, int> MaprquestIdToLastIndex { get; private set; }

        static DataHolder()
        {
            MaprquestIdToLastIndex = new Dictionary<string, int>();
        }
    }
}
