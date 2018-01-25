using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmsParser
{
  public class IfSyntax:SyntaxNonTerminal
  {
    public string Identifier { get; set; }

    public List<SyntaxNonTerminal > Body { get; set; }

    public List<ElseIfSyntax> ElseIfs { get; set; }

    public List<SyntaxNonTerminal> Else { get; set; }

    public IfSyntax(string id, List<SyntaxNonTerminal> body, List<ElseIfSyntax> elseIfs,List<SyntaxNonTerminal> elseClause)
    {
      Identifier = id;
      Body = body;
      ElseIfs = elseIfs;
      Else = elseClause;
    }
  }
}
