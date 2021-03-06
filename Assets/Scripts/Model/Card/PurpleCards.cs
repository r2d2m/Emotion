﻿using System;
using UnityEngine;

public class Complain : Card
{
    public Complain() : base(CardName.Complain, CardColor.Purple, 1, 4)
    {
        this.cName = "抱怨";
        this.tip = "减少敌人血量*点，使敌人获得两层消沉/+2点";
        this.tipUpgrade = "减少敌人血量*点，使敌人获得两层消沉/+4点";
        this.oDamage = 10;
    }

    public override void TakeEffect(Role self, Role target)
    {
        //减少敌人血量10点，使敌人获得两层消沉 / +2点
        self.TakeDamage(target, 10 + 2 * self.CardManager.GetBonus(this.color));
        target.Despondent += 2;

        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            //再使敌人获得两层消沉
            target.Despondent += 2;
        }
    }

}

public class DullAtmosphere : Card
{
    public DullAtmosphere() : base(CardName.DullAtmosphere, CardColor.Purple, 2, 4, 10)
    {
        this.cName = "沉闷氛围";
        this.tip = "2回合内，敌人每打出一张卡牌，获得一层消沉";
        this.tipUpgrade = "3回合内，敌人每打出一张卡牌，获得一层消沉";
        this.tipUpgradeTwice = "3回合内，敌人每打出一张卡牌，获得两层消沉";
    }

    public override void TakeEffect(Role self, Role target)
    {
        //两回合内，敌人每打出一张卡牌，获得一层消沉
        target.GetBuffManager.AddBuff(BuffName.DullAtmosphereBuff, 2);

        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            //+1持续回合 
        }
        if (self.CardManager.GetBonus(this.color) > this.upgradeTwice)
        {
            //再获得一层消沉
        }

    }

}

public class WeiYuChouMou : Card
{
    public WeiYuChouMou() : base(CardName.WeiYuChouMou, CardColor.Purple, 1, 3, 8)
    {
        this.cName = "未雨绸缪";
        this.tip = "随机减少一张手牌的费用1点";
        this.tipUpgrade = "随机减少两张手牌的费用1点";
        this.tipUpgradeTwice = "随机减少两张手牌的费用2点";
    }

    public override void TakeEffect(Role self, Role target)
    {
        //随机减少一张手牌的费用1点
        Card card = self.CardManager.GetRandomCard();
        if (card != null)
        {
            self.CardManager.GetRandomCard().Cost--;
        }


        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            //再随机减少一张手牌费用
            self.CardManager.GetRandomCard().Cost--;
        }


        if (self.CardManager.GetBonus(this.color) > this.upgradeTwice)
        {
            //再减少一遍
            self.CardManager.GetRandomCard().Cost--;
        }

    }

}

public class OuDuanSiLian : Card
{
    public OuDuanSiLian() : base(CardName.OuDuanSiLian, CardColor.Purple, 1, 4)
    {
        this.cName = "藕断丝连";
        this.tip = "抽取2张牌，当手牌数小于*时再抽一张牌/+1再抽牌上限";
        this.tipUpgrade = "抽取3张牌，当手牌数小于*时再抽一张牌/+1再抽牌上限";
        this.oDamage = 3;
    }

    public override void TakeEffect(Role self, Role target)
    {
        //抽取2张牌，当手牌数小于3时再抽一张牌 
        self.GetCardsFromLibrary(2);

        if (self.CardManager.Cards.Count < 3)
        {
            self.GetCardsFromLibrary(1);
        }


        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            //+1再抽牌上限
            self.GetCardsFromLibrary(1);
        }

    }

}

public class Depress : Card
{
    public Depress() : base(CardName.Depress, CardColor.Purple, 1, 8)
    {
        this.cName = "忧郁";
        this.tip = "减少敌人血量的*%/+2%";
        this.tipUpgrade = "减少敌人血量的*%，再减少敌人血量20%/+2%";
        this.oDamage = 20;
    }

