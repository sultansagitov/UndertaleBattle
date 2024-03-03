using Godot;
using System;
using System.Linq;

public partial class Pause : Node2D
{
	public Main main;

	public Sprite2D soulIn;
	public Marker2D posnode;

	public List pauseMenu;

	public Node2D items;
	
	public List itemlist;
	public List iteminter;

	public Stats stats;
	public List characters;

	public Node2D journal;
	public Node2D config;

	public List currNode;

	public readonly double Speed = 12;

	public Focus focus;

	public override void _Ready()
	{
		Position = new Vector2(-320, -180);
		main = GetNode<Main>("/root/Main");
		soulIn = GetNode<Sprite2D>("Soul-in");

		//items = GD.Load<PackedScene>("res://Assets/Scenes/SingleScenes/Windows/Items.tscn").Instance<Node2D>();
		//stats = GD.Load<PackedScene>("res://Assets/Scenes/SingleScenes/Windows/Stats.tscn").Instance<Stats>();

		//AddChild(GD.Load<PackedScene>("res://Assets/Scenes/SingleScenes/Windows/Journal.tscn").Instance<Node2D>());
		//AddChild(GD.Load<PackedScene>("res://Assets/Scenes/SingleScenes/Windows/Config.tscn").Instance<Node2D>());

		pauseMenu = GetNode<List>("Menu");
		posnode = pauseMenu.GetNode<Marker2D>("pos-0-0");
		posnode.Modulate = new Color("ff0000");
		posnode.GetNode<Label>("Shadow").Visible = false;
		focus = Focus.PMenu;

		currNode = pauseMenu;
	}


	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsActionJustPressed("ui_up") && currNode.Row != 0)
			currNode.Row -= 1;

		if (Input.IsActionJustPressed("ui_down") && currNode.Row != currNode.Rows - 1)
			currNode.Row += 1;

		if (Input.IsActionJustPressed("ui_left") && currNode.Col != 0)
			currNode.Col -= 1;

		if (Input.IsActionJustPressed("ui_right") && currNode.Col != currNode.Cols - 1)
			currNode.Col += 1;


		var newposnode = currNode.GetNode<Marker2D>("pos-" + currNode.Col.ToString() + "-" + currNode.Row.ToString());

		if (posnode != newposnode)
		{
			posnode.Modulate = new Color(1, 1, 1, 1);
			posnode.GetNode<Label>("Shadow").Visible = true;
			posnode = newposnode;
			posnode.Modulate = new Color(1, 0, 0, 1);
			posnode.GetNode<Label>("Shadow").Visible = false;
		}

		soulIn.GlobalPosition = soulIn.GlobalPosition.Lerp(posnode.GlobalPosition, (float)(delta * Speed));


		if (Input.IsActionJustPressed("ui_accept"))
		{
			if (focus == Focus.PMenu)
			{
                focus = currNode.Index switch
                {
                    0 => Focus.PItems,
                    1 => Focus.PStats,
                    2 => Focus.PJournal,
                    3 => Focus.PConfig,
                    4 => Focus.Menu,
                    5 => Focus.QuitGame,
                    _ => Focus.PMenu,
                };
            }
			else if (focus == Focus.PStats)
				GD.Print(main.location.Characters[currNode.Index]);

			if (focus == Focus.PMenu)
			{
				if (HasNode("Items")) RemoveChild(items);
				if (HasNode("Stats")) RemoveChild(stats);
				//if (HasNode("Journal")) RemoveChild(journal);
				//if (HasNode("Config")) RemoveChild(config);

				currNode = pauseMenu;
			}
			else if (focus == Focus.PItems)
			{
				if (!HasNode("Items")) AddChild(items);
				if (HasNode("Stats")) RemoveChild(stats);
				//if (HasNode("Journal")) RemoveChild(journal);
				//if (HasNode("Config")) RemoveChild(config);

				currNode = items.GetNode<List>("ItemList");
			}
			else if (focus == Focus.PItemInters)
			{
				if (!HasNode("Items")) AddChild(items);
				if (HasNode("Stats")) RemoveChild(stats);
				//if (HasNode("Journal")) RemoveChild(journal);
				//if (HasNode("Config")) RemoveChild(config);

				currNode = items.GetNode<List>("ItemInter");
			}
			else if (focus == Focus.PStats)
			{
				if (HasNode("Items")) RemoveChild(items);
				if (!HasNode("Stats")) AddChild(stats);
				//if (HasNode("Journal")) RemoveChild(journal);
				//if (HasNode("Config")) RemoveChild(config);

				currNode = stats.GetNode<List>("Characters");
			}
			else if (focus == Focus.PJournal)
			{
				if (HasNode("Items")) RemoveChild(items);
				if (HasNode("Stats")) RemoveChild(stats);
				//if (!HasNode("Journal")) AddChild(journal);
				//if (HasNode("Config")) RemoveChild(config);

				//currNode = GetNode<List>("Journal");
			}
			else if (focus == Focus.PConfig)
			{
				if (HasNode("Items")) RemoveChild(items);
				if (HasNode("Stats")) RemoveChild(stats);
				//if (HasNode("Journal")) RemoveChild(journal);
				//if (!HasNode("Config")) AddChild(config);

				//currNode = GetNode<List>("Config");
			}
        }

        if (Input.IsActionJustPressed("ui_cancel"))
		{
            switch (focus)
            {
                case Focus.PMenu:
                    focus = Focus.Overworld;
                    GetParent().RemoveChild(this);
                    break;
                case Focus.PItems:
                case Focus.PItemInters:
                case Focus.PStats:
                case Focus.PJournal:
                case Focus.PConfig:
                    focus = Focus.PMenu;
                    break;
            }

        }
    }
}
