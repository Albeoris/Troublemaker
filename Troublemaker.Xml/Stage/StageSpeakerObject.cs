namespace Troublemaker.Xml
{
    public sealed class StageSpeakerObject : IStageSpeaker
    {
        private readonly StagePoint _unit;

        public StageSpeakerObject(StagePoint unit)
        {
            _unit = unit;
        }

        public StageSpeaker Resolve(IStage stage)
        {
            if (!(_unit is StagePointObject obj))
                return null;

            
            if (stage.TryResolveMapComponent(obj.ObjectKey, out StageMapComponent component) && component is IStageMapUnit unit)
            {
                XmlMonster xmlMonster = DB.Monsters[unit.MonsterName];
                return new StageSpeaker {Info = xmlMonster.InfoName};
            }

            if (DB.Objects.Items.ContainsKey(obj.ObjectKey))
            {
                return new StageSpeaker {Info = obj.ObjectKey};
            }

            return null;
        }
    }
}