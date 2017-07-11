﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerProject.collidables.enemies
{
    public abstract class Attacker : Enemy
    {
        protected float WAITTIME { get; set; } = 0.4f;
        protected float ATTACKTIME { get; set; } = 0.4f;
        protected Timer waitTimer;
        protected Timer attackTimer;

        public Attacker(Vector2 position, Player player, IDs id = IDs.DEFAULT) : base(position, player, id)
        {
            attackTimer = new Timer(ATTACKTIME);
            waitTimer = new Timer(WAITTIME);
        }

        protected override void AI(GameTime gameTime)
        {
            if (waitTimer.IsFinished)
            {
                Attack(gameTime);
                attackTimer.CountDown(gameTime);
                if (attackTimer.IsFinished)
                {
                    attackTimer.Reset();
                    waitTimer.Reset();
                }
            }
            else
            {
                waitTimer.CountDown(gameTime);
                Wait(gameTime);
            }
        }

        protected abstract void Wait(GameTime gameTime);
        protected abstract void Attack(GameTime gameTime);
    }
}