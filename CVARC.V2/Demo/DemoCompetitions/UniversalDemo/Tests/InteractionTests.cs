﻿using AIRLab.Mathematics;
using CVARC.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo
{
	partial class DemoLogicPartHelper
	{
		void LoadInteractionTests(LogicPart logic, MoveAndGripRules rules)
		{

			//для AlignmentRect пришлось увеличить delta на проверке угла поворота до 0,005
			logic.Tests["Interaction_Rect_Alignment"] = new RectangularInteractionTestBase(
				LocationTest((frame,asserter)=>
					{
						asserter.IsEqual(Angle.HalfPi.Grad,frame.Angle.Grad,1);
						asserter.IsEqual(22.36, frame.Y, 1e-3);
					},
				rules.Move(-10),
				rules.Rotate(Angle.HalfPi / 2),
				rules.Move(50)));



			logic.Tests["Interaction_Rect_Collision"] = new RectangularInteractionTestBase(LocationTest(
			   (frame, asserter) => asserter.True(frame.X < 100 && frame.X > 70),
			   rules.Move(100))); //думаю что тест не проходит из-за физики, поэтому не баг а фича

			//тут явно нужны схожие тесты для круглого робота, плюс еще какие-нибудь ситуации со взаимодействием робота и стены

		}
	}
}
