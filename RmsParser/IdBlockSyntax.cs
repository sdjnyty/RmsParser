using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmsParser
{
  public class IdBlockSyntax:BlockSyntax
  {
    public string Identifier { get; set; }

    public IdBlockSyntax(string name, string id,List<SyntaxNonTerminal>body )
      : base(name, body)
    {
      Identifier = id;
    }
  }
}
