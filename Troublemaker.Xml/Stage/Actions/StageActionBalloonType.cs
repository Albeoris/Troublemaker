using System;

namespace Troublemaker.Xml
{
    public sealed class StageActionBalloonType
    {
        public String Loudness { get; }
        public String Type { get; }

        public static StageActionBalloonType? Wrap(String? type)
        {
            if (type == null)
                return null;

            return new StageActionBalloonType(type);
        }
        
        private StageActionBalloonType(String? type)
        {
            String[] parts = type.Split('_');
            if (parts.Length != 2)
                return;

            Loudness = parts[0];
            Type = parts[1];
        }

        public Boolean IsNormal => Loudness == "Normal";
        public Boolean IsShout => Loudness == "Shout";
        public Boolean IsThink => Loudness == "Think";

        public Boolean IsAlly => Type == "Ally";
        public Boolean IsBeast => Type == "Beast";
        public Boolean IsCivil => Type == "Civil";
        public Boolean IsEnemy => Type == "Enemy";
        public Boolean IsMachine => Type == "Machine";
        public Boolean IsNeutral => Type == "Neutral";
        public Boolean IsPlayer => Type == "Player";
        public Boolean IsThird => Type == "Third";
    }
}