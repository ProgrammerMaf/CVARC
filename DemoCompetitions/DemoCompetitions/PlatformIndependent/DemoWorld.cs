﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIRLab.Mathematics;
using CVARC.V2;

namespace DemoCompetitions
{
    public class DemoWorld : World<object, IDemoWorldManagerPrototype>, ISimpleMovementWorld
    {
        protected override IEnumerable<IActor> CreateActors()
        {
            yield return new DemoRobot(TwoPlayersId.Left);
            yield return new DemoRobot(TwoPlayersId.Right);
        }

        public double LinearVelocityLimit
        {
            get { return 200; }
        }

        public AIRLab.Mathematics.Angle AngularVelocityLimit
        {
            get { return Angle.FromGrad(200); }
        }



        public override void Initialize(Competitions competitions, IEnvironment environment)
        {
            base.Initialize(competitions, environment);
            Engine.Collision += CollisionDetected;
        }

        void CollisionDetected(string arg1, string arg2)
        {
            
        }

    }
}
