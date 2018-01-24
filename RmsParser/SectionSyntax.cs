using System;
using System.Collections.Generic;
using System.Text;

namespace RmsParser
{
  public class SectionSyntax : SyntaxNonTerminal
  {
    public string Name { get; set; }

    public List<SyntaxNonTerminal > Statements { get; set; }

    public SectionSyntax(string name,List<SyntaxNonTerminal> statements)
    {
      Name = name;
      Statements = statements;
    }
  }
}
