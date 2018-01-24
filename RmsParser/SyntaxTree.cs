using System;
using System.Collections.Generic;
using GOLD;

namespace RmsParser
{
  public class SyntaxTree
  {
    private static Parser Parser = new Parser();

    public SyntaxNonTerminal Root { get; set; }

    static SyntaxTree()
    {
      Parser.TrimReductions = true;
      Parser.LoadTables("rms.egt");
    }

    private SyntaxTree() { }

    public static SyntaxTree Parse(string sourceCode)
    {
      Parser.Open(ref sourceCode);
      while (true)
      {
        switch (Parser.Parse())
        {
          case ParseMessage.Accept:
            return new SyntaxTree {Root = ReadNode((Reduction) Parser.CurrentReduction),};
          case ParseMessage.GroupError:
          case ParseMessage.SyntaxError:
            throw new Exception();
        }
      }
    }

    private static SyntaxNonTerminal ReadNode(Reduction reduction)
    {
      switch ((ProductionIndex)reduction.Parent.TableIndex())
      {
        case ProductionIndex.DefineStatement:
          return new DefineSyntax((string) reduction[1].Data);
        case ProductionIndex.PercentStatement1:
          return new PercentChanceSyntax(int.Parse((string)reduction[1].Data),
            ReadStatements((Reduction)reduction[2].Data));
        case ProductionIndex.RandomStatement:
          return new RandomSyntax(ReadPercentStatements((Reduction)reduction[1].Data));
        case ProductionIndex.Section:
          var header = (string)((Reduction)reduction[0].Data)[0].Data;
          return new SectionSyntax(header, ReadStatements((Reduction) reduction[1].Data));
        case ProductionIndex.IdStatement:
          return new IdStatementSyntax((string)((Reduction) reduction[0].Data)[0].Data,(string)reduction[1].Data);

      }
      throw new ArgumentOutOfRangeException();
    }

    private static List<SyntaxNonTerminal> ReadStatements(Reduction reduction)
    {
      var ret = new List<SyntaxNonTerminal>();
      AddStatement(reduction);
      return ret;

      void AddStatement(Reduction red)
      {
        switch ((ProductionIndex)red.Parent.TableIndex())
        {
          case ProductionIndex.Statements2:
            var red0 = (Reduction)red[0].Data;
            switch ((ProductionIndex)red0.Parent.TableIndex())
            {
              case ProductionIndex.Statements2:
                AddStatement(red0);
                break;
              default:
                ret.Add(ReadNode(red0));
                break;
            }
            ret.Add(ReadNode((Reduction)red[1].Data));
            break;
          case ProductionIndex.Statements1:
            ret.Add(ReadNode((Reduction)red[0].Data));
            break;
          default:
            ret.Add(ReadNode(reduction));
            break;
        }
      }
    }

    private static List<PercentChanceSyntax> ReadPercentStatements(Reduction reduction)
    {
      var ret = new List<PercentChanceSyntax>();
      AddPercentStatement(reduction);
      return ret;

      void AddPercentStatement(Reduction red)
      {
        switch ((ProductionIndex) red.Parent.TableIndex())
        {
          case ProductionIndex.PercentStatement1:
            ret.Add((PercentChanceSyntax) ReadNode(reduction));
            break;
          case ProductionIndex.PercentStatement2:
            var red0 = (Reduction)red[0].Data;
            switch ((ProductionIndex) red0.Parent.TableIndex())
            {
              case ProductionIndex.PercentStatement2:
                AddPercentStatement(red0);
                break;
              default:
                ret.Add((PercentChanceSyntax) ReadNode(red0));
                break;
            }
            break;
        }
      }
    }

    private enum ProductionIndex
    {
      DefineStatement,
      PercentStatement1,
      PercentStatement2,
      RandomStatement=4,
      SectionHeaderPlayerSetup,
      SectionHeaderLandGeneration,
      SectionHeaderElevationGeneration,
      Statements2=34,
      Statements1,
      Section,
      IdStatement=59,
    }
  }
}