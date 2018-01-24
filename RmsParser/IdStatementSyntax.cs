using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmsParser
{
  public class IdStatementSyntax:NoArgStatement
  {
    public string Id { get; set; }

    public IdStatementSyntax(string name,string id)
      :base(name)
    {
      Id = id;
    }
  }
}
