using System.Globalization;
using WREdit.Base.Attributes;
using WREdit.Base.Entities;
using WREdit.Base.Entities.Properties;
using WREdit.Base.Processing;
using WREdit.Base.Processing.Properties;
using WREdit.Common.Utils;

namespace WREdit.Common
{
    [EntityProcessor(DisplayName = "Ground replacer")]
    internal class GroundReplacer : IEntityProcessor
    {
        [Property(Help = "Set to \"None\" to replace any type of the ground.")]
        public GroundType From { get; set; }

        [Property(Help = "Set to \"None\" to remove the ground.")]
        public GroundType To { get; set; }

        public void Execute(IEntity entity)
        {
            if (From == GroundType.None)
            {
                foreach (var groundType in Enum.GetValues<GroundType>())
                {
                    if (groundType == GroundType.None)
                        continue;
                    ReplaceGround(entity, groundType);
                }
            }
            else
            {
                ReplaceGround(entity, From);
            }
        }

        public void RegisterProperties(ICollection<IProcessorProperty> properties) { }

        private void ReplaceGround(IEntity entity, GroundType replacee)
        {
            if (replacee == GroundType.None || replacee == To)
            {
                return;
            }

            IProperty? property = null;

            //Replace path
            do
            {
                if (entity.TrySelectNextProperty($"$CONNECTION_{replacee.ToPropertyString()}_DEAD number:x number:y number:z", out property))
                {
                    double x = property.GetValue<double>("x");
                    double y = property.GetValue<double>("y");
                    double z = property.GetValue<double>("z");

                    entity.RemoveSelection();

                    if (To != GroundType.None)
                    {
                        entity.Append(string.Format(
                            provider: CultureInfo.InvariantCulture,
                            format: "$CONNECTION_{0}_DEAD {1:F4} {2:F4} {3:F4}",
                            args: [To.ToPropertyString(), x, y, z]
                        ));
                    }
                }
            } while (property is not null);

            //Replace square
            do
            {
                if (entity.TrySelectNextProperty($"$CONNECTIONS_{replacee.ToPropertyString()}_DEAD_SQUARE number:x1 number:y1 number:x2 number:y2", out property))
                {
                    double x1 = property.GetValue<double>("x1");
                    double y1 = property.GetValue<double>("y1");
                    double x2 = property.GetValue<double>("x2");
                    double y2 = property.GetValue<double>("y2");

                    entity.RemoveSelection();

                    if (To != GroundType.None && To != GroundType.Dirt)
                    {
                        entity.Append(string.Format(
                            provider: CultureInfo.InvariantCulture,
                            format: "$CONNECTIONS_{0}_DEAD_SQUARE {1:F4} {2:F4} {3:F4} {4:F4}",
                            args: [To.ToPropertyString(), x1, y1, x2, y2]
                        ));
                    }
                    //The game doesn't support mudroad square so we have to create it manually
                    else if (To == GroundType.Dirt)
                    {
                        (x1, x2) = x1 > x2 ? (x2, x1) : (x1, x2);
                        (y1, y2) = y1 > y2 ? (y2, y1) : (y1, y2);
                        x1 += 3;
                        x2 -= 3;

                        int count = (int)Math.Ceiling((x2 - x1) / 6);
                        double step = (x2 - x1) / (count - 1);

                        for (int i = 0; i < count; i++)
                        {
                            CreateMudroad(entity, x1 + i * step, y1, x1 + i * step, y2);
                        }

                        CreateMudroad(entity, x1, y1 + 3, x2, y1 + 3);
                        CreateMudroad(entity, x1, y2 - 3, x2, y2 - 3);
                    }
                }
            } while (property is not null);
        }

        private void CreateMudroad(IEntity entity, double x1, double y1, double x2, double y2)
        {
            entity.Append(string.Format(
                provider: CultureInfo.InvariantCulture,
                format: "$CONNECTION_MUDROAD_DEAD {0:F4} 0 {1:F4} ",
                args: [x1, y1]
            ));

            entity.Append(string.Format(
                provider: CultureInfo.InvariantCulture,
                format: "$CONNECTION_MUDROAD_DEAD {0:F4} 0 {1:F4} ",
                args: [x2, y2]
            ));
        }
    }
}
