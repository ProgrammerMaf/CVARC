using System.Collections.Generic;
using System.Windows.Forms;
using AIRLab.Mathematics;
using CVARC.Basic.Controllers;

namespace RepairTheStarship
{
    public class KbController : KeyboardController
    {
        public override IEnumerable<Command> GetCommand(Keys keyData)
        {
            switch(keyData)
            {
                case Keys.W:
                    yield return new Command{ Time=10, Move = 100, RobotId = 0};
                    break;
                case Keys.S:
                    yield return new Command { Time = 10, Move = -100, RobotId = 0 };
                    break;
                case Keys.A:
                    yield return new Command { Time = 10, Angle = Angle.FromGrad(90), RobotId = 0 };
                    break;
                case Keys.D:
                    yield return new Command { Time = 10, Angle = Angle.FromGrad(-90), RobotId = 0 };
                    break;
                case Keys.Q:
                    yield return new Command { Action = CommandAction.Grip, RobotId = 0 };
                    break;
                case Keys.E:
                    yield return new Command { Action = CommandAction.Release, RobotId = 0 };
                    break;
                case Keys.NumPad8:
                    yield return new Command { Move = 100, RobotId = 1 };
                    break;
                case Keys.NumPad5:
                    yield return new Command { Move = -100, RobotId = 1 };
                    break;
                case Keys.NumPad4:
                    yield return new Command { Angle = Angle.FromGrad(90), RobotId = 1 };
                    break;
                case Keys.NumPad6:
                    yield return new Command { Angle = Angle.FromGrad(-90), RobotId = 1 };
                    break;
                case Keys.NumPad7:
                    yield return new Command { Action = CommandAction.Grip, RobotId = 1 };
                    break;
                case Keys.NumPad9:
                    yield return new Command { Action = CommandAction.Release, RobotId = 1 };
                    break;
            }
        }
    }
}