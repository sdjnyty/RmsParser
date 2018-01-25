using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmsParser
{
  public class ElseIfSyntax:SyntaxNonTerminal
  {
    public List<SyntaxNonTerminal> Body { get; set; }

    public ElseIfSyntax(List<SyntaxNonTerminal> body)
    {
      Body = body;
    }
  }
}