    public override void TakeEffect(Role self, Role target)
    {
        //减少敌人血量的20%/+2%
        self.TakeDamage(target, (int)((20 + 2 * self.CardManager.GetBonus(this.color)) / 100 * target.HP));

        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            //再减少敌人血量的20%
            self.TakeDamage(target, (int)((20) / 100 * target.HP));
        }

    }

}

public class Blues : Card
{
    public Blues() : base(CardName.Blues, CardColor.Purple, 1, 4)
    {
        this.cName = "蓝调";
        this.tip = "减少敌人血量*点/+2点";
        this.oDamage = 25;
        this.tipUpgrade = "减少敌人血量*点/+4点";
    }

    public override void TakeEffect(Role self, Role target)
    {
        //减少敌人血量25点/+2点
        self.TakeDamage(target, 25 + 2 * self.CardManager.GetBonus(this.color));

        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            //BONUS再+2点
            self.TakeDamage(target, 2 * self.CardManager.GetBonus(this.color));
        }

    }

}


public class Pacify : Card
{
    public Pacify() : base(CardName.Pacify, CardColor.Purple, 2, 4)
    {
        this.cName = "安抚";
        this.tip = "减少敌人血量*%并回复相同数值的自己血量/+1%";
        this.tipUpgrade = "回复40点血量，减少敌人血量*%并回复相同数值的自己血量/+1%";
        this.oDamage = 10;
    }

    public override void TakeEffect(Role self, Role target)
    {
        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            //先回复自己40点血量
            self.GetHeal(40);
        }
        //减少敌人血量10%并回复相同百分比的自己血量/+1%
        self.TakeDamage(target, (int)((10 + 1 * self.CardManager.GetBonus(this.color)) / 100.0 * target.HP));
        self.GetHeal((int)((10 + 1 * self.CardManager.GetBonus(this.color)) / 100.0 * self.HP));



    }

}

public class Compare : Card
{
    public Compare() : base(CardName.Compare, CardColor.Purple, 2, 4)
    {
        this.cName = "攀比";
        this.tip = "如果敌人血量大于血量上限的50%，减少敌人血量*点，如果自己血量小于自己血量上限的50%回复自己血量*点/+3点";
        this.tipUpgrade = "如果敌人血量大于血量上限的50%，减少敌人血量*点，如果自己血量小于自己血量上限的50%回复自己血量*点/+5点";
        this.oDamage = 80;
    }

    public override void TakeEffect(Role self, Role target)
    {

        //如果敌人血量大于血量上限的50%，减少敌人血量80点，如果自己血量小于自己血量上限的50%回复自己血量40点/+3点

        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            //bonus +2
            if (target.HP * 2 > target.HPMax)
            {
                self.TakeDamage(target, 80 + 5 * self.CardManager.GetBonus(this.color));
            }
            if (self.HP * 2 < self.HPMax)
            {
                self.GetHeal(40 + 5 * self.CardManager.GetBonus(this.color));
            }
        }
        else
        {
            if (target.HP * 2 > target.HPMax)
            {
                self.TakeDamage(target, 80 + 3 * self.CardManager.GetBonus(this.color));
            }
            if (self.HP * 2 < self.HPMax)
            {
                self.GetHeal(40 + 3 * self.CardManager.GetBonus(this.color));
            }
        }


    }

}




public class Confess : Card
{
    public Confess() : base(CardName.Confess, CardColor.Purple, 4, 10)
    {
        this.cName = "倾诉";
        this.tip = "移除敌人身上的所有消沉，每移除一层消沉，减少敌人血量的1%";
    }

    public override void TakeEffect(Role self, Role target)
    {

        //移除敌人身上的所有消沉，每移除一层消沉，减少敌人血量的1 %
        //再减少1%

        float tmp;
        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            tmp = 0.02f;
        }
        else
        {
            tmp = 0.01f;
        }

        self.TakeDamage(target, (int)(target.HP * (1 - target.Despondent * tmp)));
    }

}


