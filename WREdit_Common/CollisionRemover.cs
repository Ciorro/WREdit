using WREdit.Base.Attributes;
using WREdit.Base.Entities;
using WREdit.Base.Processing;
using WREdit.Base.Processing.Properties;

namespace WREdit.Common
{
    [EntityProcessor(DisplayName = "Collision remover")]
    internal class CollisionRemover : IEntityProcessor
    {
        [Property(DisplayName = "Value")]
        public short Value { get; set; } = -50;

        public void Execute(IEntity entity)
        {
            var existingProperty = entity.SelectNextProperty("$HARBOR_EXTEND_AREA_WHEN_BULDING number");

            if (existingProperty is null)
            {
                entity.Append($"$HARBOR_EXTEND_AREA_WHEN_BULDING {Value}");
            }
            else if (existingProperty?.GetValue<short>(0) != Value)
            {
                entity.RemoveSelection();
                entity.Append($"$HARBOR_EXTEND_AREA_WHEN_BULDING {Value}");
            }
        }

        public void RegisterProperties(ICollection<IProcessorProperty> properties) { }
    }
}
