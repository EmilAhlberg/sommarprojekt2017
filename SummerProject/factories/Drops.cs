using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SummerProject.collidables;

namespace SummerProject.factories
{
    class Drops : Entities
    {
        private Timer spawnTimer;
        private const float spawnTime = 10f;
        public Drops(List<Sprite> sprites, int entityCap) : base(sprites, entityCap)
        {
            spawnTimer = new Timer(spawnTime);
            InitializeEntities(2);
        }

        public void Spawn(Vector2 source)
        {
            if (spawnTimer.IsFinished)
            {
                if (ActivateEntities(source, source))
                    spawnTimer.Reset();
            }
        }

        public void Update(GameTime gameTime)
        {
            UpdateEntities(gameTime);
            spawnTimer.CountDown(gameTime);
        }

        public override void Reset()
        {
           spawnTimer.Reset();
            ResetEntities();
        }

        protected override AIEntity CreateEntity(int type) // health pack = 2
        {
            return EntityFactory.CreateEntity(Sprites[0], type);  // temp fix, we want different EntityFactories in the future
        }                                                         // i.e. BulletFactory, DropFactory
    }
}
