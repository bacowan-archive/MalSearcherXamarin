using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace MalSearcher.Model.MalProxy.Interfaces
{
    public interface WebParser<OutType>
    {
        OutType Parse(string html);
    }
}
