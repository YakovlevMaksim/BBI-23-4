using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Myserial
{
    public abstract class MySerializeBase
    {
        public abstract bool Serialize<T>(T t, string fileName) where T : class;
        public abstract T Deserialize<T>(string fileName) where T : class;
    }
}