using WREdit.Base.Attributes;
using WREdit.Base.Extensions;
using WREdit.Base.Models;
using WREdit.Base.Processing;
using WREdit.Base.Properties;
using WREdit.Common.Properties;

namespace WREdit.Common
{
    public enum GroundType
    {
        Runway, Asphalt, Gravel, Mud, None
    }

    [Processor(DisplayName = "Test action 1")]
    internal class TestProcessor : IGameObjectProcessor
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

        public void Execute(GameObject gameObject)
        {
            Console.WriteLine($"Executing {GetType().Name}:");

            foreach (var property in GetType().GetProperties())
            {
                if (property.HasAttribute<PropertyAttribute>())
                {
                    Console.WriteLine($"{property.Name}: {property.GetValue(this)}");
                }
            }
        }

        public void RegisterProperties(ICollection<IProcessorProperty> properties) { }
    }
}
