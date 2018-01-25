using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmsParser
{
  public class IdSyntax:NoArgSyntax
  {
    public string Id { get; set; }

    public IdSyntax(string name,string id)
      :base(name)
    {
      Id = id;
    }
  }
}
