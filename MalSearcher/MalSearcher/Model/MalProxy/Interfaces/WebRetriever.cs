using System;
using System.Collections.Generic;
using System.Text;

namespace MalSearcher.Model.MalProxy.Interfaces
{
    public interface WebRetriever<InType, OutType>
    {
        OutType Get(InType param);
        OutType Get(InType param, string username, string password);
    }
}
