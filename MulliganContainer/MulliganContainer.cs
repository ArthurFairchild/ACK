using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SmartBot.Database;
using SmartBot.Plugins;
using SmartBot.Plugins.API;



public class MulliganContainer

{
    //public ACK.Style
    
    

    public Style MyStyle { get; set; }
    public List<Card.Cards> MyDeck { get; set; }
    public Style EnemyStyle { get; set; }
    public bool LogData { get; set; }

    public List<Card.Cards> Choices { get; set; }
    public Card.CClass OpponentClass { get; set; }
    public Card.CClass OwnClass { get; set; }

    /// <summary>
    /// All of the below drops originate from choices
    /// </summary>
    public List<Card.Cards> ZeroDrops { get; set; }
    public List<Card.Cards> OneDrops { get; set; }
    public List<Card.Cards> TwoDrops { get; set; }
    public List<Card.Cards> ThreeDrops { get; set; }
    public List<Card.Cards> FourDrops { get; set; }
    public List<Card.Cards> FivePlusDrops { get; set; }

    public bool HasTurnOne { get; set; }
    public bool HasTurnTwo { get; set; }
    public bool HasTurnThree { get; set; }
    public bool Coin { get; set; }
    public DeckClassification MyDeckClassification { get; set; }
    public DeckClassification MyChoicesClassification { get; set; }
    //needed
    public MulliganContainer(List<Card.Cards> choices, Card.CClass opponentClass, Card.CClass ownClass)
    {
        Choices = choices;
        OpponentClass = opponentClass;
        OwnClass = ownClass;
        Coin = choices.Count > 3;

        LogData = false;
        MyDeck = Bot.CurrentDeck().ToCardCards();
        MyDeckClassification = new DeckClassification(MyDeck, ownClass);
        MyChoicesClassification = new DeckClassification(choices, ownClass);
        if (Bot.CurrentMode().IsArena())
        {
            MyStyle = Style.Midrange;
            EnemyStyle = Style.Midrange;
        }
        else
        {
            MyStyle = MyDeckClassification.DeckStyle;
            EnemyStyle = Style.Midrange;
        }

        ZeroDrops = Choices.Where(card => CardTemplate.LoadFromId(card).Cost == 0).ToList();
        OneDrops = Choices.Where(card => CardTemplate.LoadFromId(card).Cost == 1).ToList();
        TwoDrops = Choices.Where(card => CardTemplate.LoadFromId(card).Cost == 2).ToList();
        ThreeDrops = Choices.Where(card => CardTemplate.LoadFromId(card).Cost == 3).ToList();
        FourDrops = Choices.Where(card => CardTemplate.LoadFromId(card).Cost == 4).ToList();
        FivePlusDrops = Choices.Where(card => CardTemplate.LoadFromId(card).Cost > 4).ToList();
        HasTurnOne = false;
        HasTurnTwo = false;
        HasTurnThree = false;

    }

    /// <summary>
    /// Mulligan Tester
    /// </summary>

    public MulliganContainer(Bot.Mode mode, List<Card.Cards> choices, Card.CClass opponentClass, Card.CClass ownClass, List<Card.Cards> myDeckList )
    {

        Choices = choices;
        OpponentClass = opponentClass;
        OwnClass = ownClass;
        Coin = choices.Count > 3;
        
        LogData = false;
        MyDeck = myDeckList;
        MyDeckClassification = new DeckClassification(MyDeck, ownClass);
        MyChoicesClassification = new DeckClassification(choices, ownClass);
        if (mode.IsArena())
        {
            MyStyle = Style.Midrange;
            EnemyStyle = Style.Midrange;
        }
        else
        {
            MyStyle = MyDeckClassification.DeckStyle;
            EnemyStyle = Style.Midrange;
        }

        ZeroDrops = Choices.Where(card => CardTemplate.LoadFromId(card).Cost == 0).ToList();
        OneDrops = Choices.Where(card => CardTemplate.LoadFromId(card).Cost == 1).ToList();
        TwoDrops = Choices.Where(card => CardTemplate.LoadFromId(card).Cost == 2).ToList();
        ThreeDrops = Choices.Where(card => CardTemplate.LoadFromId(card).Cost == 3).ToList();
        FourDrops = Choices.Where(card => CardTemplate.LoadFromId(card).Cost == 4).ToList();
        FivePlusDrops = Choices.Where(card => CardTemplate.LoadFromId(card).Cost > 4).ToList();
        HasTurnOne = false;
        HasTurnTwo = false;
        HasTurnThree = false;

    }



}
