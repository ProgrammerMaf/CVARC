﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CVARC.V2
{
    public class UnitResponse
    {
        private UnitResponse(){}
        public bool Processed { get; private set; }
        public double RequestedTime { get; private set; }
        public static UnitResponse Denied() { return new UnitResponse(); }
        public static UnitResponse Accepted(double time) { return new UnitResponse { Processed=true, RequestedTime = time }; }
    }

    public interface IUnit
    {
    }

    public interface IUnit<in TCommand> : IUnit
    {
        UnitResponse ProcessCommand(TCommand command);
    }
}
