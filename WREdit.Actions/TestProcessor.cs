using WREdit.Base.Attributes;
using WREdit.Base.Models;

namespace WREditActions
{
    public enum GroundType
    {
        Runway, Asphalt, Gravel, Mud, None
    }

    [Processor(DisplayName = "Test action 1")]
    internal class TestProcessor : GameObjectProcessor
    {
        public string? TestString { get; set; }

        [Property]
        public float? TestSingle { get; set; }
        public double TestDouble { get; set; }

        [Property("Liczba całkowita")]
        public int? TestInt { get; set; }

        [Property("Typ wyliczeniowy")]
        public GroundType? Ground { get; set; }

        [Property("Wartość logiczna")]
        public bool? TestBoolean { get; set; }

        public override void Execute(GameObject gameObject)
        {
            Console.WriteLine("Executed");
        }
    }
}
