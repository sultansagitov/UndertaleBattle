using System;
using Godot;
using Godot.Collections;

public partial class Stats : Node2D
{
    public Main main;
    public List characters;

    public readonly Dictionary<string, string> CharactersList = new()
    {
       { "Melody", "ff0000" },
       { "Ron", "0080ff" },
       { "John", "ff8080" },
       { "Vix", "ff8000" },
       { "Lorein", "8080ff" }
    };

    public override void _Ready()
    {
        main = GetNode<Main>("/root/Main");
        characters = GetNode<List>("Characters");

        var Characters = new string[3] { "Melody", "Lorein", "Ron" };////////////////////////////////////

        for (int Index = 0; Index < Characters.Length; Index++)
        {
            string Name = Characters[Index];
            characters.Data[Index] = Name;
            //characters.Colors[Index] = new Color(CharactersList[Name]);
        }
    }

    public override void _PhysicsProcess(double Delta) { }
}
