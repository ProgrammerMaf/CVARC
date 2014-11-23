﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CVARC.V2;
using CVARC.V2.SimpleMovement;

namespace Demo
{
    public partial class MovementLogicPart :  LogicPart<
                                                           MovementWorld,
                                                           SimpleMovementTwoPlayersKeyboardControllerPool,
                                                           MovementRobot,
                                                           SimpleMovementPreprocessor,
                                                           NetworkController<SimpleMovementCommand> 
                                                >
    {
        public MovementLogicPart()
            : base(new[] { ControllerId }, GetDefaultSettings)
        {
            Bots["Square"]=()=>new SquareWalkingBot(50);
            Bots["Random"]=()=>new RandomWalkingBot(50);

            LoadTests();
        }



        public const string ControllerId = "Left";

        static Settings GetDefaultSettings()
        {
            return new Settings
            {
                TimeLimit = double.PositiveInfinity,
                OperationalTimeLimit = 1,
                Controllers = 
                {
                    new ControllerSettings { ControllerId=ControllerId, Name="Square", Type= ControllerType.Bot },
                }
            };
        }
    }
}
