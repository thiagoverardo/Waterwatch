﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : InventoryItemBase
{
    public HUD hud;
    public string data;
    public string nome;
    public string mensagem;
    public override string Name
    {
        get
        {
            return nome;
        }
    }

    public override void OnDrop()
    {
        hud.CloseScrollPanel();
    }

    public override void OnUse()
    {
        hud.OpenScrollPanel(data, mensagem);
    }
}
