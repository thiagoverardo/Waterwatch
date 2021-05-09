using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scroll : InventoryItemBase
{
    public HUD hud;
    public string data;
    public string nome;
    public string mensagem;
    public TMP_Text txtDate;
    public TMP_Text txtMessage;

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
        string data = txtDate.text;
        string mensagem = txtMessage.text;
        hud.OpenScrollPanel(data, mensagem);
    }
}
