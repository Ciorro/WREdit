using WREdit.Base.Attributes;
using WREdit.Base.Entities;
using WREdit.Base.Processing;
using WREdit.Base.Processing.Properties;

namespace WREdit.Common
{
    public enum GroundType
    {
        Runway, Asphalt, Gravel, Mud, None
    }

    [EntityProcessor(DisplayName = "Test action 1")]
    internal class TestProcessor : IEntityProcessor
    {
        [Property]
        public string? TestString { get; set; }

        [Property]
        public float? TestSingle { get; set; }

        [Property]
        public double TestDouble { get; set; }

        [Property("Liczba całkowita")]
        public int? TestInt { get; set; }

        [Property("Typ wyliczeniowy")]
        public GroundType? Ground { get; set; }

        [Property("Wartość logiczna")]
        public bool? TestBoolean { get; set; }

        public void Execute(Entity entity)
        {
            //Console.WriteLine($"Executing {GetType().Name}:");

            //foreach (var property in GetType().GetProperties())
            //{
            //    if (property.HasAttribute<PropertyAttribute>())
            //    {
            //        Console.WriteLine($"{property.Name}: {property.GetValue(this)}");
            //    }
            //}

            for (int i = 0; i < 10; i++)
            {
                entity.SelectProperty("$CONNECTION_ROAD_DEAD", "number", "number", "number");
                Console.WriteLine($"Start: {entity.SelectionStart}, End: {entity.SelectionEnd}");
            }
        }

        public void RegisterProperties(ICollection<IProcessorProperty> properties) { }
    }
}
