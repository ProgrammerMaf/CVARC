﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CVARC.V2
{
    public class Competitions
    {
        public readonly LogicPart Logic;
        public readonly EnginePart Engine;
        public readonly ManagerPart Manager;

        public Competitions(LogicPart logic, EnginePart engine, ManagerPart manager)
        {
            this.Logic = logic;
            this.Engine = engine;
            this.Manager = manager;

        }

        public IWorld Create(Dictionary<string,string> commandLineArguments, IEnvironment environment)
        {
            environment.Initialize(commandLineArguments, this);
            Logic.World.Initialize(this, environment);
            return Logic.World;
        }
    }
}