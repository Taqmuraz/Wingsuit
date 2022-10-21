using UnityEngine;

using Core.UserInterface.UnityUI;
using Core.UserInterface.Basement;
using Core.UserInterface;
using System.Collections.Generic;
using System;

public sealed class Menu : MonoBehaviour
{
    IExtendedCanvas canvas;
    DrawOperationIdentifier menuDraw = new DrawOperationIdentifier();
    IMenu currentMenu;
    [SerializeField] string entryMenu;

    static readonly Dictionary<string, Func<IMenu>> menuConstructors = new Dictionary<string, Func<IMenu>>()
    {
        ["Main"] = () => new MainMenu(),
        ["Settings"] = () => new SettingsMenu(),
    };

    private void Start()
    {
        canvas = UnityCanvasExtended.CreateCanvas();
        currentMenu = menuConstructors[entryMenu]();
    }
    private void Update()
    {
        canvas.Clear();
        canvas.BeginDraw(menuDraw);

        currentMenu = currentMenu.DrawMenu(canvas);

        canvas.EndDraw();
    }
}