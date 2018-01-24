using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmsParser
{
  public class DefineSyntax:SyntaxNonTerminal
  {
    public string Identifier { get; set; }

    public DefineSyntax(string identifier)
    {
      Identifier = identifier;
    }
  }
}
