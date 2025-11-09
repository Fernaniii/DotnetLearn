using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic_Demo;


    // this Mean that to Use This it must be a class and new()/empty constructor
    // you can also use T : notNull
    // you can also use T : BeterList<T> or your custom Generic class
    // you can also use T : IImportance<T> or your Generic Interface
    // you can also use  : class, new()
    // this this allow non reference type
    // this not allow non class
    public class SampleClass<T, U> where T : class, new()
    {

    }
