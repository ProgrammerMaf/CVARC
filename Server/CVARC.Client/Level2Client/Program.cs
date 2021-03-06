﻿using CommonTypes;
using CVARC.Basic.Controllers;
using CVARC.Network;
using ClientBase;
using RepairTheStarship.Sensors;
using MapHelper;

namespace Level2Client
{
    internal class Program
    {
        private static readonly ClientSettings Settings = new ClientSettings
            {
                BotName = Bots.Azura,
                Side = Side.Left,
                LevelName = LevelName.Level2,
                MapNumber = 4
            };

        private static void Main(string[] args)
        {
            using (var server = new CvarcClient(args, Settings).GetServer<PositionSensorsData>())
            {
                var sensorData = server.Run().SensorsData;
                var map = sensorData.BuildMap();
                var robotLocator = new RobotLocator(map);
                var path = PathSearcher.FindPath(map, map.GetDiscretePosition(map.CurrentPosition), new Point(2, 1));//(2, 1) - just random point

                foreach (var direction in path)
                {
                    foreach (var command in robotLocator.GetCommandsByDirection(direction))
                    {
                        sensorData = server.SendCommand(command);
                        robotLocator.Update(sensorData);
                    }
                }
                server.SendCommand(new Command { Action = CommandAction.WaitForExit });
            }
        }
    }
}
