using System;
using System.Collections.Generic;
using GOLD;

namespace RmsParser
{
  public class SyntaxTree
  {
    private static Parser Parser = new Parser();

    public List<SectionSyntax> Root { get; set; }

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
            return new SyntaxTree {Root = ReadSections((Reduction) Parser.CurrentReduction),};
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
        case ProductionIndex.IfStatement:
          return new IfSyntax((string) reduction[1].Data,
            ReadStatements((Reduction)reduction[2].Data),
            ReadElseIfs((Reduction)reduction[3].Data) ,
            ReadElseClause((Reduction)reduction[4].Data));
        case ProductionIndex.Section:
          var header = (string)((Reduction)reduction[0].Data)[0].Data;
          return new SectionSyntax(header, ReadStatements((Reduction) reduction[1].Data));
        case ProductionIndex.StmtRandomPlacement:
        case ProductionIndex.StmtGroupByTeam:
        case ProductionIndex.StmtNomadResources:
        case ProductionIndex.StmtSetZoneRandomly:
        case ProductionIndex.StmtSetZoneByTeam:
        case ProductionIndex.StmtSetScaleBySize:
        case ProductionIndex.StmtSetScaleByGroups:
        case ProductionIndex.StmtSetAvoidPlayerStartAreas:
        case ProductionIndex.StmtSetFlatTerrainOnly:
        case ProductionIndex.StmtSetScalingToMapSize:
        case ProductionIndex.StmtSetScalingToPlayerNumber:
        case ProductionIndex.StmtSetPlaceForEveryPlayer:
        case ProductionIndex.StmtSetGaiaObjectOnly:
        case ProductionIndex.StmtSetTightGrouping:
        case ProductionIndex.StmtSetLooseGrouping:
          return new NoArgSyntax((string) reduction[0].Data);
        case ProductionIndex.IdStatement:
          return new IdSyntax((string)((Reduction) reduction[0].Data)[0].Data,(string)reduction[1].Data);
        case ProductionIndex.NumStatement:
          return new NumSyntax((string) ((Reduction) reduction[0].Data)[0].Data,
            int.Parse((string) reduction[1].Data));
        case ProductionIndex.BlockStatement:
          return new BlockSyntax((string) ((Reduction) reduction[0].Data)[0].Data,
            ReadStatements((Reduction) ((Reduction) reduction[1].Data)[1].Data));
        case ProductionIndex.NumBlockStatement:
          return new NumBlockSyntax((string)((Reduction)reduction[0].Data)[0].Data,
            int.Parse((string)reduction[1].Data),
            ReadStatements((Reduction)((Reduction)reduction[2].Data)[1].Data));
        case ProductionIndex.IdBlockStatement:
          return new IdBlockSyntax((string) ((Reduction) reduction[0].Data)[0].Data,
            (string)reduction[1].Data,
            ReadStatements((Reduction) ((Reduction) reduction[2].Data)[1].Data));
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

    private static List<SectionSyntax> ReadSections(Reduction reduction)
    {
      var ret = new List<SectionSyntax>();
      AddSection(reduction);
      return ret;

      void AddSection(Reduction red)
      {
        switch ((ProductionIndex) red.Parent.TableIndex())
        {
          case ProductionIndex.Section:
            ret.Add((SectionSyntax) ReadNode(red));
            break;
          case ProductionIndex.Sections:
            AddSection((Reduction) red[0].Data);
            AddSection((Reduction) red[1].Data);
            break;
        }
      }
    }

    private static List<ElseIfSyntax> ReadElseIfs(Reduction reduction)
    {
      var ret = new List<ElseIfSyntax>();
      AddElseIf(reduction);
      return ret;

      void AddElseIf(Reduction red)
      {
        switch ((ProductionIndex) reduction.Parent.TableIndex())
        {
          case ProductionIndex.ElseIfClause0:
            break;
          case ProductionIndex.ElseIfClause1:
            ret.Add(new ElseIfSyntax(ReadStatements((Reduction) red[1].Data)));
            break;
          case ProductionIndex.ElseIfClause2:
            AddElseIf((Reduction) red[0].Data);
            AddElseIf((Reduction) red[1].Data);
            break;
        }
      }
    }

    private static List<SyntaxNonTerminal> ReadElseClause(Reduction reduction)
    {
      switch ((ProductionIndex) reduction.Parent.TableIndex())
      {
        case ProductionIndex.ElseClause:
          return ReadStatements((Reduction) reduction[1].Data);
        case ProductionIndex.ElseClause0:
          return null;
      }
      throw new ArgumentOutOfRangeException();
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
      ElseIfClause1=13,
      ElseIfClause2,
      ElseIfClause0=16,
      ElseClause,
      ElseClause0,
      IfStatement,
      Statements2=34,
      Statements1,
      Section,
      Sections,
      StmtRandomPlacement=39,
      StmtGroupByTeam,
      StmtNomadResources,
      StmtSetZoneRandomly,
      StmtSetZoneByTeam,
      StmtSetScaleBySize,
      StmtSetScaleByGroups,
      StmtSetAvoidPlayerStartAreas,
      StmtSetFlatTerrainOnly,
      StmtSetScalingToMapSize,
      StmtSetScalingToPlayerNumber,
      StmtSetPlaceForEveryPlayer,
      StmtSetGaiaObjectOnly,
      StmtSetTightGrouping,
      StmtSetLooseGrouping,
      IdStatement=59,
      NumStatement=95,
      BlockNameCreatePlayerLands,
      BlockNameCreateLand,
      BlockNameCreateConnectAllPlayersLand,
      BlockNameCreateConnectTeamsLands,
      BlockNameCreateConnectAllLands,
      BlockNameCreateConnectSameLandZones,
      BlockStatement,
      NumBlockStatement=107,
      IdBlockStatement=110,
    }
  }
}