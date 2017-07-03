﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.collidables.parts
{
    public interface IPartCarrier
    {
        List<Part> Parts { get; }
        void Collision(Collidable c2);
        bool AddPart(Part part, int pos);     
    }
}
