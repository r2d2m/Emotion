﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardManager : CardManager
{
    public PlayerCardManager() : base()
    {

    }

    public override void PutCurrentCard(Role self, Role target)
    {

        if (expenseCurrent >= currentCard.Cost && currentCard != Card.EmptyCard)
        {
            View.Instance.ShowPlayerPutCard(currentCardIndex);
            expenseCurrent -= currentCard.Cost;

            cards.Remove(currentCard);

            if (self.GetBuffManager.CheckBuff(BuffType.AfterPutCard))
            {
                self.GetBuffManager.BuffProcess(BuffType.AfterPutCard, self);
            }

            currentCard.TakeEffect(self, target);
            self.CardDiscard.Add(currentCard);


            View.Instance.ShowPlayerCards();

            currentCard = Card.EmptyCard;
            currentCardIndex = -1;
        }
    }

    public override void ExpenseReset()
    {
        if (Player.Instance.GetBuffManager.CheckLayer("WearyBuff") > 0)
            expenseCurrent = expenseMax - 1;
        else
            expenseCurrent = expenseMax;

    }

    public override void PutSelectCard(Role self, Role target, int index)
    {
        currentCard = cards[index];
        if (expenseCurrent >= currentCard.Cost)
        {
            View.Instance.ShowPlayerPutCard(index);
            Debug.Log("player打出一张" + currentCard.cardname);
            if (index == 0 && cards.Count == 1)
            {
                leftCard = Card.EmptyCard;
                rightCard = Card.EmptyCard;
            }
            else if (index == 0)
            {
                leftCard = Card.EmptyCard;
                rightCard = cards[index + 1];
            }
            else if (index == (cards.Count - 1))
            {
                leftCard = cards[index - 1];
                rightCard = Card.EmptyCard;

            }
            else
            {
                leftCard = cards[index - 1];
                rightCard = cards[index + 1];
            }

            expenseCurrent -= currentCard.Cost;

            cards.Remove(currentCard);

            if (self.GetBuffManager.CheckBuff(BuffType.AfterPutCard))
            {
                self.GetBuffManager.BuffProcess(BuffType.AfterPutCard, self);
            }
            currentCard.TakeEffect(self, target);
            self.CardDiscard.Add(currentCard);


            View.Instance.ShowPlayerCards();

            currentCard = Card.EmptyCard;
            currentCardIndex = -1;
        }


    }
}
