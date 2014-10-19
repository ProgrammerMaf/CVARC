﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using AIRLab;
using AIRLab.Mathematics;
using CVARC.V2;

namespace RepairTheStarship
{
    public class MapSensor : Sensor<Map,IActor>
    {
        WallMapData CreateWallMapData(WallData data, Frame3D location)
        {
            var result = new WallMapData();
            result.Orientation = data.Orientation;
            result.Type = data.Type;
            result.Center = new PointF((float)location.X, (float)location.Y);
            if (result.Orientation == WallOrientation.Horizontal)
            {
                result.FirstEnd = new PointF(result.Center.X - 25, result.Center.Y);
                result.SecondEnd = new PointF(result.Center.X + 25, result.Center.Y);
            }
            else
            {
                result.FirstEnd = new PointF(result.Center.X, result.Center.Y+25);
                result.SecondEnd = new PointF(result.Center.X, result.Center.Y-25);
            }
            return result;
        }

        public override Map Measure()
        {
            var map = new Map();
            map.Details = Actor.World.IdGenerator.GetAllPairsOfType<DetailColor>()
                .Where(z => Actor.World.Engine.ContainBody(z.Item2))
                .Select(z => new Tuple<DetailColor, Frame3D>(z.Item1, Actor.World.Engine.GetAbsoluteLocation(z.Item2)))
                .Select(z => new DetailMapData { Color = z.Item1, Location = new PointF((float)z.Item2.X, (float)z.Item2.Y) })
                .ToArray();
            map.Walls = Actor.World.IdGenerator.GetAllPairsOfType<WallData>()
                .Where(z => Actor.World.Engine.ContainBody(z.Item2))
                .Select(z => new Tuple<WallData, Frame3D>(z.Item1, Actor.World.Engine.GetAbsoluteLocation(z.Item2)))
                .Select(z => CreateWallMapData(z.Item1, z.Item1))
                .ToArray();
            return map;
        }
    }
}
