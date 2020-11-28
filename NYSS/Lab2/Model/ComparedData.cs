using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Model
{
    public class ComparedData
    {
        public Data OldData { get; }
        public Data NewData { get; }

        public ComparedData(Data oldData, Data newData)
        {
            OldData = oldData;
            NewData = newData;
        }
    }
}
