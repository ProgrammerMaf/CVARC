﻿using System.Runtime.Serialization;
using CVARC.Basic;
using CVARC.Basic.Sensors;
using CVARC.Core;
using CVARC.Graphics;
using System.Linq;

namespace Gems.Sensors
{
    public class MapSensor : Sensor<MapSensorData>
    {
        private readonly Body root;

        public MapSensor(Robot robot, World world, DrawerFactory factory) 
            : base(robot, world, factory)
        {
            root = robot.Body.TreeRoot;

        }

        public override MapSensorData Measure()
        {
            return new MapSensorData(root);
        }
    }

    [DataContract]
    public class MapSensorData : ISensorData
    {
        private readonly Body root;

        public MapSensorData(Body root)
        {
            this.root = root;
            MapItems = root.GetSubtreeChildrenFirst()
                           .Select(e => new MapItem(e))
                           .Where(x => x.Tag != null)
                           .ToArray();
        }

        [DataMember]
        public MapItem[] MapItems { get; set; }
    }

    [DataContract]
    public class MapItem
    {
        [DataMember]
        public string Tag { get; set; }

        [DataMember]
        public double X { get; set; }

        [DataMember]
        public double Y { get; set; }

        public MapItem(Body body)
        {
            switch (body.Name)
            {
                case "DR": Tag = "RedDetail"; break;
                case "DB": Tag = "BlueDetail"; break;
                case "DG": Tag = "GreenDetail"; break;
                case "VW": Tag = "VerticalWall"; break;
                case "VWR": Tag = "VerticalRedSocket"; break;
                case "VWB": Tag = "VerticalBlueSocket"; break;
                case "VWG": Tag = "VerticalGreenSocket"; break;
                case "HW": Tag = "HorizontalWall"; break;
                case "HWR": Tag = "HorizontalRedSocket"; break;
                case "HWB": Tag = "HorizontalBlueSocket"; break;
                case "HWG": Tag = "HorizontalGreenSocket"; break;
            }
            X = body.Location.X;
            Y = body.Location.Y;
        }
    }
}