﻿using System;
using Sudoku.Data;
using Sudoku.Frontend.Models;
using Sudoku.Frontend.Views;

namespace Sudoku.Frontend.Controllers
{
    public class StartController : IController
    {
        private readonly StartView _view;
        private readonly StartModel _model;
        private readonly IGameReader _gameReader;
        
        public StartController()
        {
            _model = new StartModel();
            _view = new StartView(_model);
            _gameReader = new GameReader();
        }
        
        public void Update(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.D: // Toggle display mode
                    _model.SimpleDisplay = !_model.SimpleDisplay;
                    break;
                case ConsoleKey.F: // Change file path
                    _model.SudokuPath = null;
                    break;
                case ConsoleKey.S: // Start game (if file is set)
                    if (_model.SudokuPath != null)
                    {
                        App.Instance.SetController(new SudokuController(_gameReader.Read(_model.SudokuPath), _model.SimpleDisplay));
                    }
                    break;
            }
            
            _view.Update();
        }
    }
}