﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OnEstPasBenevole.src.Interface;

namespace OnEstPasBenevole;

public class Game1 : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;



    private readonly Menu menu;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        _graphics.ApplyChanges();

        Window.AllowUserResizing = true;

        menu = new Menu(Content.Load<SpriteFont>("Arial"), GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

    }

    protected override void Initialize()
    {

        menu.LoadContent(Content, GraphicsDevice);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
        {
            Exit();
        }
        menu.Update(gameTime, GraphicsDevice, Content);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);

        _spriteBatch.Begin();
        menu.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    protected void OnExiting(object sender, EventArgs args)
    {
        menu.Option.SaveData();
        base.OnExiting(sender, (ExitingEventArgs)args);
    }
}
