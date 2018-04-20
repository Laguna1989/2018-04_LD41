using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;

namespace JamUtilities.Particles
{
    public class ParticleProperties
    {
        public ParticleManager.ParticleType Type { get; set; }
        public float sizeSingle { get; set; }
        public float sizeMultiple { get; set; }
        public Color col { get; set; }
        public float lifeTime { get; set; }


        public bool AffectedByGravity { get; set; }
        public ParticleManager.ParticleRotationType RotationType { get; set; }
    }
}
